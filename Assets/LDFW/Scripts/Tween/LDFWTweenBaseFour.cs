using UnityEngine;
using System;

namespace LDFW.Tween
{

    /// <summary>
    /// Initialization
    /// </summary>
    public abstract class LDFWTweenBaseFour : LDFWTweenBase
    {
        public Vector4 fromValueVec;
        public Vector4 toValueVec;

        protected override void PreStart()
        {
            curveCount = 4;
            fromValue = new float[curveCount];
            fromValue[0] = fromValueVec.x;
            fromValue[1] = fromValueVec.y;
            fromValue[2] = fromValueVec.z;
            fromValue[3] = fromValueVec.w;

            toValue = new float[curveCount];
            toValue[0] = toValueVec.x;
            toValue[1] = toValueVec.y;
            toValue[2] = toValueVec.z;
            toValue[3] = toValueVec.w;

        }

        public LDFWTweenBase Init(Vector4 fromValue, Vector4 toValue, float duration, float startDelay, bool autoStart = false,
            Action endAction = null, bool autoDestroyComponent = false, bool autoDestroyGameObject = false)
        {
            return Init(new float[] { fromValue.x, fromValue.y, fromValue.z, fromValue.w }, new float[] { toValue.x, toValue.y, toValue.z, toValue.w },
                duration, startDelay, autoStart, endAction, autoDestroyComponent, autoDestroyGameObject);
        }
        

    }

}