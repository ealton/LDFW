using UnityEngine;
using System.Collections;

using LDFW.Math;

namespace LDFW.Model
{

    public class BendObject : MonoBehaviour
    {
        
        /// <summary>
        /// Used to store bend angles on each axis, also acts as a bend flag for each axis
        /// </summary>
        public Vector3                  bendAngles;
        public Vector3                  startingPosition;

        protected MeshFilter            meshFilter;
        protected Mesh                  originalMesh;
        protected Vector3[]             originalVertices;
        protected Vector3[]             currentVertices;


        protected void Awake()
        {
            Init ();
        }

        /// <summary>
        /// Initializes script
        /// </summary>
        protected virtual void Init ()
        {
            meshFilter = GetComponent<MeshFilter> ();
            originalMesh = Instantiate(meshFilter.mesh);
            originalVertices = originalMesh.vertices;
            int vertexCount = originalVertices.Length;
            currentVertices = new Vector3[vertexCount];

            for (int i = 0; i < vertexCount; i++)
                currentVertices[i] = originalVertices[i];

            startingPosition = startingPosition.Clamp(Vector3.one * -1, Vector3.one * 1);
        }
        
        /// <summary>
        /// Recalculates vertices
        /// </summary>
        public virtual void Recalculate()
        {

        }



    }
    

}