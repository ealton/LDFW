using UnityEditor;
using UnityEngine;

namespace LDFW.Tween
{

    [CanEditMultipleObjects]
    [CustomEditor(typeof(LDFWTweenBaseOne))]
    public class LDFWTweenBaseOneEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            LDFWTweenBaseOne myTarget = (LDFWTweenBaseOne)target;

            //base.OnInspectorGUI();
            myTarget.targetTransform = (Transform)EditorGUILayout.ObjectField("Target", myTarget.targetTransform, typeof(Transform), true);
            myTarget.autoPlay = EditorGUILayout.Toggle("Auto Play", myTarget.autoPlay);
            myTarget.autoDestroyComponent = EditorGUILayout.Toggle("Destroy Component", myTarget.autoDestroyComponent);
            myTarget.autoDestroyGameObject = EditorGUILayout.Toggle("Destroy GameObject", myTarget.autoDestroyGameObject);

            myTarget.tweenStyle = (TweenStyle)EditorGUILayout.EnumPopup("Tween Style", myTarget.tweenStyle);
            myTarget.curveStyle = (CurveStyle)EditorGUILayout.EnumPopup("Curve Style", myTarget.curveStyle);

            myTarget.fromValueFloat = EditorGUILayout.FloatField("From Value", myTarget.fromValueFloat);
            myTarget.toValueFloat = EditorGUILayout.FloatField("To Value", myTarget.toValueFloat);

            myTarget.startDelay = EditorGUILayout.FloatField("Start Delay", myTarget.startDelay);
            myTarget.duration = EditorGUILayout.FloatField("Tween Duration", myTarget.duration);
            myTarget.targetTimeScale = EditorGUILayout.FloatField("Time Scale", myTarget.targetTimeScale);

            if (myTarget.curveStyle == CurveStyle.Custom)
            {
                if (myTarget.curveList == null || myTarget.curveList.Length != 1)
                {
                    myTarget.curveList = new AnimationCurve[1];
                    myTarget.curveList[0] = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
                }

                myTarget.curveList[0] = EditorGUILayout.CurveField("X Curve", myTarget.curveList[0]);
                myTarget.generateRandomCurveBasedOnFromAndTo = EditorGUILayout.Toggle("Use random curve", myTarget.generateRandomCurveBasedOnFromAndTo);
            }
        }
    }

    [CustomEditor(typeof(LDFWTweenCameraFieldOfView))]
    public class LDFWTweenCameraFieldOfViewEditor : LDFWTweenBaseOneEditor
    {
    }

    [CustomEditor(typeof(LDFWTweenEmpty))]
    public class LDFWTweenEmptyEditor : LDFWTweenBaseOneEditor
    {
    }

    [CustomEditor(typeof(LDFWTweenUICanvasGroup))]
    public class LDFWTweenUICanvasGroupEditor : LDFWTweenBaseOneEditor
    {
    }

    [CustomEditor(typeof(LDFWTweenUIImageFilledRate))]
    public class LDFWTweenUIImageFilledRateEditor : LDFWTweenBaseOneEditor
    {
    }

    [CustomEditor(typeof(LDFWTweenUILayoutElement))]
    public class LDFWTweenUILayoutElementEditor : LDFWTweenBaseOneEditor
    {
    }

}