using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LDFW.Math
{

    public class BezierCurve
    {

        // List of vector3 used to store point coordinates
        private List<Vector3> points;
        // Cached points
        private Dictionary<int, Vector3> cachedPoints;
        // Precision
        private int precision;

        /// <summary>
        /// Constructor
        /// </summary>
        public BezierCurve()
        {
            Reset();
        }

        /// <summary>
        /// Initialization
        /// </summary>
        public void Reset()
        {
            points = new List<Vector3>();
            cachedPoints = new Dictionary<int, Vector3>();
            precision = 0;
        }

        /// <summary>
        /// Generate Cache based on segment count
        /// </summary>
        /// <param name="segmentCount"></param>
        public void GenerateCache(int segmentCount)
        {
            precision = Mathf.Max(segmentCount, 1);

            for (int i = 0; i <= precision; i++)
                InternalEvaluate(i);

        }

        /// <summary>
        /// Evaluate based on time provided
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Vector3? Evaluate(float t)
        {
            if (t < 0 || t > 1)
            {
                Debug.LogError("t must be [0, 1]");
                return null;
            }

            float targetKey = t * precision;
            int targetKeyFloor = Mathf.FloorToInt(targetKey);
            int targetKeyCeiling = Mathf.CeilToInt(targetKey);

            if (targetKey == targetKeyFloor)
            {
                return cachedPoints[targetKeyFloor];
            }
            else
            {
                return cachedPoints[targetKeyFloor] * ((targetKeyCeiling - targetKey) / (targetKeyCeiling - targetKeyFloor)) +
                    cachedPoints[targetKeyCeiling] * ((targetKey - targetKeyFloor) / (targetKeyCeiling - targetKeyFloor));
            }
        }

        /// <summary>
        /// Add point
        /// </summary>
        /// <param name="point"></param>
        public void AddPoint(Vector3 point)
        {
            points.Add(point);
        }

        /// <summary>
        /// Get total point count
        /// </summary>
        /// <returns></returns>
        public int GetPointCount()
        {
            return points.Count;
        }

        private Vector3? InternalEvaluate(int cacheIndex)
        {
            float t = (float)cacheIndex / precision;

            if (t < 0 || t > 1)
            {
                Debug.LogError("t must be [0, 1]");
                return null;
            }

            int n = points.Count - 1;
            if (points.Count < 2)
            {
                Debug.LogError("needs at least 2 points");
                return null;
            }

            Vector3 result = Vector3.zero;
            for (int i = 0; i <= n; i++)
                result += (GetBinomialCoefficient(n, i) * points[i] * Mathf.Pow(1f - t, n - i) * Mathf.Pow(t, i));

            cachedPoints.Add(cacheIndex, result);
            return result;
        }

        /// <summary>
        /// Binomial Coefficient
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long GetBinomialCoefficient(int m, int n)
        {
            if (m < n)
                return 0;
            else if (n == 0)
                return 1;
            long result = 1;

            for (int i = 0; i <= n - 1; i++)
                result *= (m - i);

            for (; n > 0; n--)
                result /= n;
            
            return result;
        }

    }
}