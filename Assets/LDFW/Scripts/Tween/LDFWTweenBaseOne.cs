using UnityEngine;
using System;

namespace LDFW.Tween
{

    public abstract class LDFWTweenBaseOne : LDFWTweenBase
    {

        public float fromValueFloat;
        public float toValueFloat;

        protected override void PreStart()
        {
            curveCount = 1;
            fromValue = new float[curveCount];
            fromValue[0] = fromValueFloat;

            toValue = new float[curveCount];
            toValue[0] = toValueFloat;
        }

        public LDFWTweenBase Init(float fromValue, float toValue, float duration, float startDelay, bool autoStart = false,
            Action endAction = null, bool autoDestroyComponent = false, bool autoDestroyGameObject = false)
        {
            fromValueFloat = fromValue;
            toValueFloat = toValue;

            return Init(new float[] { fromValue }, new float[] { toValue },
                duration, startDelay, autoStart, endAction, autoDestroyComponent, autoDestroyGameObject);
        }

    }

}