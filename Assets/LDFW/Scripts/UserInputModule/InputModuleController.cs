using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LDFW.UserInput
{

    public class InputModuleController : MonoBehaviour
    {

        private static InputModuleController _instance;
        public static InputModuleController Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject("LDFWInputModule");
                    _instance = go.AddComponent<InputModuleController>();
                    return _instance;
                }
                else
                {
                    return _instance;
                }
            }
        }

        public List<CameraRaycaster>        inputCameras;
        public GameObject                   selectedObject;
        public int                          maxTouchCount = 2;

        private InputData[]                 touchInputArray;
        private GameObject[]                selectedGameObject;
        private InputData                   leftMouseButon;
        private InputData                   rightMouseButton;

        private void Awake()
        {
            if (_instance != null)
            {
                DestroyImmediate(gameObject);
                return;
            }
            _instance = this;
            inputCameras = new List<CameraRaycaster>();
            touchInputArray = new InputData[maxTouchCount];
            for (int i = 0; i < maxTouchCount; i++)
                touchInputArray[i] = new InputData();

            leftMouseButon = new InputData();
            rightMouseButton = new InputData();
        }

        private void Update()
        {
#if UNITY_EDITOR
            ProcessMouseButton(leftMouseButon, 0);
            ProcessMouseButton(rightMouseButton, 1);
#else
            ProcessTouchInput();
#endif
        }



        /// <summary>
        /// Registers camRaycaster to list
        /// </summary>
        /// <param name="camRaycaster"></param>
        public void RegisterCamera(CameraRaycaster camRaycaster)
        {
            float camDepth = camRaycaster.targetCamera.depth;
            int cameraCount = inputCameras.Count;
            if (cameraCount == 0)
            {
                inputCameras.Insert(0, camRaycaster);
            }
            else
            {

                for (int i = 0; i < cameraCount; i++)
                {
                    if (inputCameras[i].targetCamera.depth < camDepth)
                    {
                        inputCameras.Insert(i, camRaycaster);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Removes camRaycaster from the list
        /// </summary>
        /// <param name="camRaycaster"></param>
        public void UnRegisterCamera(CameraRaycaster camRaycaster)
        {
            int cameraCount = inputCameras.Count;
            for (int i = 0; i < cameraCount; i++)
            {
                if (inputCameras[i] == camRaycaster)
                {
                    inputCameras.RemoveAt(i);
                    break;
                }
            }
        }

        /// <summary>
        /// Processes a touch
        /// </summary>
        private void ProcessTouchInput()
        {
            //if (Input.touchCount > 0)
            //    ScreenPrinter.instance.Log("Touch count: " + Input.touchCount);

            for (int i = 0; i < maxTouchCount; i++)
            {
                if (i < Input.touchCount)
                {
                    Touch touch = Input.GetTouch(i);
                    //ScreenPrinter.instance.Log("Touch " + i + ": " + touch.position);
                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            RaycastHit hit;
                            Camera cam;
                            GetRaycastTarget(touch.position, out hit, out cam);
                            if (hit.transform != null)
                                touchInputArray[i].MouseButtonBegin(hit.transform.gameObject, touch.position, cam);
                            else
                                touchInputArray[i].MouseButtonBegin(null, touch.position, cam);
                            break;
                        case TouchPhase.Moved:
                            touchInputArray[i].MouseButtonUpdate(touch.position);
                            break;
                        case TouchPhase.Canceled:
                        case TouchPhase.Ended:
                            touchInputArray[i].MouseButtonEnd(touch.position);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Process mouse button
        /// </summary>
        /// <param name="mouseButtonInputData"></param>
        /// <param name="mouseButtonIndex"></param>
        private void ProcessMouseButton(InputData mouseButtonInputData, int mouseButtonIndex)
        {
            if (Input.GetMouseButtonDown(mouseButtonIndex))
            {
                //ScreenPrinter.instance.Log("Mouse button down: " + mouseButtonIndex);
                RaycastHit hit;
                Camera cam;
                GetRaycastTarget(Input.mousePosition, out hit, out cam);
                if (hit.transform != null)
                    mouseButtonInputData.MouseButtonBegin(hit.transform.gameObject, Input.mousePosition, cam);
            }
            else if (Input.GetMouseButton(mouseButtonIndex))
            {
                mouseButtonInputData.MouseButtonUpdate(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(mouseButtonIndex))
            {
                mouseButtonInputData.MouseButtonEnd(Input.mousePosition);
            }
        }

        /// <summary>
        /// Finds the first raycast target
        /// </summary>
        /// <param name="screenPosition"></param>
        /// <returns></returns>
        private void GetRaycastTarget(Vector2 screenPosition, out RaycastHit hit, out Camera camera)
        {
            foreach (var cam in inputCameras)
            {
                hit = cam.TryProcessInput(screenPosition);
                camera = cam.targetCamera;
                if (hit.transform != null)
                    return;
            }
            hit = new RaycastHit();
            camera = null;
        }
        
    }
    
}