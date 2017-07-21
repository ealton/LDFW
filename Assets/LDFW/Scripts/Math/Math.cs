using UnityEngine;
using System.Collections;

namespace LDFW.Math
{
    
    public class Math
    {
        public static Vector2 RandomPointInTriangle(Vector2 vertex0, Vector2 vertex1, Vector2 vertex2)
        {
            float rand1 = Random.value;
            float rand2 = Random.value;

            return (1 - Mathf.Sqrt(rand1)) * vertex0 + Mathf.Sqrt(rand1) * (1 - rand2) * vertex1 + Mathf.Sqrt(rand1) * rand2 * vertex2;
        }
    }

}