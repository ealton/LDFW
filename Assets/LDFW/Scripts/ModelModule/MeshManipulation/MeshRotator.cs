using UnityEngine;
using System.Collections;

namespace LDFW.Model
{
    

    public class MeshRotator : MonoBehaviour
    {
        public static Mesh RotateMeshAroundXAxis(Mesh targetMesh, float degree)
        {
            if (targetMesh == null)
            {
                Debug.LogError("Target mesh cannot be null!");
                return null;
            }

            Mesh newMesh = MeshGenerator.DuplicateMesh(targetMesh);

            int vertexCount = targetMesh.vertices.Length;
            Vector3[] newVertices = new Vector3[vertexCount];

            for (int i = 0; i < vertexCount; i++)
            {
                Vector3 currentVertex = targetMesh.vertices[i];
                Vector2 rotatedVector = Rotate(new Vector2(currentVertex.y, currentVertex.z), degree);
                newVertices[i] = new Vector3(currentVertex.x, rotatedVector.x, rotatedVector.y);
            }

            newMesh.vertices = newVertices;

            newMesh.RecalculateBounds();
            newMesh.RecalculateNormals();

            System.GC.Collect();

            return newMesh;
        }

        public static Mesh RotateMeshAroundYAxis(Mesh targetMesh, float degree)
        {
            if (targetMesh == null)
            {
                Debug.LogError("Target mesh cannot be null!");
                return null;
            }

            Mesh newMesh = MeshGenerator.DuplicateMesh(targetMesh);

            int vertexCount = targetMesh.vertices.Length;
            Vector3[] newVertices = new Vector3[vertexCount];

            for (int i = 0; i < vertexCount; i++)
            {
                Vector3 currentVertex = targetMesh.vertices[i];
                Vector2 rotatedVector = Rotate(new Vector2(currentVertex.x, currentVertex.z), degree);
                newVertices[i] = new Vector3(rotatedVector.x, currentVertex.y,  rotatedVector.y);
            }

            newMesh.vertices = newVertices;

            newMesh.RecalculateBounds();
            newMesh.RecalculateNormals();

            System.GC.Collect();

            return newMesh;
        }

        public static Mesh RotateMeshAroundZAxis(Mesh targetMesh, float degree)
        {
            if (targetMesh == null)
            {
                Debug.LogError("Target mesh cannot be null!");
                return null;
            }

            Mesh newMesh = MeshGenerator.DuplicateMesh(targetMesh);

            int vertexCount = targetMesh.vertices.Length;
            Vector3[] newVertices = new Vector3[vertexCount];

            for (int i = 0; i < vertexCount; i++)
            {
                Vector3 currentVertex = targetMesh.vertices[i];
                Vector2 rotatedVector = Rotate(new Vector2(currentVertex.x, currentVertex.y), degree);
                newVertices[i] = new Vector3(rotatedVector.x, rotatedVector.y, currentVertex.z);
            }

            newMesh.vertices = newVertices;

            newMesh.RecalculateBounds();
            newMesh.RecalculateNormals();

            System.GC.Collect();

            return newMesh;
        }

        public static Vector2 Rotate(Vector2 v, float degrees)
        {
            float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
            float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

            float tx = v.x;
            float ty = v.y;

            return new Vector2((cos * tx) - (sin * ty), (sin * tx) + (cos * ty));
        }
    }
    
}
