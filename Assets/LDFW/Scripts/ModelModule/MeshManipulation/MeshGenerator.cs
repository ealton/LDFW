using UnityEngine;
using System.Collections;


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
                
            /*
            int verticesCount =     targetMesh.vertices == null ?       0 : targetMesh.vertices.Length;
            int triangleCount =     targetMesh.triangles == null ?      0 : targetMesh.triangles.Length;
            int uvCount =           targetMesh.uv == null ?             0 : targetMesh.uv.Length;
            int uv2Count =          targetMesh.uv2 == null ?            0 : targetMesh.uv2.Length;
            int uv3Count =          targetMesh.uv3 == null ?            0 : targetMesh.uv3.Length;
            int uv4Count =          targetMesh.uv4 == null ?            0 : targetMesh.uv4.Length;

            Mesh newMesh = new Mesh();

            if (verticesCount > 0)
            {
                Vector3[] vertices = new Vector3[verticesCount];
                for (int i = 0; i < verticesCount; i++)
                    vertices[i] = targetMesh.vertices[i];

                newMesh.vertices = vertices;
            }

            if (triangleCount > 0)
            {
                int[] triangles = new int[triangleCount];
                for (int i = 0; i < triangleCount; i++)
                    triangles[i] = targetMesh.triangles[i];

                newMesh.triangles = triangles;
            }

            if (uvCount > 0)
            {
                Vector2[] uvs = new Vector2[uvCount];
                for (int i = 0; i < uvCount; i++)
                    uvs[i] = targetMesh.uv[i];

                newMesh.uv = uvs;
            }

            if (uv2Count > 0)
            {
                Vector2[] uv2s = new Vector2[uv2Count];
                for (int i = 0; i < uv2Count; i++)
                    uv2s[i] = targetMesh.uv2[i];

                newMesh.uv2 = uv2s;
            }

            if (uv3Count > 0)
            {
                Vector2[] uv3s = new Vector2[uv3Count];
                for (int i = 0; i < uv3Count; i++)
                    uv3s[i] = targetMesh.uv3[i];

                newMesh.uv3 = uv3s;
            }

            if (uv4Count > 0)
            {
                Vector2[] uv4s = new Vector2[uv4Count];
                for (int i = 0; i < uv4Count; i++)
                    uv4s[i] = targetMesh.uv4[i];

                newMesh.uv4 = uv4s;
            }

            
            return newMesh;
            */
        }

        
    }

}