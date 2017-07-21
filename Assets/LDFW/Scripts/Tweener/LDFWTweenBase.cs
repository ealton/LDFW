using UnityEngine;
using System;

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
            EasyStart, EasyEnd,
            EasyStartEasyEnd, HardStartHardEnd,
            BounceEnd4, BounceEnd2,
        }

        // Public Variables
        public Transform                                targetTransform = null;

        // Optional Controllers
        public bool                                     autoPlay = false;
        public bool                                     useCurrentValueAsStartingValue = false;
        public bool                                     useRelativeValueBasedOnStartingValue = false;
        public bool                                     generateRandomCurveBasedOnFromAndTo = false;
        public bool                                     ignoreTimeScale = true;
        public bool                                     autoDestroyComponent = false;
        public bool                                     autoDestroyGameObject = false;

        public float                                    startDelay = 0f;
        public float                                    duration = 1f;
        public float                                    targetTimeScale = 1;

        public TweenStyle                               tweenStyle = TweenStyle.Once;
        public CurveStyle                               curveStyle = CurveStyle.Normal;


        [HideInInspector] public float[]                fromValue;
        [HideInInspector] public float[]                toValue;
        public AnimationCurve[]                         curveList;
        

        public Action                                   endAction;

        // Private and protected variables
        protected float[]                               startingValue;
        protected float[]                               currentValue;
        protected float[]                               diffValue;
        protected float                                 accumulatedTime = 0f;
        protected bool                                  isPlayingReverse = false;
        protected bool                                  isCurrentAnimationBackwards = false;
        protected bool                                  isTweenerPlaying = false;
        protected int                                   curveCount = 0;
        protected int                                   burstFrameCount = 0;
        protected float                                 burstTime = 0;





        #region Initialization
        /// <summary>
        /// Used to set the current values if any, it runs first in Start method
        /// </summary>
        protected abstract void PreStart();

        /// <summary>
        /// Start method
        /// </summary>
        protected void Start()
        {
            if (targetTransform == null)
                targetTransform = transform;
            
            PreStart();

            // Reinitialize fromValue and toValue if they're not setup properly
            if (fromValue == null || fromValue.Length < curveCount || toValue == null || toValue.Length < curveCount)
            {
                fromValue = new float[curveCount];
                toValue = new float[curveCount];
            }

            // Init function
            Init();

            // Start tweener if autoPlay flag is true
            if (autoPlay)
                isTweenerPlaying = true;
        }

        /// <summary>
        /// Initialization
        /// </summary>
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

        }
        #endregion



        #region PublicAPI
        /// <summary>
        /// Public API - Re-initialize a tweener
        /// </summary>
        /// <param name="fromValue"></param>
        /// <param name="toValue"></param>
        /// <param name="duration"></param>
        /// <param name="startDelay"></param>
        /// <param name="autoStart"></param>
        /// <param name="endAction"></param>
        /// <param name="autoDestroyComponent"></param>
        /// <param name="autoDestroyGameObject"></param>
        /// <returns></returns>
        public LDFWTweenBase Init(float[] fromValue, float[] toValue, float duration, float startDelay, bool autoStart = false,
            Action endAction = null, bool autoDestroyComponent = false, bool autoDestroyGameObject = false)
        {

            if (fromValue.Length != toValue.Length)
            {
                Debug.LogError("Invalid initialization, from value and to value have different dimensions");
                return null;
            }

            // Assign curveCount
            curveCount = fromValue.Length;

            // Generate fromValue and toValue arrays
            this.fromValue = new float[curveCount];
            this.toValue = new float[curveCount];
            for (int i = 0; i < curveCount; i++)
            {
                this.fromValue[i] = fromValue[i];
                this.toValue[i] = toValue[i];
            }

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

        /// <summary>
        /// Set tweener style
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        public LDFWTweenBase SetTweenerStyle(TweenStyle style)
        {
            this.tweenStyle = style;
            return this;
        }

        /// <summary>
        /// Set curve style
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        public LDFWTweenBase SetCurveStyle(CurveStyle style)
        {
            this.curveStyle = style;
            if (!generateRandomCurveBasedOnFromAndTo)
            {
                for (int i = 0; i < curveCount; i++)
                    curveList[i] = GenerateAnimationCurve(curveStyle);
            }

            return this;
        }
        #endregion



        #region MainCalculation
        /// <summary>
        /// Update method
        /// </summary>
        protected void Update()
        {
            UpdateCurrentValue();
        }

        /// <summary>
        /// Update current value
        /// </summary>
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

                        if (this.autoDestroyComponent)
                        {
                            Destroy(this);
                            return;
                        }

                        if (this.autoDestroyGameObject)
                        {
                            gameObject.SetActive(false);
                            Destroy(gameObject);
                            return;
                        }

                        if (endAction != null)
                        {
                            endAction();
                        }
                    }
                }

                float timeScale = targetTimeScale;
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
        
        /// <summary>
        /// Pre current value calculation, runs right before current value calculation
        /// </summary>
        protected virtual void PreCurrentValueCalculation()
        {
        }
        
        /// <summary>
        /// Post current value calculation, runs right after current value calculation
        /// </summary>
        protected virtual void PostCurrentValueCalculation()
        {
        }
        #endregion



        #region AnimationCurve
        /// <summary>
        /// Generates a new animation curve
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get animation curve value
        /// </summary>
        /// <param name="curve"></param>
        /// <param name="diffValue"></param>
        /// <param name="fromValue"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Update animation curve
        /// </summary>
        /// <param name="curveIndex"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public LDFWTweenBase UpdateAnimationCurve(int curveIndex, CurveStyle style) 
        {
            if (curveIndex < curveList.Length)
                curveList[curveIndex] = GenerateAnimationCurve(style);
            
            return this;
        }
                
        #endregion



        #region FlowControl
        /// <summary>
        /// Set to beginning
        /// </summary>
        /// <returns></returns>
        public LDFWTweenBase SetToBeginning()
        {
            accumulatedTime = startDelay;
            return this;
        }

        /// <summary>
        /// Set to end
        /// </summary>
        /// <returns></returns>
        public LDFWTweenBase SetToEndding()
        {
            accumulatedTime = startDelay + duration;
            return this;
        }

        /// <summary>
        /// Set to middle based on percentage of completion
        /// </summary>
        /// <param name="percent"></param>
        /// <returns></returns>
        public LDFWTweenBase SetToPercentagePoint(float percent)
        {
            accumulatedTime = startDelay + duration * percent;
            return this;
        }

        /// <summary>
        /// Get current percentage
        /// </summary>
        /// <returns></returns>
        public float GetCurrentPercentage()
        {
            return Mathf.Clamp((accumulatedTime - startDelay) / duration, 0f, 1f);
        }

        /// <summary>
        /// Pause tweener
        /// </summary>
        /// <returns></returns>
        public LDFWTweenBase PauseTweener()
        {
            isTweenerPlaying = false;
            return this;
        }

        /// <summary>
        /// Resume tweener
        /// </summary>
        /// <returns></returns>
        public LDFWTweenBase ResumeTweener()
        {
            isTweenerPlaying = true;
            return this;
        }
        
        /// <summary>
        /// Reset tweener
        /// </summary>
        /// <returns></returns>
        public LDFWTweenBase ResetTweener()
        {
            accumulatedTime = 0f;
            return this;
        }

        /// <summary>
        /// Play tweener
        /// </summary>
        /// <returns></returns>
        public LDFWTweenBase Play()
        {
            Init();
            SetToBeginning();
            isPlayingReverse = false;
            isTweenerPlaying = true;
            return this;
        }

        /// <summary>
        /// Play reverse
        /// </summary>
        /// <returns></returns>
        public LDFWTweenBase PlayReverse()
        {
            Init();
            SetToBeginning();
            isPlayingReverse = true;
            isTweenerPlaying = true;
            return this;
        }

        /// <summary>
        /// Burst tweener based on number of frames
        /// </summary>
        /// <param name="framesNum"></param>
        /// <returns></returns>
        public LDFWTweenBase BurstTweenBasedOnFrames(int framesNum)
        {
            burstFrameCount = framesNum;
            return this;
        }

        /// <summary>
        /// Burst tweener based on time
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public LDFWTweenBase BurstTweenBasedOnTime(float time)
        {
            burstTime = time;
            return this;
        }
        #endregion

        

        #region OtherFunctions
        /// <summary>
        /// Generate a random curve
        /// </summary>
        /// <param name="curve"></param>
        /// <param name="slices"></param>
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