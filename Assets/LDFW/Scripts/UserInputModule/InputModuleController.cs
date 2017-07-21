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

        private Dictionary<int, InputData>  touchInputDic;
        private Dictionary<int, GameObject> selectedGameObjectDic;
        

        private void Awake()
        {
            if (_instance != null)
            {
                DestroyImmediate(gameObject);
                return;
            }
            _instance = this;

            inputCameras = new List<CameraRaycaster>();
            touchInputDic = new Dictionary<int, InputData>();
            selectedGameObjectDic = new Dictionary<int, GameObject>();

#if UNITY_EDITOR
            touchInputDic.Add(0, new InputData());
            touchInputDic.Add(1, new InputData());
#else
            for (int i = 2; i < maxTouchCount + 2; i++)
                touchInputArray[i] = new InputData();
#endif

            
        }

        private void Update()
        {
#if UNITY_EDITOR
            ProcessMouseButton(touchInputDic[0], 0);
            ProcessMouseButton(touchInputDic[1], 1);
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

#if true || !UNITY_EDITOR
        /// <summary>
        /// Processes a touch
        /// </summary>
        private void ProcessTouchInput()
        {
            //if (Input.touchCount > 0)
            //    ScreenPrinter.instance.Log("Touch count: " + Input.touchCount);

            for (int i = 2; i < maxTouchCount + 2; i++)
            {
                if (i - 2 < Input.touchCount)
                {
                    Touch touch = Input.GetTouch(i - 2);
                    int touchID = touch.fingerId;
                    //ScreenPrinter.instance.Log("Touch " + i + ": " + touch.position);
                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            RaycastHit hit;
                            Camera cam;
                            GetRaycastTarget(touch.position, out hit, out cam);
                            if (hit.transform != null && !IsObjectSelected(hit.transform.gameObject))
                            {
                                if (touchInputDic[touchID] != null)
                                    touchInputDic.Remove(touchID);

                                touchInputDic.Add(touchID, new InputData());
                                touchInputDic[touchID].TouchBegin(hit.transform.gameObject, touch.position, cam);

                                if (selectedGameObjectDic[touchID] != null)
                                    selectedGameObjectDic.Remove(touchID);

                                selectedGameObjectDic[touchID] = hit.transform.gameObject;
                            }
                            break;
                        case TouchPhase.Moved:
                            if (touchInputDic[touchID] != null)
                                touchInputDic[touchID].TouchMove(touch.position);
                            break;
                        case TouchPhase.Canceled:
                        case TouchPhase.Ended:
                            if (touchInputDic[touchID] != null)
                                touchInputDic[touchID].TouchEnd(touch.position);

                            if (selectedGameObjectDic[touchID] != null)
                                selectedGameObjectDic.Remove(touchID);
                            break;
                    }
                }
            }
        }
#endif

#if UNITY_EDITOR
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
                if (hit.transform != null && !IsObjectSelected(hit.transform.gameObject))
                {
                    mouseButtonInputData.TouchBegin(hit.transform.gameObject, Input.mousePosition, cam);
                    selectedGameObjectDic[mouseButtonIndex] = hit.transform.gameObject;
                }
            }
            else if (Input.GetMouseButton(mouseButtonIndex) && selectedGameObjectDic[mouseButtonIndex] != null)
            {
                mouseButtonInputData.TouchMove(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(mouseButtonIndex) && selectedGameObjectDic[mouseButtonIndex] != null)
            {
                mouseButtonInputData.TouchEnd(Input.mousePosition);
                selectedGameObjectDic[mouseButtonIndex] = null;
            }
        }
#endif

        /// <summary>
        /// Look through the registered camera, try to finds the first raycast target
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

            Debug.Log("Return null");
            hit = new RaycastHit();
            camera = null;
        }


#region HelperFunctions
        /// <summary>
        /// Checks if the gameObject is selected by any touches
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        private bool IsObjectSelected(GameObject gameObject)
        {
            foreach (var keyPair in selectedGameObjectDic)
            {
                if (keyPair.Value == gameObject)
                    return true;
            }
            
            return false;
        }
#endregion
    }
    
}