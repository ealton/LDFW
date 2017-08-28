using UnityEngine;
using System;

namespace LDFW.Tween
{

    public abstract class LDFWTweenBaseThree : LDFWTweenBase {

        public Vector3 fromValueVec;
        public Vector3 toValueVec;

        protected override void PreStart()
        {
            curveCount = 3;
            fromValue = new float[curveCount];
            fromValue[0] = fromValueVec.x;
            fromValue[1] = fromValueVec.y;
            fromValue[2] = fromValueVec.z;

            toValue = new float[curveCount];
            toValue[0] = toValueVec.x;
            toValue[1] = toValueVec.y;
            toValue[2] = toValueVec.z;
            
        }

        public LDFWTweenBase Init(Vector3 fromValue, Vector3 toValue, float duration, float startDelay, bool autoStart = false,
            Action endAction = null, bool autoDestroyComponent = false, bool autoDestroyGameObject = false)
        {
            return Init(new float[] { fromValue.x, fromValue.y, fromValue.z }, new float[] { toValue.x, toValue.y, toValue.z },
                duration, startDelay, autoStart, endAction, autoDestroyComponent, autoDestroyGameObject);
        }

    }

}