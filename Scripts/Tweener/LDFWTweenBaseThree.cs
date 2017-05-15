using UnityEngine;
using System;

namespace LDFW.Tween
{

    public abstract class LDFWTweenBaseThree : LDFWTweenBase {

        public LDFWTweenBase Init(Vector3 fromValue, Vector3 toValue, float duration, float startDelay, bool autoStart = false,
            Action endAction = null, bool autoDestroyComponent = false, bool autoDestroyGameObject = false)
        {
            return Init(new float[] { fromValue.x, fromValue.y, fromValue.z }, new float[] { toValue.x, toValue.y, toValue.z },
                duration, startDelay, autoStart, endAction, autoDestroyComponent, autoDestroyGameObject);
        }

    }

}