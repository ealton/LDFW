using UnityEngine;
using System;
using System.Reflection;

using Random = UnityEngine.Random;

namespace LDFW.Tween
{

    public partial class LDFWTweenBase
    {

        public LDFWTweenBase SetCurveStyle(CurveStyle style)
        {
            curveStyle = style;

            if (curveStyle != CurveStyle.AnimationCurve)
            {
                var minfo = typeof(LDFWTweenFunctions).GetMethod(
                    style.ToString(), BindingFlags.Public | BindingFlags.Static);

                var tempFunction = (EvaluationFunction)Delegate.CreateDelegate(
                    typeof(EvaluationFunction), minfo);

                if (tempFunction != null)
                {
                    evalFunctions = new EvaluationFunction[paramCount];
                    for (int i = 0; i < paramCount; i++)
                        evalFunctions[i] = tempFunction;

                }
                else
                {
                    Debug.Log("Invalid temp function");
                }
            }

            return this;
        }

        public LDFWTweenBase SetTweenStyle(TweenStyle style)
        {
            tweenStyle = style;

            return this;
        }

        public LDFWTweenBase SetTarget(UnityEngine.Object obj)
        {
            target = obj;

            return this;
        }

        public LDFWTweenBase SetAnimationCurve(AnimationCurve curve, int curveIndex = -1)
        {
            SetCurveStyle(CurveStyle.AnimationCurve);

            curveList = new AnimationCurve[paramCount];
            if (curveIndex == -1)
            {
                for (int i = 0; i < paramCount; i++)
                    curveList[i] = curve;
            }
            else
            {
                for (int i = 0; i < paramCount; i++)
                {
                    if (i == curveIndex)
                        curveList[i] = curve;
                    else
                        curveList[i] = GetLinearAnimationCurve();
                }
            }
            return this;
        }

        public LDFWTweenBase SetAnimationCurve(params AnimationCurve[] curves)
        {
            curveList = new AnimationCurve[paramCount];
            for (int i = 0; i < paramCount; i++)
            {
                if (i < curves.Length)
                    curveList[i] = curves[i];
                else
                    curveList[i] = GetLinearAnimationCurve();
            }

            return this;
        }

        private AnimationCurve GetLinearAnimationCurve()
        {
            var curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
            return curve;
        }

    }

}
