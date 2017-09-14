using UnityEngine;
using System.Collections;

namespace LDFW.Model
{


    public class MeshScaler : MonoBehaviour
    {

        public static Mesh ScaleMesh(Mesh targetMesh, Vector3 scaleMultiplier)
        {
            if (targetMesh == null)
            {
                Debug.LogError("Target mesh cannot be null");
                return null;
            }

            Mesh newMesh = MeshGenerator.DuplicateMesh(targetMesh);

            Vector3[] oldVertices = newMesh.vertices;
            Vector3[] newVertices = new Vector3[oldVertices.Length];

            for (int i = 0; i < oldVertices.Length; i++)
            {
                newVertices[i] = Vector3.Scale(oldVertices[i], scaleMultiplier);
            }

            newMesh.vertices = newVertices;

            newMesh.RecalculateBounds();
            newMesh.RecalculateNormals();

            System.GC.Collect();
            return newMesh;
        }
        
    }

}
