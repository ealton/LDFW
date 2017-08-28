using UnityEngine;
using System;

namespace LDFW.Tween
{

    public abstract class LDFWTweenBaseTwo : LDFWTweenBase
    {

        public Vector2 fromValueVec;
        public Vector2 toValueVec;

        protected override void PreStart()
        {
            curveCount = 2;
            fromValue = new float[curveCount];
            fromValue[0] = fromValueVec.x;
            fromValue[1] = fromValueVec.y;

            toValue = new float[curveCount];
            toValue[0] = toValueVec.x;
            toValue[1] = toValueVec.y;

        }


        public LDFWTweenBase Init(Vector2 fromValue, Vector2 toValue, float duration, float startDelay, bool autoStart = false,
            Action endAction = null, bool autoDestroyComponent = false, bool autoDestroyGameObject = false)
        {
            fromValueVec = fromValue;
            toValueVec = toValue;
            
            return Init(new float[] { fromValue.x, fromValue.y }, new float[] { toValue.x, toValue.y },
                duration, startDelay, autoStart, endAction, autoDestroyComponent, autoDestroyGameObject);
        }
    }

}