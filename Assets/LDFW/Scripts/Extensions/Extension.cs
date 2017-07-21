using UnityEngine;

using System;
using System.Collections;

namespace LDFW.Extensions
{

    public static class CommonExtensions
    {

        #region Transform
        /// <summary>
        /// Sets trans' parent
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="parent"></param>
        /// <param name="localPosition"></param>
        /// <param name="localScale"></param>
        /// <param name="localEulerAngles"></param>
        /// <param name="name"></param>
        public static void SetParent(this Transform trans, Transform parent, Vector3 localPosition, Vector3 localScale, Vector3 localEulerAngles, string name = "")
        {
            trans.parent = parent;
            trans.localPosition = localPosition;
            trans.localScale = localScale;
            trans.localEulerAngles = localEulerAngles;
            if (!string.IsNullOrEmpty(name))
                trans.name = name;
        }

        /// <summary>
        /// Destroys all children
        /// </summary>
        /// <param name="trans"></param>
        public static void DestroyAllChildren(this Transform trans)
        {
            Transform temp = null;
            while (trans.childCount > 0)
            {
                temp = trans.GetChild(0);
                temp.parent = null;
                MonoBehaviour.Destroy(temp.gameObject);
            }
        }
        #endregion


        /// <summary>
        /// Converts to precise string
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public static string ToPreciseString(this Vector2 vec)
        {
            return "{" + vec.x.ToString("0.0000") + ", " + vec.y.ToString("0.0000") + "}";
        }

        /// <summary>
        /// Converts to precise string
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public static string ToPreciseString(this Vector3 vec)
        {
            return "{" + vec.x.ToString("0.0000") + ", " + vec.y.ToString("0.0000") + ", " + vec.z.ToString("0.0000") + "}";
        }

        /// <summary>
        /// Converts to precise string
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public static string ToPreciseString(this Vector4 vec)
        {
            return "{" + vec.x.ToString("0.0000") + ", " + vec.y.ToString("0.0000") + ", " + vec.z.ToString("0.0000") + ", " + vec.w.ToString("0.0000") + "}";
        }


    }

}