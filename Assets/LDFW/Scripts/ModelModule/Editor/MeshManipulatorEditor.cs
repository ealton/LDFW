using UnityEngine;
using UnityEditor;
using System.Collections;

namespace LDFW.Model
{

    [CustomEditor (typeof (MeshManipulator))]
    public class MeshManipulatorEditor : Editor
    {

        private string meshSavePath = "Assets/";

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            MeshManipulator meshManipulator = (MeshManipulator)target;

            meshSavePath = EditorGUILayout.TextField("Save Path", meshSavePath);
            if (GUILayout.Button("Save to file"))
            {
                Mesh targetMesh = meshManipulator.GetCurrentMesh();
                LDFW.Tools.SaveToHardDrive.SaveToFile(targetMesh, meshSavePath);
            }
        }
    }

}