using UnityEngine;
using UnityEditor;


namespace LDFW.Model
{


    [CustomEditor(typeof(MeshRotator))]
    public class MeshRotatorEditor : Editor
    {
        private enum RotateReferenceAxis
        {
            x, y, z
        }

        private float targetDegree;
        private Mesh targetMesh;
        private string meshSavePath = "Assets/";
        private RotateReferenceAxis referenceAxis = RotateReferenceAxis.x;


        public override void OnInspectorGUI()
        {
            MeshRotator meshRotator = (MeshRotator)target;
            targetMesh = (Mesh)EditorGUILayout.ObjectField("Target Mesh", targetMesh, typeof(Mesh), true);
            referenceAxis = (RotateReferenceAxis)EditorGUILayout.EnumPopup("Reference axis", referenceAxis);
            targetDegree = EditorGUILayout.FloatField("Target degree", targetDegree);
            meshSavePath = EditorGUILayout.TextField("Save Path", meshSavePath);


            if (GUILayout.Button("Save"))
            {
                if (targetMesh != null)
                {
                    switch (referenceAxis)
                    {
                        case (RotateReferenceAxis.x):
                            LDFW.Tools.SaveToHardDrive.SaveAssetToFile(MeshRotator.RotateMeshAroundXAxis(targetMesh, targetDegree), meshSavePath);
                            break;
                        case (RotateReferenceAxis.y):
                            LDFW.Tools.SaveToHardDrive.SaveAssetToFile(MeshRotator.RotateMeshAroundYAxis(targetMesh, targetDegree), meshSavePath);
                            break;
                        case (RotateReferenceAxis.z):
                            LDFW.Tools.SaveToHardDrive.SaveAssetToFile(MeshRotator.RotateMeshAroundZAxis(targetMesh, targetDegree), meshSavePath);
                            break;
                    }
                }
            }

        }
    }

}
