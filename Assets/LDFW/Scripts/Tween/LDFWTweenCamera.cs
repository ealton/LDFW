using UnityEngine;
using System;
using System.Collections;

namespace LDFW.Tween
{

    
    public class LDFWTweenCamera : LDFWTweenBase
    {

        public Camera targetCamera;

        public Camera fromCamera;
        public Camera toCamera;

        public bool isTweeningFieldOfView;
        public bool isTweeningFarClipPlane;
        public bool isTweeningNearClipPlane;

        public bool isTweeningPosition;
        public bool isTweeningEulerAngles;
        public bool isTweeningScale;



        protected override void PreStart()
        {
            if (targetCamera == null)
                targetCamera = GetComponent<Camera>();

            if (targetCamera == null)
                isTweenerPlaying = false;

            curveCount = 12;
            fromValue = EncodeCameraData(fromCamera);
            toValue = EncodeCameraData(toCamera);
        }

        public LDFWTweenBase Init(Camera targetCamera, Camera fromCamera, Camera toCamera, float duration, float startDelay, 
            bool autoStart = false, Action endAction = null, bool autoDestroyComponent = false, bool autoDestroyGameObject = false)
        {
            this.targetCamera = targetCamera;
            this.fromCamera = fromCamera;
            this.toCamera = toCamera;

            return Init(EncodeCameraData(fromCamera), EncodeCameraData(toCamera),
                duration, startDelay, autoStart, endAction, autoDestroyComponent, autoDestroyGameObject);
        }

        protected override void PostCurrentValueCalculation()
        {
            DecodeCameraData(targetCamera, currentValue);
        }

        private float[] EncodeCameraData(Camera cam)
        {
            return new float[] {
                cam.fieldOfView, cam.farClipPlane, cam.nearClipPlane,
                cam.transform.position.x, cam.transform.position.y, cam.transform.position.z,
                cam.transform.eulerAngles.x, cam.transform.eulerAngles.y, cam.transform.eulerAngles.z,
                cam.transform.localScale.x, cam.transform.localScale.y, cam.transform.localScale.z
            };
        }

        private void DecodeCameraData(Camera cam, float[] data)
        {
            cam.fieldOfView = data[0];
            cam.farClipPlane = data[1];
            cam.nearClipPlane = data[2];
            cam.transform.position = new Vector3(data[3], data[4], data[5]);
            cam.transform.eulerAngles = new Vector3(data[6], data[7], data[8]);
            cam.transform.localScale = new Vector3(data[9], data[10], data[11]);
        }
        
    }

}
