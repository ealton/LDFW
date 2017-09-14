using UnityEngine;
using UnityEditor;

namespace LDFW.Model
{

    [CustomEditor(typeof(MeshGenerator))]
    public class MeshGeneratorEditor : Editor
    {
        public enum MeshType
        {
            DoubleSidedPlane,
            Cube,
            Mesh,
        }

        private MeshType currentMeshType = MeshType.DoubleSidedPlane;

        // Plane
        private Vector2 planeSize;
        private int planeLengthSegmentCount;
        private int planeDepthSegmentCount;

        // Cube
        private Vector3 cubeSize;
        private int cubeXSegmentCount;
        private int cubeYSegmentCount;
        private int cubeZSegmentCount;

        // Mesh
        private Mesh targetMesh;


        private string meshSavePath = "Assets/";

        public override void OnInspectorGUI()
        {

            //EditorGUILayout.TextArea

            currentMeshType = (MeshType)EditorGUILayout.EnumPopup("Mesh Type", currentMeshType);

            if (currentMeshType == MeshType.DoubleSidedPlane)
            {
                planeSize = EditorGUILayout.Vector2Field("Size", planeSize);
                planeLengthSegmentCount = EditorGUILayout.IntField("Length Segment Count", planeLengthSegmentCount);
                planeDepthSegmentCount = EditorGUILayout.IntField("Depth Segment Count", planeDepthSegmentCount);
            }
            else if (currentMeshType == MeshType.Cube)
            {
                cubeSize = EditorGUILayout.Vector3Field("Size", cubeSize);
                cubeXSegmentCount = EditorGUILayout.IntField("X-Axis Segment Count", cubeXSegmentCount);
                cubeYSegmentCount = EditorGUILayout.IntField("Y-Axis Segment Count", cubeYSegmentCount);
                cubeZSegmentCount = EditorGUILayout.IntField("Z-Axis Segment Count", cubeZSegmentCount);
            }
            else if (currentMeshType == MeshType.Mesh)
            {
                targetMesh = (Mesh)EditorGUILayout.ObjectField("Target Mesh", targetMesh, typeof(Mesh), true);
            }

            meshSavePath = EditorGUILayout.TextField("Save Path", meshSavePath);
            if (GUILayout.Button("Save to project"))
            {
                switch (currentMeshType)
                {
                    case MeshType.DoubleSidedPlane:
                        if (planeLengthSegmentCount < 1 || planeDepthSegmentCount < 1)
                        {
                            Debug.LogError("Segment count must be greater than 0");
                            break;
                        }

                        LDFW.Tools.SaveToHardDrive.SaveAssetToFile(MeshGenerator.GenerateDoubleSidedPlane(planeSize, planeLengthSegmentCount, planeDepthSegmentCount), meshSavePath);
                        break;
                    case MeshType.Cube:
                        if (cubeXSegmentCount < 1 || cubeYSegmentCount < 1 || cubeZSegmentCount < 1)
                        {
                            Debug.LogError("Segment count must be greater than 0");
                            break;
                        }

                        LDFW.Tools.SaveToHardDrive.SaveAssetToFile(MeshGenerator.GenerateCube(cubeSize, new Vector3(cubeXSegmentCount, cubeYSegmentCount, cubeZSegmentCount)), meshSavePath);
                        break;
                    case MeshType.Mesh:
                        Mesh newMesh = MeshGenerator.DuplicateMesh(targetMesh);
                        if (newMesh == null)
                            Debug.LogError("NewMesh is null!");
                        else
                            LDFW.Tools.SaveToHardDrive.SaveAssetToFile(MeshGenerator.DuplicateMesh(targetMesh), meshSavePath);
                        break;
                }
            }
        }
    }

}