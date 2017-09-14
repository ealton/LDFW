using UnityEngine;
using UnityEditor;


namespace LDFW.Model
{

    
    [CustomEditor(typeof(MeshScaler))]
    public class MeshScalerEditor : Editor
    {

        private Mesh targetMesh;
        private string meshSavePath = "Assets/";
        private Vector3 scaler;

        public override void OnInspectorGUI()
        {

            targetMesh = (Mesh)EditorGUILayout.ObjectField("Target Mesh", targetMesh, typeof(Mesh), true);
            scaler = EditorGUILayout.Vector3Field("Target Scale", scaler);
            meshSavePath = EditorGUILayout.TextField("Save Path", meshSavePath);
            
            if (GUILayout.Button("Save"))
            {
                if (targetMesh != null)
                {
                    LDFW.Tools.SaveToHardDrive.SaveAssetToFile(MeshScaler.ScaleMesh(targetMesh, scaler), meshSavePath);
                }
            }
        }

    }
    
}