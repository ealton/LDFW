using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LDFW.UserInput
{

    public class InputModuleController : MonoBehaviour
    {


        public static InputModuleController instance;
        public List<CameraRaycaster>        inputCameras;
        public GameObject                   selectedObject;
        public int                          maxTouchCount = 1;


        private void Awake()
        {
            if (instance != null)
            {
                Destroy (this);
                return;
            }
            instance = this;
            inputCameras = new List<CameraRaycaster> ();
        }

        private void Update()
        {
            Debug.Log ("Touch count = " + Input.touchCount);
            if (maxTouchCount > 0 && Input.touchCount > 0)
            {
                //for (int i=0; i)
                for (int i=0; i<maxTouchCount && i<Input.touchCount; i++)
                {
                    ProcessTouchInput (Input.touches[i]);
                }
            }
        }

        public void RegisterCamera(CameraRaycaster camRaycaster)
        {
            float camDepth = camRaycaster.targetCamera.depth;
            int cameraCount = inputCameras.Count;
            for (int i=0; i<cameraCount; i++)
            {
                if (inputCameras[i].targetCamera.depth < camDepth)
                {
                    inputCameras.Insert (i, camRaycaster);
                }
            }
        }

        private void ProcessTouchInput(Touch touch)
        {
            Debug.Log ("Touch position = " + touch.position.ToString ());
            RaycastHit? hitNullable = GetRaycastTarget(touch.position);
            if (hitNullable != null)
            {
                RaycastHit hit = (RaycastHit) hitNullable;
                Debug.Log ("Touched " + hit.transform.name);

            }
        }

        private RaycastHit? GetRaycastTarget(Vector2 screenPosition)
        {
            RaycastHit? hit = null;
            foreach (var cam in inputCameras)
            {
                hit = cam.TryProcessInput (screenPosition);
                if (hit != null)
                    return hit;
            }
            return null;
        }

        /*
        private Touch ConvertMouseToTouch(int mouseButtonID, int type, Vector2 mousePosition)
        {
            Touch touch = new Touch();
            touch.position = mousePosition;
        }
        */
    }

    
}