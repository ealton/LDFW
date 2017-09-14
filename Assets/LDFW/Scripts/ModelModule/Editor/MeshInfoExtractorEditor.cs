using UnityEngine;
using UnityEditor;
using System.Collections;

namespace LDFW.Model
{

    [CustomEditor(typeof(MeshInfoExtractor))]
    public class MeshInfoExtractorEditor : Editor
    {

        private string info;

        private void Awake()
        {
            RefreshInfo();
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Refresh"))
                RefreshInfo();

            EditorGUILayout.HelpBox(info, MessageType.Info);
        }

        private void RefreshInfo()
        {
            MeshInfoExtractor myTarget = (MeshInfoExtractor)target;
            MeshFilter meshFilter = myTarget.GetComponent<MeshFilter>();

            if (meshFilter != null)
            {
                Mesh mesh = meshFilter.mesh;

                info =
                    MeshInfoExtractor.GetMeshVerticesData(mesh) + "\n" +
                    MeshInfoExtractor.GetTriangleData(mesh) + "\n" +
                    MeshInfoExtractor.GetMeshUVData(mesh) + "\n" +
                    MeshInfoExtractor.GetSubmeshData(mesh) +


                    MessageType.Info;

            }
            else
            {
                info = "No MeshFilter component";
            }
        }
    }

}
