using UnityEngine;
using System;
using System.Collections;

namespace LDFW.Tween
{


    public class LDFWTweenCamera : LDFWTweenBase
    {

        public bool tweenFOV = false;
        public bool tweenClipPlane = false;
        public bool tweenPosition = false;
        public bool tweenEulerAngles = false;
        public bool tweenScale = false;
        public bool tweenOrthographicSize = false;


        protected override void PostCurrentValueCalculation()
        {
            DecodeCameraData(target as Camera, currentValue);
        }

        public static float[] EncodeCameraData(Camera cam)
        {
            return new float[] {
                cam.fieldOfView,
                cam.farClipPlane,
                cam.nearClipPlane,

                cam.transform.position.x, cam.transform.position.y, cam.transform.position.z,
                cam.transform.eulerAngles.x, cam.transform.eulerAngles.y, cam.transform.eulerAngles.z,
                cam.transform.localScale.x, cam.transform.localScale.y, cam.transform.localScale.z,

                cam.orthographicSize
            };
        }

        private void DecodeCameraData(Camera cam, float[] data)
        {
            if (cam.orthographic && tweenOrthographicSize)
            {
                cam.orthographicSize = data[12];
            }

            if (!cam.orthographic && tweenFOV)
            {
                cam.fieldOfView = data[0];
            }

            if (tweenClipPlane)
            {
                cam.farClipPlane = data[1];
                cam.nearClipPlane = data[2];
            }

            if (tweenPosition)
            {
                cam.transform.position = new Vector3(data[3], data[4], data[5]);
            }

            if (tweenEulerAngles)
            {
                cam.transform.eulerAngles = new Vector3(data[6], data[7], data[8]);
            }

            if (tweenScale)
            {
                cam.transform.localScale = new Vector3(data[9], data[10], data[11]);
            }
        }

        public LDFWTweenCamera SetTweenFlags(
            bool fov, bool clipPlanes, bool position, bool eulerAngles,
            bool scale, bool orthographicSize)
        {
            tweenFOV = fov;
            tweenClipPlane = clipPlanes;
            tweenPosition = position;
            tweenEulerAngles = eulerAngles;
            tweenScale = scale;
            tweenOrthographicSize = orthographicSize;

            return this;
        }

    }

}
