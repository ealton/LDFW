using UnityEngine;
using System;
using System.Collections;

namespace LDFW.Math
{
    
    public static class Extensions
    {


        #region NumbersAndVectors
        /// <summary>
        /// Parse string to Vector4
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Vector4 ParseToVector4(this string str)
        {
            char[] delimiters = { ',' };
            string[] temp = str.Split(delimiters);
            try
            {
                return new Vector4(float.Parse(temp[0]), float.Parse(temp[1]), float.Parse(temp[2]), float.Parse(temp[3]));
            }
            catch (Exception e)
            {
                Debug.LogError("ParseVector4 (" + str + ") error: " + e.StackTrace);
                return Vector4.zero;
            }
        }

        /// <summary>
        /// Parse string to Vector3
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Vector3 ParseToVector3(this string str)
        {
            char[] delimiters = { ',' };
            string[] temp = str.Split(delimiters);
            try
            {
                return new Vector3(float.Parse(temp[0]), float.Parse(temp[1]), float.Parse(temp[2]));
            }
            catch (Exception e)
            {
                Debug.LogError("ParseVector3 (" + str + ") error: " + e.StackTrace);
                return Vector3.zero;
            }
        }

        /// <summary>
        /// Parse string to Vector2
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Vector2 ParseToVector2(this string str)
        {
            char[] delimiters = { ',' };
            string[] temp = str.Split(delimiters);
            try
            {
                return new Vector2(float.Parse(temp[0]), float.Parse(temp[1]));
            }
            catch (Exception e)
            {
                Debug.LogError("ParseVector2 (" + str + ") error: " + e.StackTrace);
                return Vector2.zero;
            }
        }

        /// <summary>
        /// Clamps the Vector4 between min and max
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static Vector4 Clamp(this Vector4 vec, Vector4 min, Vector4 max)
        {
            return new Vector4(
                Mathf.Clamp(vec.x, min.x, max.x),
                Mathf.Clamp(vec.y, min.y, max.y),
                Mathf.Clamp(vec.z, min.z, max.z),
                Mathf.Clamp(vec.w, min.w, max.w));
        }

        /// <summary>
        /// Clamps the Vector3 between min and max
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static Vector3 Clamp(this Vector3 vec, Vector3 min, Vector3 max)
        {
            return new Vector3(
                Mathf.Clamp(vec.x, min.x, max.x),
                Mathf.Clamp(vec.y, min.y, max.y),
                Mathf.Clamp(vec.z, min.z, max.z));
        }

        /// <summary>
        /// Clamps the Vector2 between min and max
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static Vector2 Clamp(this Vector2 vec, Vector2 min, Vector2 max)
        {
            return new Vector2(
                Mathf.Clamp(vec.x, min.x, max.x),
                Mathf.Clamp(vec.y, min.y, max.y));
        }

        /// <summary>
        /// Memberwise vector sacle
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="multiplier"></param>
        /// <returns></returns>
        public static Vector4 ScaleMultiplier(this Vector4 vec, Vector4 multiplier)
        {
            return new Vector4(
                vec.x * multiplier.x,
                vec.y * multiplier.y,
                vec.z * multiplier.z,
                vec.w * multiplier.w);
        }

        /// <summary>
        /// Memberwise vector sacle
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="multiplier"></param>
        /// <returns></returns>
        public static Vector3 ScaleMultiplier(this Vector3 vec, Vector3 multiplier)
        {
            return new Vector3(
                vec.x * multiplier.x,
                vec.y * multiplier.y,
                vec.z * multiplier.z);
        }

        /// <summary>
        /// Memberwise vector sacle
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="multiplier"></param>
        /// <returns></returns>
        public static Vector2 ScaleMultiplier(this Vector2 vec, Vector2 multiplier)
        {
            return new Vector2(
                vec.x * multiplier.x,
                vec.y * multiplier.y);
        }

        /// <summary>
        /// Checks if num is within [min, max]
        /// </summary>
        /// <param name="num"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool IsInRange(this float num, float min, float max)
        {
            return num >= min && num <= max;
        }

        /// <summary>
        /// Checks if num is within [min, max]
        /// </summary>
        /// <param name="num"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool IsInRange(this int num, float min, float max)
        {
            return num >= min && num <= max;
        }
        #endregion


    }

}