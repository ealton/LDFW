using UnityEngine;
using System;

using Random = UnityEngine.Random;

namespace LDFW.Tween
{
    public abstract class LDFWTweenBase : MonoBehaviour
    {
        // Public Variables
        public Transform                                targetTransform = null;

        // Optional Controllers
        public bool                                     autoPlay = false;
        public bool                                     useCurrentValueAsStartingValue = false;
        public bool                                     useRelativeValueBasedOnStartingValue = false;
        public bool                                     generateRandomCurveBasedOnFromAndTo = false;
        public bool                                     ignoreTimeScale = false;
        public bool                                     autoDestroyComponent = false;
        public bool                                     autoDestroyGameObject = false;

        public float                                    startDelay = 0f;
        public float                                    duration = 1f;
        public float                                    targetTimeScale = 1;

        public TweenStyle                               tweenStyle = TweenStyle.Once;
        public CurveStyle                               curveStyle = CurveStyle.Default;


        public float[]                                  fromValue;
        public float[]                                  toValue;
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

        private float                                   realtimeSinceStartup = 0;





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
            realtimeSinceStartup = Time.realtimeSinceStartup;
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
            
            for (int i = 0; i < curveCount; i++)
            {
                currentValue[i] = fromValue[i];
                diffValue[i] = toValue[i] - fromValue[i];
            }

            if (curveStyle == CurveStyle.Custom && curveList == null)
            {
                curveList = new AnimationCurve[curveCount];
                for (int i = 0; i < curveCount; i++)
                {
                    curveList[i] = GenerateAnimationCurve(curveStyle);
                    if (generateRandomCurveBasedOnFromAndTo)
                        GenerateRandomCurve(curveList[i], 1);
                }
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

        /// <summary>
        /// Set curve
        /// </summary>
        /// <param name="keyFrames"></param>
        /// <returns></returns>
        public LDFWTweenBase SetCurve(Keyframe[] keyFrames)
        {
            for (int i = 0; i < curveCount; i++)
                curveList[i] = new AnimationCurve(keyFrames);

            return this;
        }

        /// <summary>
        /// Set curve
        /// </summary>
        /// <param name="keyFrames"></param>
        /// <param name="curveIndex"></param>
        /// <returns></returns>
        public LDFWTweenBase SetCurve(Keyframe[] keyFrames, int curveIndex)
        {
            if (curveIndex < curveList.Length)
                curveList[curveIndex] = new AnimationCurve(keyFrames);

            return this;
        }

        /// <summary>
        /// Set end action
        /// </summary>
        /// <param name="endAction"></param>
        /// <returns></returns>
        public LDFWTweenBase SetEndAction(Action endAction)
        {
            this.endAction = endAction;

            return this;
        }

        /// <summary>
        /// Add end action
        /// </summary>
        /// <param name="endAction"></param>
        /// <returns></returns>
        public LDFWTweenBase AppendEndAction(Action endAction)
        {
            this.endAction += endAction;

            return this;
        }

        /// <summary>
        /// Randomize animation curve
        /// </summary>
        /// <param name="steps"></param>
        /// <param name="multipliers"></param>
        /// <param name="isLine"></param>
        /// <returns></returns>
        public LDFWTweenBase RandomizeAnimationCurve(float steps, float[] multipliers, bool isLine = false)
        {
            float[] newMultipliers = null;
            if (multipliers.Length < curveCount)
            {
                newMultipliers = new float[curveCount];
                int i;
                for (i = 0; i < multipliers.Length; i++)
                    newMultipliers[i] = multipliers[i];

                for (; i < curveCount; i++)
                    newMultipliers[i] = 1;
            }

            for (int i = 0; i < curveCount; i++)
                GenerateRandomCurve(curveList[i], newMultipliers != null && newMultipliers.Length >= curveCount ? newMultipliers[i] : multipliers[i], steps, isLine);

            return this;
        }

        /// <summary>
        /// Randomize animation curve
        /// </summary>
        /// <param name="curveIndex"></param>
        /// <param name="steps"></param>
        /// <param name="multiplier"></param>
        /// <param name="isLine"></param>
        /// <returns></returns>
        public LDFWTweenBase RandomizeAnimationCurve(int curveIndex, float steps, float multiplier = 1, bool isLine = false)
        {
            if (curveIndex < curveList.Length)
                GenerateRandomCurve(curveList[curveIndex], multiplier, steps, isLine);

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
                    currentValue[i] = GetCurrentValue(i);

                // Burst frame logic
                if (!isTweenerPlaying && burstFrameCount > 0)
                    burstFrameCount--;
                else if (!isTweenerPlaying && burstTime > 0)
                    burstTime -= Time.deltaTime;

                // Post currentValue calculation
                PostCurrentValueCalculation();
            }

            // increments accumualtedTime;
            accumulatedTime += GetScaledDeltaTime();
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

        /// <summary>
        /// calls the appropriate math function based on curve style
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private float GetCurrentValue(int index)
        {
            switch (curveStyle)
            {
                case CurveStyle.InBack:
                    return InBack(fromValue[index], toValue[index], accumulatedTime, duration);
                case CurveStyle.OutBack:
                    return OutBack(fromValue[index], toValue[index], accumulatedTime, duration);
                case CurveStyle.InOutBack:
                    return InOutBack(fromValue[index], toValue[index], accumulatedTime, duration);
                case CurveStyle.OutInBack:
                    return OutInBack(fromValue[index], toValue[index], accumulatedTime, duration);
                case CurveStyle.InQuad:
                    return InQuad(fromValue[index], toValue[index], accumulatedTime, duration);
                case CurveStyle.OutQuad:
                    return OutQuad(fromValue[index], toValue[index], accumulatedTime, duration);
                case CurveStyle.InoutQuad:
                    return InoutQuad(fromValue[index], toValue[index], accumulatedTime, duration);
                case CurveStyle.InCubic:
                    return InCubic(fromValue[index], toValue[index], accumulatedTime, duration);
                case CurveStyle.OutCubic:
                    return OutCubic(fromValue[index], toValue[index], accumulatedTime, duration);
                case CurveStyle.InoutCubic:
                    return InoutCubic(fromValue[index], toValue[index], accumulatedTime, duration);
                case CurveStyle.InQuart:
                    return InQuart(fromValue[index], toValue[index], accumulatedTime, duration);
                case CurveStyle.OutQuart:
                    return OutQuart(fromValue[index], toValue[index], accumulatedTime, duration);
                case CurveStyle.InOutQuart:
                    return InOutQuart(fromValue[index], toValue[index], accumulatedTime, duration);
                case CurveStyle.OutInQuart:
                    return OutInQuart(fromValue[index], toValue[index], accumulatedTime, duration);
                case CurveStyle.InQuint:
                    return InQuint(fromValue[index], toValue[index], accumulatedTime, duration);
                case CurveStyle.OutQuint:
                    return OutQuint(fromValue[index], toValue[index], accumulatedTime, duration);
                case CurveStyle.InOutQuint:
                    return InOutQuint(fromValue[index], toValue[index], accumulatedTime, duration);
                case CurveStyle.OutInQuint:
                    return OutInQuint(fromValue[index], toValue[index], accumulatedTime, duration);
                case CurveStyle.InSine:
                    return InSine(fromValue[index], toValue[index], accumulatedTime, duration);
                case CurveStyle.OutSine:
                    return OutSine(fromValue[index], toValue[index], accumulatedTime, duration);
                case CurveStyle.InOutSine:
                    return InOutSine(fromValue[index], toValue[index], accumulatedTime, duration);
                case CurveStyle.OutInSine:
                    return OutInSine(fromValue[index], toValue[index], accumulatedTime, duration);
                case CurveStyle.InExpo:
                    return InExpo(fromValue[index], toValue[index], accumulatedTime, duration);
                case CurveStyle.OutExpo:
                    return OutExpo(fromValue[index], toValue[index], accumulatedTime, duration);
                case CurveStyle.InOutExpo:
                    return InOutExpo(fromValue[index], toValue[index], accumulatedTime, duration);
                case CurveStyle.OutInExpo:
                    return OutInExpo(fromValue[index], toValue[index], accumulatedTime, duration);
                case CurveStyle.InBounce:
                    return InBounce(fromValue[index], toValue[index], accumulatedTime, duration);
                case CurveStyle.OutBounce:
                    return OutBounce(fromValue[index], toValue[index], accumulatedTime, duration);
                case CurveStyle.InOutBounce:
                    return InOutBounce(fromValue[index], toValue[index], accumulatedTime, duration);
                case CurveStyle.OutInBounce:
                    return OutInBounce(fromValue[index], toValue[index], accumulatedTime, duration);
                case CurveStyle.Custom:
                    return GetValueBasedOnAnimationCurve(curveList[index], diffValue[index], fromValue[index]);


                case CurveStyle.Default:
                case CurveStyle.Linear:
                default:
                    if (isCurrentAnimationBackwards)
                        return Mathf.Lerp(fromValue[index], toValue[index], 1 - accumulatedTime / duration);
                    else
                        return Mathf.Lerp(fromValue[index], toValue[index], accumulatedTime / duration);
            }
        }

        #endregion



        #region AnimationCurve
        /// <summary>
        /// Generates a new animation curve
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        protected virtual AnimationCurve GenerateAnimationCurve(CurveStyle style)
        {

            Keyframe[] keyFrames;
            switch (style)
            {
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
        /// Play with a starting percentage
        /// </summary>
        /// <param name="startingPercentage"></param>
        /// <returns></returns>
        public LDFWTweenBase Play(float startingPercentage = 0)
        {
            startingPercentage = Mathf.Clamp(startingPercentage, 0, 1);
            SetToPercentagePoint(startingPercentage);
            isPlayingReverse = false;
            isTweenerPlaying = true;
            return this;
        }

        /// <summary>
        /// Play with delay
        /// </summary>
        /// <returns></returns>
        public LDFWTweenBase PlayWithDelay()
        {
            accumulatedTime = 0;
            isPlayingReverse = false;
            isTweenerPlaying = true;
            return this;
        }

        /// <summary>
        /// Play reverse with a starting percentage
        /// </summary>
        /// <param name="startingPercentage"></param>
        /// <returns></returns>
        public LDFWTweenBase PlayReverse(float startingPercentage = 1)
        {
            Play(startingPercentage);
            isPlayingReverse = true;
            return this;
        }

        /// <summary>
        /// Play reverse from end
        /// </summary>
        public void PlayReverseFromEnd()
        {
            PlayReverse(1);
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



        #region TweenStyleFunctions

        public float InBack(float b, float to, float t, float d)
        {
            float s = 1.70158f;
            float c = to - b;
            t = t / d;

            return c * t * t * ((s + 1) * t - s) + b;
        }

        public float OutBack(float b, float to, float t, float d, float s = 1.70158f)
        {
            float c = to - b;

            t = t / d - 1;

            return c * (t * t * ((s + 1) * t + s) + 1) + b;

        }

        public float InOutBack(float b, float to, float t, float d, float s = 1.70158f)
        {
            float c = to - b;
            s = s * 1.525f;
            t = t / d * 2;
            if (t < 1)
                return c / 2 * (t * t * ((s + 1) * t - s)) + b;
            else
            {
                t = t - 2;
                return c / 2 * (t * t * ((s + 1) * t + s) + 2) + b;
            }
        }

        public float OutInBack(float b, float to, float t, float d, float s = 1.70158f)
        {
            float c = to - b;
            if (t < d / 2)
            {
                t *= 2;
                c *= 0.5f;
                t = t / d * 2;

                t = t / d - 1;
                return c * (t * t * ((s + 1) * t + s) + 1) + b;
            }

            else
            {
                t = t * 2 - d;
                b += c * 0.5f;
                c *= 0.5f;


                if (t < 1)
                    return c / 2 * (t * t * ((s + 1) * t - s)) + b;
                else
                {
                    t = t - 2;
                    return c / 2 * (t * t * ((s + 1) * t + s) + 2) + b;
                }
            }

        }

        public float InQuad(float b, float to, float t, float d)
        {
            float c = to - b;
            t = t / d;
            return (float)(c * System.Math.Pow(t, 2) + b);
        }

        public float OutQuad(float b, float to, float t, float d)
        {
            float c = to - b;
            t = t / d;
            return (float)(-c * t * (t - 2) + b);
        }

        public float InoutQuad(float b, float to, float t, float d)
        {
            float c = to - b;
            t = t / d * 2;
            if (t < 1)
                return (float)(c / 2 * System.Math.Pow(t, 2) + b);
            else
                return -c / 2 * ((t - 1) * (t - 3) - 1) + b;

        }

        public float InCubic(float b, float to, float t, float d)
        {
            float c = to - b;
            t = t / d;
            return (float)(c * System.Math.Pow(t, 3) + b);

        }

        public float OutCubic(float b, float to, float t, float d)
        {
            float c = to - b;
            t = t / d - 1;
            return (float)(c * (System.Math.Pow(t, 3) + 1) + b);

        }

        public float InoutCubic(float b, float to, float t, float d)
        {
            float c = to - b;
            t = t / d * 2;
            if (t < 1)
                return c / 2 * t * t * t + b;
            else
            {
                t = t - 2;
                return c / 2 * (t * t * t + 2) + b;
            }
        }

        public float InQuart(float b, float to, float t, float d)
        {
            float c = to - b;
            t = t / d;
            return (float)(c * System.Math.Pow(t, 4) + b);

        }

        public float OutQuart(float b, float to, float t, float d)
        {
            float c = to - b;
            t = t / d - 1;
            return (float)(-c * (System.Math.Pow(t, 4) - 1) + b);

        }

        public float InOutQuart(float b, float to, float t, float d)
        {
            float c = to - b;
            t = t / d * 2;
            if (t < 1)
                return (float)(c / 2 * System.Math.Pow(t, 4) + b);
            else
            {
                t = t - 2;
                return (float)(-c / 2 * (System.Math.Pow(t, 4) - 2) + b);
            }

        }

        public float OutInQuart(float b, float to, float t, float d)
        {
            if (t < d / 2)
            {
                float c = to - b;
                t *= 2;
                c *= 0.5f;
                t = t / d - 1;

                return (float)(-c * (System.Math.Pow(t, 4) - 1) + b);
            }
            else
            {
                float c = to - b;
                t = t * 2 - d;
                b = b + c * 0.5f;
                c *= 0.5f;
                t = t / d;


                return (float)(c * System.Math.Pow(t, 4) + b);

            }
        }

        public float InQuint(float b, float to, float t, float d)
        {
            float c = to - b;
            t = t / d;
            return (float)(c * System.Math.Pow(t, 5) + b);

        }

        public float OutQuint(float b, float to, float t, float d)
        {
            float c = to - b;
            t = t / d - 1;
            return (float)(c * (System.Math.Pow(t, 5) + 1) + b);
        }

        public float InOutQuint(float b, float to, float t, float d)
        {
            float c = to - b;
            t = t / d * 2;
            if (t < 1)
                return (float)(c / 2 * System.Math.Pow(t, 5) + b);
            else
            {
                t = t - 2;
                return (float)(c / 2 * (System.Math.Pow(t, 5) + 2) + b);

            }

        }

        public float OutInQuint(float b, float to, float t, float d)
        {
            float c = to - b;
            if (t < d / 2)
            {
                t *= 2;
                c *= 0.5f;
                t = t / d - 1;
                return (float)(c * (System.Math.Pow(t, 5) + 1) + b);
            }
            else
            {
                t = t * 2 - d;
                b = b + c * 0.5f;
                c *= 0.5f;

                t = t / d;
                return (float)(c * System.Math.Pow(t, 5) + b);
            }
        }

        public float InSine(float b, float to, float t, float d)
        {
            float c = to - b;
            return (float)(-c * System.Math.Cos(t / d * (System.Math.PI / 2)) + c + b);

        }

        public float OutSine(float b, float to, float t, float d)
        {
            float c = to - b;
            return (float)(c * System.Math.Sin(t / d * (System.Math.PI / 2)) + b);
        }

        public float InOutSine(float b, float to, float t, float d)
        {
            float c = to - b;
            return (float)(-c / 2 * (System.Math.Cos(System.Math.PI * t / d) - 1) + b);

        }

        public float OutInSine(float b, float to, float t, float d)
        {
            float c = to - b;
            if (t < d / 2)
            {
                t *= 2;
                c *= 0.5f;
                return (float)(c * System.Math.Sin(t / d * (System.Math.PI / 2)) + b);
            }
            else
            {
                t = t * 2 - d;
                b += c * 0.5f;
                c *= 0.5f;
                return (float)(-c * System.Math.Cos(t / d * (System.Math.PI / 2)) + c + b);

            }
        }

        public float InExpo(float b, float to, float t, float d)
        {
            float c = to - b;
            if (t == 0)
                return b;
            else
                return (float)(c * System.Math.Pow(2, 10 * (t / d - 1)) + b - c * 0.001f);
        }

        public float OutExpo(float b, float to, float t, float d)
        {
            float c = to - b;
            if (t == d)
                return b + c;
            else
                return (float)(c * 1.001 * (-System.Math.Pow(2, -10 * t / d) + 1) + b);

        }

        public float InOutExpo(float b, float to, float t, float d)
        {
            float c = to - b;
            if (t == 0)
                return b;
            if (t == d)
                return (b + c);

            t = t / d * 2;

            if (t < 1)
                return (float)(c / 2 * System.Math.Pow(2, 10 * (t - 1)) + b - c * 0.0005f);
            else
            {
                t = t - 1;
                return (float)(c / 2 * 1.0005 * (-System.Math.Pow(2, -10 * t) + 2) + b);

            }
        }

        public float OutInExpo(float b, float to, float t, float d)
        {
            float c = to - b;
            if (t < d / 2)
            {
                t *= 2;
                c *= 0.5f;
                if (t == d)
                    return b + c;
                else
                    return (float)(c * 1.001 * (-System.Math.Pow(2, -10 * t / d) + 1) + b);
            }
            else
            {
                t = t * 2 - d;
                b += c * 0.5f;
                c *= 0.5f;
                if (t == 0)
                    return b;
                else
                    return (float)(c * System.Math.Pow(2, 10 * (t / d - 1)) + b - c * 0.001f);

            }
        }

        public float OutBounce(float b, float to, float t, float d)
        {
            float c = to - b;
            t = t / d;
            if (t < 1 / 2.75)
            {
                return c * (7.5625f * t * t) + b;
            }
            else if (t < 2 / 2.75)
            {
                t = t - (1.5f / 2.75f);

                return c * (7.5625f * t * t + 0.75f) + b;
            }
            else if (t < 2.5 / 2.75)
            {

                t = t - (2.25f / 2.75f);
                return c * (7.5625f * t * t + 0.9375f) + b;
            }
            else
            {
                t = t - (2.625f / 2.75f);
                return c * (7.5625f * t * t + 0.984375f) + b;
            }
        }

        public float InBounce(float b, float to, float t, float d)
        {
            float c = to - b;
            return c - OutBounce(0, to, d - t, d) + b;
        }

        public float InOutBounce(float b, float to, float t, float d)
        {
            float c = to - b;
            if (t < d / 2)
            {
                return InBounce(0, to, t * 2f, d) * 0.5f + b;
            }
            else
            {
                return OutBounce(0, to, t * 2f - d, d) * 0.5f + c * 0.5f + b;
            }
        }

        public float OutInBounce(float b, float to, float t, float d)
        {
            float c = to - b;
            if (t < d / 2)
            {
                return OutBounce(b, b + c / 2, t * 2, d);
            }
            else
            {
                return InBounce(b + c / 2, to, t * 2f - d, d);

            }
        }


        #endregion



        #region OtherFunctions
        /// <summary>
        /// Generate a random curve
        /// </summary>
        /// <param name="curve"></param>
        /// <param name="slices"></param>
        protected void GenerateRandomCurve(AnimationCurve curve, float multipliers, float slices = 10f, bool isLine = false)
        {
            while (curve.keys.Length > 0)
                curve.RemoveKey(0);

            if (isLine && slices > 0)
            {
                curve.AddKey(0, 0);
                for (int i = 1; i <= slices; i++)
                    curve.AddKey(i / slices, Random.Range(0f, 1f) * multipliers);
            }
            else
            {
                for (int i = 0; i <= slices; i++)
                    curve.AddKey(i / slices, Random.Range(0f, 1f) * multipliers);
            }
        }

        /// <summary>
        /// Get scaled delta time
        /// </summary>
        /// <returns></returns>
        private float GetScaledDeltaTime()
        {
            float deltaTime = targetTimeScale * Time.deltaTime * Time.timeScale;

            if (ignoreTimeScale)
            {
                deltaTime = (Time.realtimeSinceStartup - realtimeSinceStartup) * targetTimeScale;
                realtimeSinceStartup = Time.realtimeSinceStartup;
            }

            return deltaTime;

        }
        #endregion

    }
}