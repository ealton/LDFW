using UnityEngine;
using System.Collections;

using LDFW.Extensions;

namespace LDFW.Model
{

    public class MeshGenerator : MonoBehaviour
    {

        public static Mesh GenerateDoubleSidedPlane(Vector2 size, int lengthSegments, int widthSegments)
        {
            Mesh resultMesh = new Mesh();

            int vertexCount = (lengthSegments + 1) * (widthSegments + 1);
            int trianglesCount = lengthSegments * widthSegments * 2 * 3;
            Vector3[] vertices =  new Vector3[vertexCount * 2];
            Vector3[] normals = new Vector3[vertexCount * 2];
            Vector2[] uvs = new Vector2[vertexCount * 2];
            int[] triangles = new int[trianglesCount * 2];




            #region GenerateFirstSide
            // Generate the left most width
            for (int i = 0; i <= widthSegments; i++)
            {
                vertices[i] = new Vector3 (size.x * 0.5f, 0, size.y * (-0.5f + (float) i / widthSegments));
                normals[i] = Vector3.up;
                uvs[i] = new Vector2 (0, (float) i / widthSegments);
            }

            int currentVertexIndexTempVar = 0;
            int currentTriangleIndexTempVar = 0;

            for (int i = 1; i <= lengthSegments; i++)       // length
            {
                // Generate starting point
                currentVertexIndexTempVar = i * (widthSegments + 1);

                vertices[currentVertexIndexTempVar] = new Vector3 (size.x * (0.5f - (float) i / lengthSegments), 0, size.y * -0.5f);
                normals[currentVertexIndexTempVar] = Vector3.up;
                uvs[currentVertexIndexTempVar] = new Vector2 ((float) i / lengthSegments, 0);


                for (int j = 1; j <= widthSegments; j++)        // width
                {
                    currentVertexIndexTempVar = i * (widthSegments + 1) + j;
                    currentTriangleIndexTempVar = ((i - 1) * widthSegments + (j - 1)) * 6;

                    vertices[currentVertexIndexTempVar] = new Vector3 (size.x * (0.5f - (float) i / lengthSegments), 0, size.y * (-0.5f + (float) j / widthSegments));
                    normals[currentVertexIndexTempVar] = Vector3.up;
                    uvs[currentVertexIndexTempVar] = new Vector2 ((float) i / lengthSegments, (float) j / widthSegments);

                    triangles[currentTriangleIndexTempVar] = currentVertexIndexTempVar - 1;
                    triangles[currentTriangleIndexTempVar + 2] = currentVertexIndexTempVar - 1 - widthSegments;
                    triangles[currentTriangleIndexTempVar + 1] = currentVertexIndexTempVar - 1 - widthSegments - 1;

                    triangles[currentTriangleIndexTempVar + 4] = currentVertexIndexTempVar - 1;
                    triangles[currentTriangleIndexTempVar + 3] = currentVertexIndexTempVar;
                    triangles[currentTriangleIndexTempVar + 5] = currentVertexIndexTempVar - 1 - widthSegments;

                }
            }
            #endregion
            


            #region GenerateSecondSide
            // Generate the left most width

            for (int i = 0; i <= widthSegments; i++)
            {
                vertices[i + vertexCount] = new Vector3 (size.x * 0.5f, 0, size.y * (-0.5f + (float) i / widthSegments));
                normals[i + vertexCount] = Vector3.down;
                uvs[i + vertexCount] = new Vector2 (0, (float) i / widthSegments);
            }

            currentVertexIndexTempVar = 0;
            currentTriangleIndexTempVar = 0;

            for (int i = 1; i <= lengthSegments; i++)       // length
            {
                // Generate starting point
                currentVertexIndexTempVar = i * (widthSegments + 1) + vertexCount;

                vertices[currentVertexIndexTempVar] = new Vector3 (size.x * (0.5f - (float) i / lengthSegments), 0, size.y * -0.5f);
                normals[currentVertexIndexTempVar] = Vector3.down;
                uvs[currentVertexIndexTempVar] = new Vector2 ((float) i / lengthSegments, 0);


                for (int j = 1; j <= widthSegments; j++)        // width
                {
                    currentVertexIndexTempVar = i * (widthSegments + 1) + j + vertexCount;
                    currentTriangleIndexTempVar = ((i - 1) * widthSegments + (j - 1)) * 6 + trianglesCount;
                    
                    vertices[currentVertexIndexTempVar] = new Vector3 (size.x * (0.5f - (float) i / lengthSegments), 0, size.y * (-0.5f + (float) j / widthSegments));
                    normals[currentVertexIndexTempVar] = Vector3.down;
                    uvs[currentVertexIndexTempVar] = new Vector2 ((float) i / lengthSegments, (float) j / widthSegments);

                    triangles[currentTriangleIndexTempVar] = currentVertexIndexTempVar - 1;
                    triangles[currentTriangleIndexTempVar + 1] = currentVertexIndexTempVar - 1 - widthSegments;
                    triangles[currentTriangleIndexTempVar + 2] = currentVertexIndexTempVar - 1 - widthSegments - 1;

                    triangles[currentTriangleIndexTempVar + 3] = currentVertexIndexTempVar;
                    triangles[currentTriangleIndexTempVar + 4] = currentVertexIndexTempVar - 1 - widthSegments;
                    triangles[currentTriangleIndexTempVar + 5] = currentVertexIndexTempVar - 1;

                }
            }
            #endregion


            resultMesh.vertices = vertices;
            resultMesh.triangles = triangles;
            resultMesh.uv = uvs;
            resultMesh.normals = normals;


            resultMesh.RecalculateBounds ();
            resultMesh.RecalculateNormals ();

            resultMesh.PrintMeshData();
            return resultMesh;
        }

        public static Mesh GenerateCube(Vector3 size, Vector3 sizeSegments)
        {
            return null;
        }

        public static Mesh DuplicateMesh(Mesh targetMesh)
        {
            if (targetMesh == null)
            {
                Debug.LogError("Target Mesh cannot be null!");
                return null;
            }

            return Instantiate(targetMesh);
        }

        
    }

}