using UnityEngine;
using System;

using Random = UnityEngine.Random;

namespace LDFW.Tween
{

    public abstract partial class LDFWTweenBase
    {

        public bool autoPlay = false;
        public bool ignoreTimeScale = false;
        public bool removeUponCompletion = false;
        public bool destroyTargetUponCompletion = false;



        public LDFWTweenBase Pause()
        {
            isTweenPlaying = false;
            return this;
        }

        public LDFWTweenBase Resume()
        {
            isTweenPlaying = true;
            return this;
        }

        public LDFWTweenBase Play()
        {
            tweenTime = -startDelay;
            isTweenPlaying = true;
            isTweenBackwards = false;
            return this;
        }

        public void RemoveTween()
        {
            if (removeUponCompletion)
            {
                Pause();
                LDFWTweenManager.Instance.tweenUpdater.RemoveTween(this);

                if (destroyTargetUponCompletion)
                    DestroyTarget();
            }
        }

        public void DestroyTarget()
        {
            if (destroyTargetUponCompletion)
                GameObject.Destroy(target);
        }

        public LDFWTweenBase GenerateRandomCurves(int sectionCount = 1)
        {
            if (curveStyle != CurveStyle.AnimationCurve || sectionCount <= 0)
                return this;

            var precision = 1f / (sectionCount + 1);
            for (int i = 0; i < paramCount; i++)
            {
                for (int j = 0; j < sectionCount; j++)
                {
                    var currentCurve = GetLinearAnimationCurve();
                    currentCurve.AddKey(
                        precision * j,
                        Random.Range(fromValue[i], toValue[i]));
                }
            }

            return this;
        }
    }


}
