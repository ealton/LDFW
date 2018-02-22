using UnityEngine;
using System;

using Random = UnityEngine.Random;

namespace LDFW.Tween
{
    [System.Serializable]
    public abstract partial class LDFWTweenBase
    {

        public UnityEngine.Object target;
        public int tweenID;

        public float[] fromValue;
        public float[] currentValue;
        public float[] toValue;
        public AnimationCurve[] curveList;
        public int paramCount;

        public float startDelay = 0f;
        public float tweenTime = 0f;
        public float tweenDuration = 1f;
        public float targetTimeScale = 1f;





        public TweenStyle tweenStyle = TweenStyle.Once;
        public CurveStyle curveStyle = CurveStyle.Default;


        protected delegate float EvaluationFunction(float b, float to, float t, float d);
        protected EvaluationFunction[] evalFunctions;



        public bool isTweenBackwards = false;
        public bool isTweenPlaying = false;
        public int burstFrameCount = 0;
        public float burstTime = 0;

        private float realtimeSinceStartup = 0;





        public LDFWTweenBase Init(float from, float to, float duration, float delay)
        {
            return Init(new float[] { from }, new float[] { to }, duration, delay);
        }

        public LDFWTweenBase Init(Vector2 from, Vector2 to, float duration, float delay)
        {
            return Init(new float[] { from.x, from.y }, new float[] { to.x, to.y }, duration, delay);
        }

        public LDFWTweenBase Init(Vector3 from, Vector3 to, float duration, float delay)
        {
            return Init(new float[] { from.x, from.y, from.z }, new float[] { to.x, to.y, to.z }, duration, delay);
        }

        public LDFWTweenBase Init(Vector4 from, Vector4 to, float duration, float delay)
        {
            return Init(new float[] { from.x, from.y, from.z, from.w }, new float[] { to.x, to.y, to.z, to.w }, duration, delay);
        }

        public LDFWTweenBase Init(float[] from, float[] to, float duration, float delay)
        {
            fromValue = from;
            toValue = to;
            paramCount = Mathf.Min(fromValue.Length, toValue.Length);
            currentValue = new float[paramCount];
            tweenDuration = duration;
            startDelay = delay;
            tweenID = LDFWTweenManager.Instance.tweenUpdater.GetTweenID();
            Init();

            return this;
        }

        private LDFWTweenBase Init()
        {
            tweenTime = -startDelay;
            SetCurveStyle(CurveStyle.Linear);
            paramCount = fromValue.Length;

            return this;
        }

        public void Update()
        {
            if (!isTweenPlaying && burstFrameCount <= 0 && burstTime <= 0)
                return;


            UpdateCurrentValues();


            if (!isTweenBackwards)
                tweenTime += Time.deltaTime;
            else
                tweenTime -= Time.deltaTime;
        }

        private void UpdateCurrentValues()
        {
            switch (tweenStyle)
            {
                case TweenStyle.Once:
                    UpdateCurrentValuesTweenOnce();
                    break;
                case TweenStyle.Loop:
                    UpdateCurrentValuesTweenLoop();
                    break;
                case TweenStyle.PingPong:
                    UpdateCurrentValuesTweenPingPong();
                    break;

            }
        }

        private void UpdateCurrentValuesTweenOnce()
        {
            PreCurrentValueCalculation();
            GetCurrentValues();


            if (tweenTime > tweenDuration)
            {
                tweenTime = tweenDuration;
                isTweenPlaying = false;
                PostCurrentValueCalculation();
                OnTweenFinish();
                return;
            }

            PostCurrentValueCalculation();
        }

        private void UpdateCurrentValuesTweenLoop()
        {
            PreCurrentValueCalculation();
            GetCurrentValues();

            if (tweenTime > tweenDuration)
            {
                tweenTime -= tweenDuration;
            }

            PostCurrentValueCalculation();
        }

        private void UpdateCurrentValuesTweenPingPong()
        {
            PreCurrentValueCalculation();
            GetCurrentValues();

            if (tweenTime >= tweenDuration)
                isTweenBackwards = true;
            else if (tweenTime <= 0)
                isTweenBackwards = false;

            PostCurrentValueCalculation();
        }

        protected virtual void PreCurrentValueCalculation()
        {
        }

        protected virtual void PostCurrentValueCalculation()
        {
        }

        private void GetCurrentValues()
        {
            if (curveStyle == CurveStyle.AnimationCurve)
            {
                for (int i = 0; i < paramCount; i++)
                {
                    currentValue[i] = LDFWTweenFunctions.AnimationCurve(
                        fromValue[i], toValue[i], tweenTime, tweenDuration, curveList[i]);
                }
            }
            else
            {
                for (int i = 0; i < paramCount; i++)
                {
                    currentValue[i] = evalFunctions[i](
                        fromValue[i],
                        toValue[i],
                        tweenTime,
                        tweenDuration);
                }
            }
        }

        private void OnTweenFinish()
        {
            isTweenPlaying = false;
            RemoveTween();
        }

        public string GetFromValueString(string format = "0.00")
        {
            return GetFloatValueString(fromValue, format);
        }

        public string GetToValueString(string format = "0.00")
        {
            return GetFloatValueString(toValue, format);
        }

        public string GetCurrentValueString(string format = "0.00")
        {
            return GetFloatValueString(currentValue, format);
        }

        private string GetFloatValueString(float[] values, string strFormat = "0.00")
        {
            if (values == null || values.Length == 0)
                return "NAN";

            var result = values[0].ToString(strFormat);
            for (int i = 1; i < values.Length; i++)
                result += " " + values[i].ToString(strFormat);

            return result;
        }


    }

}
