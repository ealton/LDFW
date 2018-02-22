using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LDFW.Tween
{

    public static partial class LDFWTweenExtensions
    {

        #region FlowController
        

        #endregion



        #region AnimationCUrves

        public static LDFWTweenBase SetAnimationCurves(LDFWTweenBase tween, LDFWTweenAnimationCurve tweenCurve)
        {
            if (tween == null)
                return tween;

            tween.SetCurveStyle(CurveStyle.AnimationCurve);

            if (tweenCurve.isUniversal)
            {
                tween.SetAnimationCurve(tweenCurve.curves[0]);
            }
            else
            {
                for (int i = 0; i < tweenCurve.curves.Length; i++)
                {
                    tween.SetAnimationCurve(tweenCurve.curves[i], i);
                }
            }

            return tween;
        }

        #endregion



        #region Camera

        public static LDFWTweenBase TweenToCamera(
            this Camera targetCamera, Camera toCamera, float duration, float delay,
            bool fov, bool clipPlanes, bool position, bool eulerAngles, bool scale, bool orthographicSize
        )
        {
            var tween = InitTween(
                targetCamera,
                new LDFWTweenCamera().SetTweenFlags(fov, clipPlanes, position, eulerAngles, scale, orthographicSize),
                LDFWTweenCamera.EncodeCameraData(targetCamera),
                LDFWTweenCamera.EncodeCameraData(toCamera),
                duration, delay);

            LDFWTweenManager.Instance.tweenUpdater.AddTween(tween);
            return tween;
        }

        public static LDFWTweenBase TweenToCameraAll(
            this Camera targetCamera, Camera toCamera, float duration, float delay)
        {
            return targetCamera.TweenToCamera(toCamera, duration, delay, true, true, true, true, true, true);
        }

        public static LDFWTweenBase TweenToCameraFOV(
            this Camera targetCamera, Camera toCamera, float duration, float delay)
        {
            return targetCamera.TweenToCamera(toCamera, duration, delay, true, false, false, false, false, false);
        }

        public static LDFWTweenBase TweenToCameraOrghographicSize(
            this Camera targetCamera, Camera toCamera, float duration, float delay)
        {
            return targetCamera.TweenToCamera(toCamera, duration, delay, false, false, false, false, false, true);
        }

        public static LDFWTweenBase TweenToCameraPosition(
            this Camera targetCamera, Camera toCamera, float duration, float delay,
            bool position, bool eulerAngles)
        {
            return targetCamera.TweenToCamera(toCamera, duration, delay, false, false, position, eulerAngles, false, true);
        }

        public static LDFWTweenBase TweenFromCamera(
            this Camera targetCamera, Camera fromCamera, float duration, float delay,
            bool fov, bool clipPlanes, bool position, bool eulerAngles, bool scale, bool orthographicSize
        )
        {
            var tween = InitTween(
                targetCamera,
                new LDFWTweenCamera().SetTweenFlags(fov, clipPlanes, position, eulerAngles, scale, orthographicSize),
                LDFWTweenCamera.EncodeCameraData(fromCamera),
                LDFWTweenCamera.EncodeCameraData(targetCamera),
                duration, delay);

            LDFWTweenManager.Instance.tweenUpdater.AddTween(tween);
            return tween;
        }

        public static LDFWTweenBase TweenFromCameraAll(
            this Camera targetCamera, Camera fromCamera, float duration, float delay)
        {
            return targetCamera.TweenToCamera(fromCamera, duration, delay, true, true, true, true, true, true);
        }

        public static LDFWTweenBase TweenFromCameraFOV(
            this Camera targetCamera, Camera fromCamera, float duration, float delay)
        {
            return targetCamera.TweenToCamera(fromCamera, duration, delay, true, false, false, false, false, false);
        }

        public static LDFWTweenBase TweenFromCameraOrghographicSize(
            this Camera targetCamera, Camera fromCamera, float duration, float delay)
        {
            return targetCamera.TweenToCamera(fromCamera, duration, delay, false, false, false, false, false, true);
        }

        public static LDFWTweenBase TweenFromCameraPosition(
            this Camera targetCamera, Camera fromCamera, float duration, float delay,
            bool position, bool eulerAngles)
        {
            return targetCamera.TweenToCamera(fromCamera, duration, delay, false, false, position, eulerAngles, false, true);
        }


        #endregion



        #region Shader

        public static LDFWTweenBase TweenShaderInt(
            this Material mat, string paramName, int fromValue, int toValue, float duration, float delay)
        {
            var tween = new LDFWTweenShader().InitFloat(
                mat, paramName, fromValue, toValue, duration, delay);
            return tween;
        }

        public static LDFWTweenBase TweenShaderFloat(
            this Material mat, string paramName, float fromValue, float toValue, float duration, float delay)
        {
            var tween = new LDFWTweenShader().InitFloat(
                mat, paramName, fromValue, toValue, duration, delay);
            return tween;
        }

        public static LDFWTweenBase TweenShaderVector4(
            this Material mat, string paramName, Vector4 fromValue, Vector4 toValue, float duration, float delay)
        {
            var tween = new LDFWTweenShader().InitVector4(
                mat, paramName, fromValue, toValue, duration, delay);
            return tween;
        }

        public static LDFWTweenBase TweenShaderTextureUV(
            this Material mat, string paramName,
            Vector2 fromScale, Vector2 toScale, Vector2 fromOffset, Vector2 toOffset,
            float duration, float delay)
        {
            var tween = new LDFWTweenShader().InitTextureUV(
                mat, paramName, fromScale, toScale, fromOffset, toOffset, duration, delay);
            return tween;
        }


        #endregion



    }

}