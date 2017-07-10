using UnityEngine;
using UnityEditor;


namespace LDFW.Model
{

    
    [CustomEditor(typeof(MeshShifter))]
    public class MeshShifterEditor : Editor
    {

        private Mesh targetMesh;
        private string meshSavePath = "Assets/";
        private Vector3 newCenter;

        public override void OnInspectorGUI()
        {
            MeshShifter meshGen = (MeshShifter)target;
            targetMesh = (Mesh)EditorGUILayout.ObjectField("Target Mesh", targetMesh, typeof(Mesh), true);
            newCenter = EditorGUILayout.Vector3Field("Target Center", newCenter);
            meshSavePath = EditorGUILayout.TextField("Save Path", meshSavePath);


            if (GUILayout.Button("Save"))
            {
                if (targetMesh != null)
                {
                    LDFW.Tools.SaveToHardDrive.SaveToFile(MeshShifter.SetMeshCenter(targetMesh, newCenter), meshSavePath);
                }
            }
        }

    }
    
}