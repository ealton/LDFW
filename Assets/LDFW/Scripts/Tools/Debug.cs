using UnityEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;
using UnityEditor.Callbacks;
#endif

namespace LDFW.Tools
{

    public static class Debug
    {
        public enum LogLevel : byte
        {
            None = 0,
            Exception = 1,
            Error = 2,
            Warning = 3,
            Info = 4,
        }

        public static LogLevel                              logLevel = LogLevel.Info;
        public static string                                infoColor = "#909090";
        public static string                                warningColor = "orange";
        public static string                                errorColor = "red";

#if UNITY_EDITOR

        private static int                                  _instanceID;
        private static int                                  _line = 104;
        private static List<StackFrame>                     _logStackFrameList = new List<StackFrame>();

        private static object                               _consoleWindow;
        private static object                               _logListView;
        private static FieldInfo                            _logListViewTotalRows;
        private static FieldInfo                            _logListViewCurrentRow;

        private static MethodInfo                           _logEntriesGetEntry;
        private static object                               _logEntry;
        
        private static FieldInfo                            _logEntryInstanceId;
        private static FieldInfo                            _logEntryLine;
        private static FieldInfo                            _logEntryCondition;

#endif

        
#if UNITY_EDITOR
        
        static Debug()
        {
            _instanceID = AssetDatabase.LoadAssetAtPath<MonoScript>("Assets/LDFW/Scripts/Tools/Debug.cs").GetInstanceID();
            _logStackFrameList.Clear();

            GetConsoleWindowListView();
        }

        private static void GetConsoleWindowListView()
        {
            if (_logListView == null)
            {
                Assembly unityEditorAssembly = Assembly.GetAssembly(typeof(EditorWindow));
                Type consoleWindowType = unityEditorAssembly.GetType("UnityEditor.ConsoleWindow");
                FieldInfo fieldInfo = consoleWindowType.GetField("ms_ConsoleWindow", BindingFlags.Static | BindingFlags.NonPublic);
                _consoleWindow = fieldInfo.GetValue(null);
                FieldInfo listViewFieldInfo = consoleWindowType.GetField("m_ListView", BindingFlags.Instance | BindingFlags.NonPublic);
                _logListView = listViewFieldInfo.GetValue(_consoleWindow);
                _logListViewTotalRows = listViewFieldInfo.FieldType.GetField("totalRows", BindingFlags.Instance | BindingFlags.Public);
                _logListViewCurrentRow = listViewFieldInfo.FieldType.GetField("row", BindingFlags.Instance | BindingFlags.Public);
                //LogEntries  
                Type logEntriesType = unityEditorAssembly.GetType("UnityEditorInternal.LogEntries");
                _logEntriesGetEntry = logEntriesType.GetMethod("GetEntryInternal", BindingFlags.Static | BindingFlags.Public);
                Type logEntryType = unityEditorAssembly.GetType("UnityEditorInternal.LogEntry");
                _logEntry = Activator.CreateInstance(logEntryType);
                _logEntryInstanceId = logEntryType.GetField("instanceID", BindingFlags.Instance | BindingFlags.Public);
                _logEntryLine = logEntryType.GetField("line", BindingFlags.Instance | BindingFlags.Public);
                _logEntryCondition = logEntryType.GetField("condition", BindingFlags.Instance | BindingFlags.Public);
            }
        }

        private static StackFrame GetListViewRowCount()
        {
            GetConsoleWindowListView();
            if (_logListView == null)
                return null;
            else
            {
                int totalRows = (int)_logListViewTotalRows.GetValue(_logListView);
                int row = (int)_logListViewCurrentRow.GetValue(_logListView);
                int logByThisClassCount = 0;
                for (int i = totalRows - 1; i >= row; i--)
                {
                    _logEntriesGetEntry.Invoke(null, new object[] { i, _logEntry });
                    string condition = _logEntryCondition.GetValue(_logEntry) as string;
                    //判断是否是由LoggerUtility打印的日志  
                    if (condition.Contains("][") && condition.Contains("Frame"))
                        logByThisClassCount++;
                }

                //同步日志列表，ConsoleWindow 点击Clear 会清理  
                while (_logStackFrameList.Count > totalRows)
                    _logStackFrameList.RemoveAt(0);
                if (_logStackFrameList.Count >= logByThisClassCount)
                    return _logStackFrameList[_logStackFrameList.Count - logByThisClassCount];
                return null;
            }
        }
        
#endif

        public static void LogBreak(object message, UnityEngine.Object sender = null)
        {
            LogInfo(message, sender);
            UnityEngine.Debug.Break();
        }

        public static void LogFormat(string format, UnityEngine.Object sender, params object[] message)
        {
            if (logLevel >= LogLevel.Info)
                LogLevelFormat(LogLevel.Info, string.Format(format, message), sender);
        }

        public static void LogFormat(string format, params object[] message)
        {
            if (logLevel >= LogLevel.Info)
                LogLevelFormat(LogLevel.Info, string.Format(format, message), null);
        }

        public static void LogInfo(object message, UnityEngine.Object sender = null)
        {
            if (logLevel >= LogLevel.Info)
                LogLevelFormat(LogLevel.Info, message, sender);
        }

        public static void LogWarning(object message, UnityEngine.Object sender = null)
        {
            if (logLevel >= LogLevel.Warning)
                LogLevelFormat(LogLevel.Warning, message, sender);
        }

        public static void LogError(object message, UnityEngine.Object sender = null)
        {
            if (logLevel >= LogLevel.Error)
                LogLevelFormat(LogLevel.Error, message, sender);
        }

        public static void LogException(Exception exption, UnityEngine.Object sender = null)
        {
            if (logLevel >= LogLevel.Exception)
                LogLevelFormat(LogLevel.Exception, exption, sender);
        }

        private static void LogLevelFormat(LogLevel level, object message, UnityEngine.Object sender)
        {
            if (!LDFWConfig.isDebug)
                return;

            string levelFormat = level.ToString().ToUpper();
            StackTrace stackTrace = new StackTrace(true);
            var stackFrame = stackTrace.GetFrame(2);
#if UNITY_EDITOR
            _logStackFrameList.Add(stackFrame);
#endif
            string stackMessageFormat = System.IO.Path.GetFileName(stackFrame.GetFileName()) + " :" + stackFrame.GetMethod().Name + "(): " + stackFrame.GetFileLineNumber() + " ";
            //Debug.Log(stackMessageFormat, sender);
            
            string timeFormat = "Frame:" + Time.frameCount + "," + DateTime.Now.Millisecond + "ms";
            string objectName = string.Empty;
            string colorFormat = infoColor;
            if (level == LogLevel.Warning)
                colorFormat = warningColor;
            else if (level == LogLevel.Error)
                colorFormat = errorColor;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<color={3}>[{0}][{4}][{1}]{2}</color>", levelFormat, timeFormat, message, colorFormat, stackMessageFormat);
            UnityEngine.Debug.Log(sb, sender);
            
        }

#if UNITY_EDITOR
        [UnityEditor.Callbacks.OnOpenAssetAttribute(0)]
        private static bool OnOpenAsset(int instanceID, int line)
        {

            string stackTrace = GetStackTrace();
            if (string.IsNullOrEmpty(stackTrace))
                return false;

            string[] lines = stackTrace.Split('\n');
            if (lines.Length <= 0)
                return false;

            int stackLineNumber = 0;
            for (; stackLineNumber < lines.Length; stackLineNumber++)
            {
                if (lines[stackLineNumber].Contains("LDFW.Tools.DebugLogger:"))
                    break;
            }

            for (; stackLineNumber < lines.Length; stackLineNumber++)
            {
                if (!lines[stackLineNumber].Contains("LDFW.Tools.DebugLogger:"))
                    break;
            }

            if (stackLineNumber < 0 || stackLineNumber >= lines.Length)
                return false;

            int index = lines[stackLineNumber].IndexOf("(at ");
            if (index < 0)
                return false;

            string fileInfo = lines[stackLineNumber].Substring(index + 4);
            fileInfo = fileInfo.Substring(0, fileInfo.Length - 1);
            string[] fileInfoList = fileInfo.Split(':');

            UnityEngine.Debug.Log("File, line = " + fileInfoList[0] + ", " + fileInfoList[1]);
            UnityEditorInternal.InternalEditorUtility.OpenFileAtLineExternal(fileInfoList[0].Replace('/', '\\'), 100);// int.Parse(fileInfoList[1]) - 1);

            /*
            Debug.Log("Load obj");
            UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath(fileInfoList[0].Replace('/', '\\'), typeof(TextAsset));
            Debug.Log("instance id = " + obj.GetInstanceID());
            AssetDatabase.OpenAsset(obj.GetInstanceID(), int.Parse(fileInfoList[1]) - 1);
            */
            return true;

        }

        private static string GetStackTrace()
        {
            var consoleWindowType = typeof(EditorWindow).Assembly.GetType("UnityEditor.ConsoleWindow");
            var fieldInfo = consoleWindowType.GetField("ms_ConsoleWindow", BindingFlags.Static | BindingFlags.NonPublic);
            var consoleWindowInstance = fieldInfo.GetValue(null);

            if (consoleWindowInstance != null)
            {
                if ((object)EditorWindow.focusedWindow == consoleWindowInstance)
                {
                    var listViewStateType = typeof(EditorWindow).Assembly.GetType("UnityEditor.ListViewState");
                    fieldInfo = consoleWindowType.GetField("m_ListView", BindingFlags.Instance | BindingFlags.NonPublic);
                    var listView = fieldInfo.GetValue(consoleWindowInstance);

                    fieldInfo = listViewStateType.GetField("row", BindingFlags.Instance | BindingFlags.Public);
                    int row = (int)fieldInfo.GetValue(listView);

                    fieldInfo = consoleWindowType.GetField("m_ActiveText", BindingFlags.Instance | BindingFlags.NonPublic);
                    string activeText = fieldInfo.GetValue(consoleWindowInstance).ToString();

                    return activeText;
                }
            }

            return null;
        }

#endif

    }

}