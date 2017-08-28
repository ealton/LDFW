using UnityEngine;
using System.Collections;

namespace LDFW.Tween
{

    public class LDFWTweenCameraFieldOfView : LDFWTweenBaseOne
    {

        private Camera targetCamera;

        void Awake()
        {
            targetCamera = GetComponent<Camera>();
        }

        public LDFWTweenCameraFieldOfView SetCamera(Camera cam)
        {
            targetCamera = cam;
            return this;
        }


        protected override void PostCurrentValueCalculation()
        {
            if (targetCamera != null)
                targetCamera.fieldOfView = currentValue[0];

        }
    }

}