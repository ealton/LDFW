using System;

namespace LDFW.Tween
{

    /// <summary>
    /// Initialization
    /// </summary>
    public abstract class LDFWTweenBaseOne : LDFWTweenBase
    {
        public LDFWTweenBase Init(float fromValue, float toValue, float duration, float startDelay, bool autoStart = false,
            Action endAction = null, bool autoDestroyComponent = false, bool autoDestroyGameObject = false)
        {
            return Init(new float[] { fromValue }, new float[] { toValue },
                duration, startDelay, autoStart, endAction, autoDestroyComponent, autoDestroyGameObject);
        }

    }

}