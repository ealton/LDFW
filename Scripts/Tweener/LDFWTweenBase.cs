using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;

using Random = UnityEngine.Random;

namespace LDFW.Tween
{
    public abstract class LDFWTweenBase : MonoBehaviour
    {

        public enum TweenStyle
        {
            Once,
            Loop,
            PingPong,
        }

        public enum CurveStyle
        {
            Normal,
            EasyStart,
            EasyEnd,
            EasyStartEasyEnd,
            HardStartHardEnd,
            BounceEnd4,
            BounceEnd2,
        }

        // Public Variables
        public Transform targetTransform = null;

        public bool autoPlay = false;
        public bool useCurrentValueAsStartingValue = false;
        public bool useRelativeValueBasedOnStartingValue = false;
        public bool generateRandomCurveBasedOnFromAndTo = false;
        public bool ignoreTimeScale = true;
        public bool autoDestroyComponent = false;
        public bool autoDestroyGameObject = false;

        public float startDelay = 0f;
        public float duration = 1f;

        public TweenStyle tweenStyle = TweenStyle.Once;
        public CurveStyle curveStyle = CurveStyle.Normal;


        public float[] fromValue;
        public float[] toValue;
        public AnimationCurve[] curveList;
        

        public Action endAction;

        // Private and protected variables
        protected float[] startingValue;
        protected float[] currentValue;
        protected float[] diffValue;
        protected float accumulatedTime = 0f;
        protected bool isPlayingReverse = false;
        protected bool isCurrentAnimationBackwards = false;
        protected bool isTweenerPlaying = false;
        protected int curveCount = 0;
        protected int burstFrameCount = 0;
        protected float burstTime = 0;

        protected IEnumerator burstTweenIEnumerator = null;




        #region Initialization
        // PreStart is used to set the current values if any, it runs first in Start () method
        protected abstract void PreStart();

        protected void Start()
        {
            if (targetTransform == null)
                targetTransform = transform;
            
            PreStart();

            if (fromValue == null || fromValue.Length < curveCount || toValue == null || toValue.Length < curveCount)
            {
                fromValue = new float[curveCount];
                toValue = new float[curveCount];
            }

            Init();

            if (autoPlay)
                isTweenerPlaying = true;
        }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="fromValue">From value.</param>
        /// <param name="toValue">To value.</param>
        /// <param name="duration">Duration.</param>
        /// <param name="startDelay">Start delay.</param>
        /// <param name="endAction">End action.</param>
        /// <param name="autoStart">If set to <c>true</c> auto start.</param>
        /// <param name="autoDestroyComponent">If set to <c>true</c> auto destroy component.</param>
        /// <param name="autoDestroyGameObject">If set to <c>true</c> auto destroy game object.</param>
        public LDFWTweenBase Init(float[] fromValue, float[] toValue, float duration, float startDelay, Action endAction = null, 
            bool autoStart = false, bool autoDestroyComponent = false, bool autoDestroyGameObject = false)
        {
            // Generate fromValue array
            this.fromValue = new float[fromValue.Length];
            for (int i = 0; i < fromValue.Length; i++)
                this.fromValue[i] = fromValue[i];

            // Generate toValue array
            this.toValue = new float[toValue.Length];
            for (int i = 0; i < toValue.Length; i++)
                this.toValue[i] = toValue[i];

            // Assign other variables
            this.accumulatedTime = 0f;
            this.duration = duration;
            this.startDelay = startDelay;
            this.autoDestroyComponent = autoDestroyComponent;
            this.autoDestroyGameObject = autoDestroyGameObject;
            this.endAction = endAction;

            Init();

            if (autoStart)
                isTweenerPlaying = true;

            return this;
        }

        protected void Init()
        {
            accumulatedTime = 0f;


            if (useRelativeValueBasedOnStartingValue)
            {
                for (int i = 0; i < curveCount; i++)
                {
                    fromValue[i] += startingValue[i];
                    toValue[i] += startingValue[i];
                }
            }

            currentValue = new float[curveCount];
            diffValue = new float[curveCount];
            curveList = new AnimationCurve[curveCount];

            for (int i = 0; i < curveCount; i++)
            {
                currentValue[i] = fromValue[i];
                diffValue[i] = toValue[i] - fromValue[i];
            }

            curveList = new AnimationCurve[curveCount];

            for (int i = 0; i < curveCount; i++)
            {
                curveList[i] = GenerateAnimationCurve(curveStyle);
                if (generateRandomCurveBasedOnFromAndTo)
                    GenerateRandomCurve(curveList[i]);
            }


            if (autoDestroyComponent)
            {
                this.endAction += () =>
                    {
                        Destroy(this);
                    };
            }

            if (autoDestroyGameObject)
            {
                this.endAction += () =>
                    {
                        Destroy(this.gameObject);
                    };
            }
                
        }
        #endregion


        #region MainCalculation
        protected void Update()
        {
            UpdateCurrentValue();
        }

        protected void UpdateCurrentValue()
        {

            // only processes if isTweenerPlaying is true
            if (!isTweenerPlaying && burstFrameCount <= 0 && burstTime <= 0)
                return;


            if (accumulatedTime >= startDelay)
            {
                // Pre currentValue calculation
                PreCurrentValueCalculation();

                // decrements accumualtedTime if it's greater than duration, meaning: next iteration has started
                if (accumulatedTime - startDelay > duration)
                {
                    accumulatedTime -= duration;
                    if (tweenStyle == TweenStyle.PingPong)
                    {
                        isCurrentAnimationBackwards = !isCurrentAnimationBackwards;
                    }
                    else if (tweenStyle == TweenStyle.Once)
                    {
                        accumulatedTime = startDelay + duration;
                        isTweenerPlaying = false;
                        if (endAction != null)
                        {
                            endAction();
                        }
                    }
                }

                float timeScale = 1;
                if (!ignoreTimeScale)
                    timeScale = Time.timeScale;
                
                for (int i = 0; i < curveCount; i++)
                    currentValue[i] = GetValueBasedOnAnimationCurve(curveList[i], diffValue[i], fromValue[i]);

                // Burst frame logic
                if (!isTweenerPlaying && burstFrameCount > 0)
                    burstFrameCount--;
                else if (!isTweenerPlaying && burstTime > 0)
                    burstTime -= Time.deltaTime;
                        
                // Post currentValue calculation
                PostCurrentValueCalculation();
            }

            // increments accumualtedTime;
            accumulatedTime += Time.deltaTime;
        }

        // runs right before currentValue has been calculated for this frame
        protected virtual void PreCurrentValueCalculation()
        {
        }

        // runs right after currentValue has been calculated for this frame
        protected virtual void PostCurrentValueCalculation()
        {
        }
        #endregion



        #region AnimationCurve
        protected virtual AnimationCurve GenerateAnimationCurve(CurveStyle style) {
            
            Keyframe[] keyFrames;
            switch (style)
            {
                case CurveStyle.Normal:
                    keyFrames = new Keyframe[] { new Keyframe(0, 0, 0, 1), new Keyframe(1, 1, 1, 0) };
                    break;
                case CurveStyle.EasyStart:
                    keyFrames = new Keyframe[] { new Keyframe(0, 0, 0, 0), new Keyframe(1, 1, 2, 0) };
                    break;
                case CurveStyle.EasyEnd:
                    keyFrames = new Keyframe[] { new Keyframe(0, 0, 0, 2), new Keyframe(1, 1, 0, 0) };
                    break;
                case CurveStyle.EasyStartEasyEnd:
                    keyFrames = new Keyframe[] { new Keyframe(0, 0, 0, 0), new Keyframe(1, 1, 0, 0) };
                    break;
                case CurveStyle.HardStartHardEnd:
                    keyFrames = new Keyframe[] { new Keyframe(0, 0, 0, 2), new Keyframe(1, 1, 2, 0) };
                    break;
                case CurveStyle.BounceEnd2:
                    keyFrames = new Keyframe[] { new Keyframe(0, 0), new Keyframe(0.5f, 1), new Keyframe(0.75f, 0.5f), new Keyframe(1, 1) };
                    break;
                case CurveStyle.BounceEnd4:
                    keyFrames = new Keyframe[] { new Keyframe(0, 0), new Keyframe(0.25f, 1), new Keyframe(0.375f, 0.5f), new Keyframe(0.5f, 1), new Keyframe(0.625f, 0.75f), new Keyframe(0.75f, 1), new Keyframe(0.875f, 0.875f), new Keyframe(1, 1) };
                    break;
                default:
                    keyFrames = new Keyframe[] { new Keyframe(0, 0), new Keyframe(1, 1) };
                    break;
            }

            return new AnimationCurve(keyFrames);
        }

        protected virtual float GetValueBasedOnAnimationCurve(AnimationCurve curve, float diffValue, float fromValue)
        {
            if (isCurrentAnimationBackwards)
            {
                if (isPlayingReverse)
                    return curve.Evaluate((accumulatedTime - startDelay) / duration) * diffValue + fromValue;
                else
                    return curve.Evaluate((duration - (accumulatedTime - startDelay)) / duration) * diffValue + fromValue;
            }
            else
            {
                if (isPlayingReverse)
                    return curve.Evaluate((duration - (accumulatedTime - startDelay)) / duration) * diffValue + fromValue;
                else
                    return curve.Evaluate((accumulatedTime - startDelay) / duration) * diffValue + fromValue;
            }
        }

        public LDFWTweenBase UpdateAnimationCurve(int curveIndex, CurveStyle style) 
        {
            if (curveIndex < curveList.Length)
                curveList[curveIndex] = GenerateAnimationCurve(style);
            
            return this;
        }
                
        #endregion



        #region FlowControl
        public LDFWTweenBase SetToBeginning()
        {
            accumulatedTime = startDelay;
            return this;
        }

        public LDFWTweenBase SetToEndding()
        {
            accumulatedTime = startDelay + duration;
            return this;
        }

        public LDFWTweenBase SetToPercentagePoint(float percent)
        {
            accumulatedTime = startDelay + duration * percent;
            return this;
        }

        public LDFWTweenBase PauseTweener()
        {
            isTweenerPlaying = false;
            return this;
        }

        public LDFWTweenBase ResumeTweener()
        {
            isTweenerPlaying = true;
            return this;
        }

        public LDFWTweenBase ResetTweener()
        {
            accumulatedTime = 0f;
            return this;
        }

        public LDFWTweenBase Play()
        {
            Init();
            SetToBeginning();
            isPlayingReverse = false;
            isTweenerPlaying = true;
            return this;
        }

        public LDFWTweenBase PlayReverse()
        {
            Init();
            SetToBeginning();
            isPlayingReverse = true;
            isTweenerPlaying = true;
            return this;
        }

        public LDFWTweenBase BurstTweenBasedOnFrames(int framesNum)
        {
            burstFrameCount = framesNum;
            return this;
        }
        #endregion



        #region OtherFunctions
        protected void GenerateRandomCurve(AnimationCurve curve, float slices = 10f)
        {
            while (curve.keys.Length > 0)
                curve.RemoveKey(0);


            for (int i = 0; i <= slices; i++)
                curve.AddKey(i / slices, Random.Range(0f, 1f));

        }
        #endregion
    }
}