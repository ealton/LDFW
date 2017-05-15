using UnityEngine;
using System;

namespace LDFW.Tween
{

    public abstract class LDFWTweenBaseTwo : LDFWTweenBase
    {
        public LDFWTweenBase Init(Vector2 fromValue, Vector2 toValue, float duration, float startDelay, bool autoStart = false,
            Action endAction = null, bool autoDestroyComponent = false, bool autoDestroyGameObject = false)
        {
            return Init(new float[] { fromValue.x, fromValue.y }, new float[] { toValue.x, toValue.y },
                duration, startDelay, autoStart, endAction, autoDestroyComponent, autoDestroyGameObject);
        }
    }

}