﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LDFW.Tween
{


    public class LDFWTweenFunctions
    {

        // http://easings.net/

        public static float s = 1.70158f;

        public static float Default(float b, float to, float t, float d)
        {
            return Linear(b, to, t, d);
        }

        public static float AnimationCurve(float b, float to, float t, float d, AnimationCurve curve)
        {
            return curve.Evaluate(t / d) * (to - b) + b;
        }

        public static float Linear(float b, float to, float t, float d)
        {
            return Mathf.Lerp(b, to, t / d);
        }

        public static float InBack(float b, float to, float t, float d)
        {
            float s = 1.70158f;
            float c = to - b;
            t = t / d;

            return c * t * t * ((s + 1) * t - s) + b;
        }

        public static float OutBack(float b, float to, float t, float d)
        {
            float c = to - b;

            t = t / d - 1;

            return c * (t * t * ((s + 1) * t + s) + 1) + b;
        }

        public static float InOutBack(float b, float to, float t, float d)
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

        public static float OutInBack(float b, float to, float t, float d)
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

        public static float InQuad(float b, float to, float t, float d)
        {
            float c = to - b;
            t = t / d;
            return (float)(c * System.Math.Pow(t, 2) + b);
        }

        public static float OutQuad(float b, float to, float t, float d)
        {
            float c = to - b;
            t = t / d;
            return (float)(-c * t * (t - 2) + b);
        }

        public static float InOutQuad(float b, float to, float t, float d)
        {
            float c = to - b;
            t = t / d * 2;
            if (t < 1)
                return (float)(c / 2 * System.Math.Pow(t, 2) + b);
            else
                return -c / 2 * ((t - 1) * (t - 3) - 1) + b;

        }

        public static float InCubic(float b, float to, float t, float d)
        {
            float c = to - b;
            t = t / d;
            return (float)(c * System.Math.Pow(t, 3) + b);

        }

        public static float OutCubic(float b, float to, float t, float d)
        {
            float c = to - b;
            t = t / d - 1;
            return (float)(c * (System.Math.Pow(t, 3) + 1) + b);

        }

        public static float InOutCubic(float b, float to, float t, float d)
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

        public static float InQuart(float b, float to, float t, float d)
        {
            float c = to - b;
            t = t / d;
            return (float)(c * System.Math.Pow(t, 4) + b);

        }

        public static float OutQuart(float b, float to, float t, float d)
        {
            float c = to - b;
            t = t / d - 1;
            return (float)(-c * (System.Math.Pow(t, 4) - 1) + b);

        }

        public static float InOutQuart(float b, float to, float t, float d)
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

        public static float OutInQuart(float b, float to, float t, float d)
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

        public static float InQuint(float b, float to, float t, float d)
        {
            float c = to - b;
            t = t / d;
            return (float)(c * System.Math.Pow(t, 5) + b);

        }

        public static float OutQuint(float b, float to, float t, float d)
        {
            float c = to - b;
            t = t / d - 1;
            return (float)(c * (System.Math.Pow(t, 5) + 1) + b);
        }

        public static float InOutQuint(float b, float to, float t, float d)
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

        public static float OutInQuint(float b, float to, float t, float d)
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

        public static float InSine(float b, float to, float t, float d)
        {
            float c = to - b;
            return (float)(-c * System.Math.Cos(t / d * (System.Math.PI / 2)) + c + b);

        }

        public static float OutSine(float b, float to, float t, float d)
        {
            float c = to - b;
            return (float)(c * System.Math.Sin(t / d * (System.Math.PI / 2)) + b);
        }

        public static float InOutSine(float b, float to, float t, float d)
        {
            float c = to - b;
            return (float)(-c / 2 * (System.Math.Cos(System.Math.PI * t / d) - 1) + b);

        }

        public static float OutInSine(float b, float to, float t, float d)
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

        public static float InExpo(float b, float to, float t, float d)
        {
            float c = to - b;
            if (t == 0)
                return b;
            else
                return (float)(c * System.Math.Pow(2, 10 * (t / d - 1)) + b - c * 0.001f);
        }

        public static float OutExpo(float b, float to, float t, float d)
        {
            float c = to - b;
            if (t == d)
                return b + c;
            else
                return (float)(c * 1.001 * (-System.Math.Pow(2, -10 * t / d) + 1) + b);

        }

        public static float InOutExpo(float b, float to, float t, float d)
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

        public static float OutInExpo(float b, float to, float t, float d)
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

        public static float OutBounce(float b, float to, float t, float d)
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

        public static float InBounce(float b, float to, float t, float d)
        {
            float c = to - b;
            return c - OutBounce(0, to, d - t, d) + b;
        }

        public static float InOutBounce(float b, float to, float t, float d)
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

        public static float OutInBounce(float b, float to, float t, float d)
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

    }

}
