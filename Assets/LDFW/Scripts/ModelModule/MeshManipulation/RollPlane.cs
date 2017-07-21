using UnityEngine;
using System.Collections;

using LDFW.Math;

namespace LDFW.Model
{

    public class RollPlane : BendObject
    {

        public float                    initialRadius;
        public Vector3                  radiusDecreasingRate = Vector3.one * 0.1f;
        public Vector3                  rollDirection = Vector3.one;
        public bool                     isUpward = true;

        
        public override void Recalculate ()
        {
            if (initialRadius <= 0)
            {
                Debug.LogError("Initial radius must be greater than 0");
                return;
            }

            if (bendAngles == Vector3.zero)
            {
                Debug.LogError("Bend angles for all three axis are 0, can't bend");
                return;
            }

            Bounds meshBounds = originalMesh.bounds;
            
            
            Vector3 startingPositionValues = originalMesh.bounds.center + originalMesh.bounds.size.ScaleMultiplier(startingPosition * 0.5f);
            
            int vertexCount = originalVertices.Length;
            Vector3 currentVertex;
            float currentRadius;
            float targetDegree;
            int multiplier = 1;

            for (int i = 0; i < vertexCount; i++)
            {
                currentVertex = originalVertices[i];
                if (bendAngles.z != 0)
                {
                    if (currentVertex.x > startingPositionValues.x)
                    {
                        targetDegree = GetTargetDegreeBasedOnLineLength(initialRadius, radiusDecreasingRate.x, currentVertex.x - startingPositionValues.x, out currentRadius) + 90;


                        if (rollDirection.x > 0)
                            multiplier = -1;

                        targetDegree *= multiplier;

                        currentVertices[i].x = startingPositionValues.x - Mathf.Cos(targetDegree * Mathf.Deg2Rad) * currentRadius;
                        currentVertices[i].y = Mathf.Sin(targetDegree * Mathf.Deg2Rad) * currentRadius + initialRadius * multiplier;

                        currentVertex = currentVertices[i];
                    }
                }
                else if (bendAngles.x != 0)
                {
                    if (currentVertex.z > startingPositionValues.z)
                    {
                        targetDegree = GetTargetDegreeBasedOnLineLength(initialRadius, radiusDecreasingRate.z, currentVertex.z - startingPositionValues.z, out currentRadius) + 90;
                        
                        if (rollDirection.z > 0)
                            multiplier = -1;

                        targetDegree *= multiplier;

                        currentVertices[i].z = startingPositionValues.z - Mathf.Cos(targetDegree * Mathf.Deg2Rad) * currentRadius;
                        currentVertices[i].y = Mathf.Sin(targetDegree * Mathf.Deg2Rad) * currentRadius + initialRadius * multiplier;
                        
                        currentVertex = currentVertices[i];
                    }
                }

                currentVertices[i] = currentVertex;
            }

            meshFilter.mesh.vertices = currentVertices;
            meshFilter.mesh.RecalculateBounds();
            
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