using UnityEngine;
using UnityEditor;


namespace LDFW.Model
{


    [CustomEditor(typeof(MeshShifter))]
    public class MeshShifterEditor : Editor
    {

        public override void OnInspectorGUI()
        {

            GUILayout.Label("Reset center");
            MeshShifter.targetMesh = (Mesh)EditorGUILayout.ObjectField("Target Mesh", MeshShifter.targetMesh, typeof(Mesh), true);
            MeshShifter.center = EditorGUILayout.Vector3Field("Target Center", MeshShifter.center);
            MeshShifter.savePath = EditorGUILayout.TextField("Save Path", MeshShifter.savePath);

            if (GUILayout.Button("Save"))
            {
                LDFW.Tools.SaveToHardDrive.SaveAssetToFile(MeshShifter.SetMeshCenter(MeshShifter.targetMesh, MeshShifter.center), MeshShifter.savePath);

            }

            EditorGUILayout.Space();
            GUILayout.Label("Shift vertices");
            MeshShifter.targetMesh = (Mesh)EditorGUILayout.ObjectField("Target Mesh", MeshShifter.targetMesh, typeof(Mesh), true);
            MeshShifter.offset = EditorGUILayout.Vector3Field("Target Center", MeshShifter.offset);
            MeshShifter.savePath = EditorGUILayout.TextField("Save Path", MeshShifter.savePath);



            if (GUILayout.Button("Save"))
            {
                LDFW.Tools.SaveToHardDrive.SaveAssetToFile(MeshShifter.ShiftMesh(MeshShifter.targetMesh, MeshShifter.offset), MeshShifter.savePath);

            }
        }

    }

}