using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

namespace LDFW.Model
{

    [CustomEditor(typeof(MeshCombiner))]
    public class MeshCombinerEditor : Editor
    {

        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (Application.isPlaying)
            {

                MeshCombiner myTarget = (MeshCombiner)target;

                if (GUILayout.Button("Combine All"))
                {
                    Mesh mesh = myTarget.CombineSubMeshes(
                        myTarget.subMeshTransformList0,
                        myTarget.subMeshTransformList1,
                        myTarget.subMeshTransformList2,
                        myTarget.subMeshTransformList3
                        );
                    if (mesh != null)
                        LDFW.Tools.SaveToHardDrive.SaveAssetToFile(mesh, myTarget.meshSavePath);

                }

                if (GUILayout.Button("Combine Set 0"))
                {
                    Mesh mesh = myTarget.CombineMeshesWithSameUV(myTarget.subMeshTransformList0);
                    if (mesh != null)
                        LDFW.Tools.SaveToHardDrive.SaveAssetToFile(mesh, myTarget.meshSavePath);
                }

                if (GUILayout.Button("Combine Set 1"))
                {
                    Mesh mesh = myTarget.CombineMeshesWithSameUV(myTarget.subMeshTransformList1);
                    if (mesh != null)
                        LDFW.Tools.SaveToHardDrive.SaveAssetToFile(mesh, myTarget.meshSavePath);
                }

                if (GUILayout.Button("Combine Set 2"))
                {
                    Mesh mesh = myTarget.CombineMeshesWithSameUV(myTarget.subMeshTransformList2);
                    if (mesh != null)
                        LDFW.Tools.SaveToHardDrive.SaveAssetToFile(mesh, myTarget.meshSavePath);
                }

                if (GUILayout.Button("Combine Set 3"))
                {
                    Mesh mesh = myTarget.CombineMeshesWithSameUV(myTarget.subMeshTransformList3);
                    if (mesh != null)
                        LDFW.Tools.SaveToHardDrive.SaveAssetToFile(mesh, myTarget.meshSavePath);
                }



                //trans1 = (Transform)EditorGUILayout.ObjectField("Transform1", trans1, typeof(Transform), true);
                //trans2 = (Transform)EditorGUILayout.ObjectField("Transform1", trans2, typeof(Transform), true);
                //meshSavePath = EditorGUILayout.TextField("Save Path", meshSavePath);

                //if (GUILayout.Button("Combine"))
                //{
                //    if (trans1 != null && trans2 != null)
                //    {
                //        try
                //        {
                //            mesh1 = trans1.GetComponent<MeshFilter>().mesh;
                //            mesh2 = trans2.GetComponent<MeshFilter>().mesh;

                //            if (mesh1 != null && mesh2 != null)
                //            {
                //                LDFW.Tools.SaveToHardDrive.SaveAssetToFile(MeshCombiner.CombineTwoMeshes(mesh1, mesh2, trans1.localPosition, trans2.localPosition), meshSavePath);
                //            }
                //        }
                //        catch (Exception e)
                //        {
                //            Debug.LogError(e.Message + "\n" + e.StackTrace);
                //        }
                //    }

                //}
            }
            else
            {
                EditorGUILayout.HelpBox("Mesh combiner needs to run in playing mode!", MessageType.Error);
            }
        }
    }

}
