using UnityEngine;
using System.Collections;
using LDFW.Extensions;

namespace LDFW.Model
{

    public class Roll3DMesh : BendObject
    {

        public float                    initialRadius;
        public Vector3                  radiusDecreasingRate = Vector3.one * 0.1f;
        public bool                     isUpward = true;
        public Vector3                  referencePlanePosition;

        private Vector3                 meshExtents;
        private Mesh[]                  meshList;
        private int                     meshListCount;


        protected override void Init()
        {
            base.Init();


            MeshFilter[] mFilters = transform.GetComponentsInChildren<MeshFilter>();
            meshListCount = mFilters.Length;
            meshList = new Mesh[meshListCount];

            for (int i = 0; i < meshListCount; i++)
            {
                meshList[i] = mFilters[i].mesh;
            }
        }

        public override void Recalculate()
        {
            if (initialRadius <= 0)
            {
                Debug.LogError("Initial radius must be greater than 0");
                return;
            }
            referencePlanePosition = referencePlanePosition.Clamp(Vector3.one * -1, Vector3.one);
            
            base.Recalculate();

            Bounds meshBounds = originalMesh.bounds;
            meshExtents = meshBounds.extents;

            if (bendAngles.z != 0)
            {
                float bendAngle = bendAngles.z;
                float startingPositionXValue = originalMesh.bounds.center.x + (startingPosition.x * 0.5f * originalMesh.bounds.size.x);


                int vertexCount = originalVertices.Length;
                Vector3 currentVertex;
                float currentRadius;

                for (int i = 0; i < vertexCount; i++)
                {
                    currentVertex = originalVertices[i];
                    //Debug.Log ("currentVertex X = " + currentVertex.x);
                    if (currentVertex.x > startingPositionXValue)
                    {

                        currentVertex.y = referencePlanePosition.y * meshExtents.y;
                        float targetDegree = GetTargetDegreeBasedOnLineLength(initialRadius, radiusDecreasingRate.x, currentVertex.x - startingPositionXValue, out currentRadius) + 90;
                        if (isUpward)
                        {
                            targetDegree = -targetDegree;
                        }
                        //Debug.Log("TargetDegree = " + targetDegree + " and InitialRadius = " + initialRadius);


                        currentVertices[i].x = startingPositionXValue - Mathf.Cos(targetDegree * Mathf.Deg2Rad) * currentRadius;

                        if (isUpward)
                        {
                            currentVertices[i].y = Mathf.Sin(targetDegree * Mathf.Deg2Rad) * currentRadius - initialRadius;
                        }
                        else
                        {
                            currentVertices[i].y = Mathf.Sin(targetDegree * Mathf.Deg2Rad) * currentRadius + initialRadius;
                        }
                    }
                    else
                    {
                        currentVertices[i] = originalVertices[i];
                    }

                }

                meshFilter.mesh.vertices = currentVertices;
                meshFilter.mesh.RecalculateBounds();
            }
        }

        public static float GetTargetDegreeBasedOnLineLength(float initialRadius, float radiusDecreasingRate, float lineLength, out float currentRadius)
        {
            currentRadius = initialRadius * (1 - lineLength / (2 * Mathf.PI * initialRadius) * radiusDecreasingRate);
            lineLength %= 2 * Mathf.PI * initialRadius;

            float circumference = 2 * Mathf.PI * initialRadius;
            float targetDegree = 180 - lineLength / circumference * 360;
            if (targetDegree < 0)
            {
                targetDegree += 360;
            }

            return targetDegree;
        }
    }

}