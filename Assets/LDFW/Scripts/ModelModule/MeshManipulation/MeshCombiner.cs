using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LDFW.Model
{


    public class MeshCombiner : MonoBehaviour
    {
        public List<Transform>                  subMeshTransformList0;
        public List<Transform>                  subMeshTransformList1;
        public List<Transform>                  subMeshTransformList2;
        public List<Transform>                  subMeshTransformList3;
        public string                           meshSavePath = "Assets/";



        public Mesh CombineMeshesWithSameUV(List<Transform> transformList)
        {
            if (transformList == null || transformList.Count <= 0)
            {
                Debug.LogError("Input transform list is null!");
                return null;
            }

            Mesh newMesh = new Mesh();
            
            List<Vector3> vertexList = new List<Vector3>();
            List<Vector2> uvList = new List<Vector2>();
            List<int> triangleList = new List<int>();
            int currentVertexListLength = 0;

            foreach (var trans in transformList)
            {
                if (trans == null)
                    continue;

                MeshFilter meshFilter = trans.GetComponent<MeshFilter>();
                if (meshFilter == null)
                    continue;

                Mesh mesh = meshFilter.mesh;
                if (mesh == null)
                    continue;

                currentVertexListLength = vertexList.Count;

                foreach (var vertex in mesh.vertices)
                    vertexList.Add(transform.InverseTransformPoint(trans.TransformPoint(vertex)));

                foreach (var uv in mesh.uv)
                    uvList.Add(uv);

                foreach (var triangleVertexIndex in mesh.triangles)
                    triangleList.Add(currentVertexListLength + triangleVertexIndex);

            }

            newMesh.vertices = vertexList.ToArray();
            newMesh.uv = uvList.ToArray();
            newMesh.triangles = triangleList.ToArray();

            return newMesh;
        }

        public Mesh CombineSubMeshes(List<Transform> subMesh0, List<Transform> subMesh1, List<Transform> subMesh2, List<Transform> subMesh3)
        {
            Mesh[] subMeshArray = new Mesh[4]
            {
                CombineMeshesWithSameUV(subMesh0),
                CombineMeshesWithSameUV(subMesh1),
                CombineMeshesWithSameUV(subMesh2),
                CombineMeshesWithSameUV(subMesh3),
            };
            

            Mesh newMesh = new Mesh();
            List<Vector3> vertexList = new List<Vector3>();
            List<Vector2> uvList = new List<Vector2>();
            List<int> triangleIndexList = new List<int>();
            int currentVertexCount = 0;

            Mesh currentMesh;
            Vector3[] currentMeshVertices;
            Vector2[] currentMeshUV;
            int[] currentMeshTriangleIndices;
            List<int>[] subMeshTriangleListArray = new List<int>[4];

            int subMeshCount = 0;


            for (int i = 0; i < 4; i++)
            {
                currentMesh = subMeshArray[i];
                subMeshTriangleListArray[i] = new List<int>();

                if (currentMesh == null)
                {
                    Debug.LogError("Stopped at subMesh" + i + " because of null mesh");
                    break;
                }

                subMeshCount++;
                currentVertexCount = vertexList.Count;
                currentMeshVertices = currentMesh.vertices;
                currentMeshUV = currentMesh.uv;
                currentMeshTriangleIndices = currentMesh.triangles;

                foreach (var vertex in currentMeshVertices)
                    vertexList.Add(vertex);

                foreach (var uv in currentMeshUV)
                    uvList.Add(uv);

                foreach (var triangleIndex in currentMeshTriangleIndices)
                {
                    subMeshTriangleListArray[i].Add(currentVertexCount + triangleIndex);
                    triangleIndexList.Add(currentVertexCount + triangleIndex);
                }

            }

            newMesh.vertices = vertexList.ToArray();

            newMesh.subMeshCount = subMeshCount;
            if (subMeshCount >= 1)
            {
                newMesh.uv = uvList.ToArray();
                newMesh.SetIndices(subMeshTriangleListArray[0].ToArray(), MeshTopology.Triangles, 0);
            }
            if (subMeshCount >= 2)
            {
                newMesh.uv2 = uvList.ToArray();
                newMesh.SetIndices(subMeshTriangleListArray[1].ToArray(), MeshTopology.Triangles, 1);
            }
            if (subMeshCount >= 3)
            {
                newMesh.uv3 = uvList.ToArray();
                newMesh.SetIndices(subMeshTriangleListArray[2].ToArray(), MeshTopology.Triangles, 2);
            }
            if (subMeshCount >= 4)
            {
                newMesh.uv4 = uvList.ToArray();
                newMesh.SetIndices(subMeshTriangleListArray[3].ToArray(), MeshTopology.Triangles, 3);
            }
            
            newMesh.RecalculateBounds();
            newMesh.RecalculateNormals();

            System.GC.Collect();

            return newMesh;
        }

    }

}