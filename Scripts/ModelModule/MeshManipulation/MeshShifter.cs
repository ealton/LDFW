using UnityEngine;
using System.Collections;
using LDFW.Extensions;

namespace LDFW.Model
{

    public class MeshShifter : MonoBehaviour
    {

        public static Mesh SetMeshCenter(Mesh targetMesh, Vector3 targetCenter)
        {
            if (targetMesh == null)
            {
                Debug.LogError("Target mesh cannot be null!");
                return null;
            }

            Mesh newMesh = MeshGenerator.DuplicateMesh(targetMesh);
            
            Vector3 diff = targetCenter - targetMesh.bounds.center;
            
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