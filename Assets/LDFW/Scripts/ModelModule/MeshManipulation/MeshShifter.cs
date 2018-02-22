using UnityEngine;
using System.Collections;

namespace LDFW.Model
{

    public class MeshShifter : MonoBehaviour
    {

        public static Mesh targetMesh;
        public static Vector3 center;
        public static Vector3 offset;
        public static string savePath = "Assets/";


        public static Mesh SetMeshCenter(Mesh targetMesh, Vector3 targetReferenceCenter)
        {
            if (targetMesh == null)
            {
                Debug.LogError("Target mesh cannot be null!");
                return null;
            }

            Debug.Log("Set mesh");
            Mesh newMesh = MeshGenerator.DuplicateMesh(targetMesh);
            
            
            Vector3 diff = Vector3.Scale(targetMesh.bounds.size * 0.5f, targetReferenceCenter * -1) - targetMesh.bounds.center;

            int vertexCount = targetMesh.vertexCount;
            Vector3[] oldVertices = newMesh.vertices;
            Vector3[] newVertices = new Vector3[vertexCount];

            for (int i = 0; i < vertexCount; i++)
            {
                newVertices[i] = oldVertices[i] + diff;
            }
            newMesh.vertices = newVertices;
            oldVertices = null;
            

            newMesh.RecalculateBounds();
            newMesh.RecalculateNormals();

            System.GC.Collect();
            return newMesh;
        }

        public static Mesh ShiftMesh(Mesh targetMesh, Vector3 offsetValue)
        {
            if (targetMesh == null)
            {
                Debug.LogError("Target mesh cannot be null!");
                return null;
            }

            Mesh newMesh = MeshGenerator.DuplicateMesh(targetMesh);


            Vector3 diff = offsetValue;

            int vertexCount = targetMesh.vertexCount;
            Vector3[] oldVertices = newMesh.vertices;
            Vector3[] newVertices = new Vector3[vertexCount];

            for (int i = 0; i < vertexCount; i++)
            {
                newVertices[i] = oldVertices[i] + diff;
            }
            newMesh.vertices = newVertices;
            oldVertices = null;


            newMesh.RecalculateBounds();
            newMesh.RecalculateNormals();

            System.GC.Collect();
            return newMesh;
        }
    }

}