using UnityEngine;
using System;

namespace LDFW.Tween
{

    public abstract class LDFWTweenBaseTwo : LDFWTweenBase
    {
        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="fromValue"></param>
        /// <param name="toValue"></param>
        /// <param name="duration"></param>
        /// <param name="startDelay"></param>
        /// <param name="autoStart"></param>
        /// <param name="endAction"></param>
        /// <param name="autoDestroyComponent"></param>
        /// <param name="autoDestroyGameObject"></param>
        /// <returns></returns>
        public LDFWTweenBase Init(Vector2 fromValue, Vector2 toValue, float duration, float startDelay, bool autoStart = false,
            Action endAction = null, bool autoDestroyComponent = false, bool autoDestroyGameObject = false)
        {
            return Init(new float[] { fromValue.x, fromValue.y }, new float[] { toValue.x, toValue.y },
                duration, startDelay, autoStart, endAction, autoDestroyComponent, autoDestroyGameObject);
        }
    }

}