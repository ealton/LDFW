using UnityEngine;
using System.Collections;

namespace LDFW.Tween
{

    public static partial class LDFWTweenExtensions
    {


        public static LDFWTweenBase TweenToPosition(this Transform trans, Vector3 pos, float duration, float delay = 0, bool isLocal = true)
        {
            LDFWTweenBase tween = null;
            if (isLocal)
            {
                tween = InitTween(trans, new LDFWTweenPosition(), trans.localPosition, pos, duration, delay);
            }
            else
            {
                tween = InitTween(trans, new LDFWTweenWorldPosition(), trans.position, pos, duration, delay);
            }

            LDFWTweenManager.Instance.tweenUpdater.AddTween(tween);
            return tween;
        }

        public static LDFWTweenBase TweenToScale(this Transform trans, Vector3 scale, float duration, float delay = 0, bool isLocal = true)
        {
            LDFWTweenBase tween = null;
            if (isLocal)
            {
                tween = InitTween(trans, new LDFWTweenScale(), trans.localScale, scale, duration, delay);
            }
            else
            {
                tween = InitTween(trans, new LDFWTweenScale(), trans.localScale, scale, duration, delay);
            }

            LDFWTweenManager.Instance.tweenUpdater.AddTween(tween);
            return tween;
        }

        public static LDFWTweenBase TweenToEulerAngles(this Transform trans, Vector3 eulerAngles, float duration, float delay = 0, bool isLocal = true)
        {
            LDFWTweenBase tween = null;
            if (isLocal)
            {
                tween = InitTween(trans, new LDFWTweenRotation(), trans.localEulerAngles, eulerAngles, duration, delay);
            }
            else
            {
                tween = InitTween(trans, new LDFWTweenWorldRotation(), trans.eulerAngles, eulerAngles, duration, delay);
            }

            LDFWTweenManager.Instance.tweenUpdater.AddTween(tween);
            return tween;
        }

        public static LDFWTweenBase TweenFromPosition(this Transform trans, Vector3 pos, float duration, float delay = 0, bool isLocal = true)
        {
            LDFWTweenBase tween = null;
            if (isLocal)
            {
                tween = InitTween(trans, new LDFWTweenPosition(), pos, trans.localPosition, duration, delay);
            }
            else
            {
                tween = InitTween(trans, new LDFWTweenWorldPosition(), pos, trans.position, duration, delay);
            }

            LDFWTweenManager.Instance.tweenUpdater.AddTween(tween);
            return tween;
        }

        public static LDFWTweenBase TweenFromScale(this Transform trans, Vector3 scale, float duration, float delay = 0, bool isLocal = true)
        {
            LDFWTweenBase tween = null;
            if (isLocal)
            {
                tween = InitTween(trans, new LDFWTweenScale(), scale, trans.localScale, duration, delay);
            }
            else
            {
                tween = InitTween(trans, new LDFWTweenScale(), scale, trans.localScale, duration, delay);
            }

            LDFWTweenManager.Instance.tweenUpdater.AddTween(tween);
            return tween;
        }

        public static LDFWTweenBase TweenFromEulerAngles(this Transform trans, Vector3 eulerAngles, float duration, float delay = 0, bool isLocal = true)
        {
            LDFWTweenBase tween = null;
            if (isLocal)
            {
                tween = InitTween(trans, new LDFWTweenRotation(), eulerAngles, trans.localEulerAngles, duration, delay);
            }
            else
            {
                tween = InitTween(trans, new LDFWTweenWorldRotation(), eulerAngles, trans.eulerAngles, duration, delay);
            }

            LDFWTweenManager.Instance.tweenUpdater.AddTween(tween);
            return tween;
        }





        private static LDFWTweenBase InitTween(Object target, LDFWTweenBase tween, Vector3 fromVal, Vector3 toVal, float duration, float delay)
        {
            return InitTween(target, tween, fromVal.ToFloatArray(), toVal.ToFloatArray(), duration, delay);
        }

        private static LDFWTweenBase InitTween(Object target, LDFWTweenBase tween, float[] fromVal, float[] toVal, float duration, float delay)
        {
            tween
                .Init(fromVal, toVal, duration, delay)
                .SetTarget(target)
                .Play();

            return tween;
        }

        private static float[] ToFloatArray(this Vector4 target)
        {
            return new float[4] { target.x, target.y, target.z, target.w };
        }

        private static float[] ToFloatArray(this Vector3 target)
        {
            return new float[3] { target.x, target.y, target.z };
        }

        private static float[] ToFloatArray(this Vector2 target)
        {
            return new float[2] { target.x, target.y };
        }

        private static float[] ToFloatArray(this float target)
        {
            return new float[1] { target };
        }

    }

}
