using UnityEngine;
using System;

namespace LDFW.Tween
{

    /// <summary>
    /// Initialization
    /// </summary>
    public abstract class LDFWTweenBaseFour : LDFWTweenBase
    {

        public LDFWTweenBase Init(Vector4 fromValue, Vector4 toValue, float duration, float startDelay, bool autoStart = false,
            Action endAction = null, bool autoDestroyComponent = false, bool autoDestroyGameObject = false)
        {
            return Init(new float[] { fromValue.x, fromValue.y, fromValue.z, fromValue.w }, new float[] { toValue.x, toValue.y, toValue.z, toValue.w },
                duration, startDelay, autoStart, endAction, autoDestroyComponent, autoDestroyGameObject);
        }
        

    }

}