using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LDFW.UserInput
{

    public class InputModuleController : MonoBehaviour
    {

        // Instance
        private static InputModuleController    _instance;
        public static InputModuleController     Instance
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
            private set
            {
                _instance = value;
            }
        }

        // ModuleSwitches
        public bool                             allowPinch = false;

        // Raycasters
        public List<BaseRaycaster>              raycasterList;
        public int                              maxTouchCount = 2;

        // Other modules
        private PinchController                 pinchController;

        // Private variables
        private Dictionary<int, InputData>      touchInputDic;
        private Dictionary<int, GameObject>     selectedGameObjectDic;
        private int                             leftMouseButtonIndex = -1;
        private int                             rightMouseButtonIndex = -2;
        

        private void Awake()
        {
            if (_instance != null)
            {
                DestroyImmediate(gameObject);
                return;
            }
            _instance = this;

            raycasterList = new List<BaseRaycaster>();
            touchInputDic = new Dictionary<int, InputData>();

            pinchController = GetComponent<PinchController>();

            selectedGameObjectDic = new Dictionary<int, GameObject>();

#if UNITY_EDITOR
            touchInputDic.Add(leftMouseButtonIndex, new InputData());
            touchInputDic.Add(rightMouseButtonIndex, new InputData());
#endif
        }

        private void Update()
        {
#if UNITY_EDITOR
            ProcessMouseButton(touchInputDic[leftMouseButtonIndex], 0);
            ProcessMouseButton(touchInputDic[rightMouseButtonIndex], 1);
#else
            ProcessTouchInput();
#endif
        }



        /// <summary>
        /// Registers camRaycaster to list
        /// </summary>
        /// <param name="camRaycaster"></param>
        public void RegisterCamera(BaseRaycaster camRaycaster)
        {
            float camDepth = camRaycaster.targetCamera.depth;
            int cameraCount = raycasterList.Count;
            if (cameraCount == 0)
            {
                raycasterList.Insert(0, camRaycaster);
            }
            else
            {

                for (int i = 0; i < cameraCount; i++)
                {
                    if (raycasterList[i].targetCamera.depth < camDepth)
                    {
                        raycasterList.Insert(i, camRaycaster);
                        break;
                    }
                }
                raycasterList.Insert(cameraCount, camRaycaster);
            }
        }

        /// <summary>
        /// Removes camRaycaster from the list
        /// </summary>
        /// <param name="camRaycaster"></param>
        public void UnRegisterCamera(CameraRaycaster camRaycaster)
        {
            int cameraCount = raycasterList.Count;
            for (int i = 0; i < cameraCount; i++)
            {
                if (raycasterList[i] == camRaycaster)
                {
                    raycasterList.RemoveAt(i);
                    break;
                }
            }
        }

#if !UNITY_EDITOR
        /// <summary>
        /// Processes a touch
        /// </summary>
        private void ProcessTouchInput()
        {
            //if (Input.touchCount > 0)
            //    ScreenPrinter.instance.Log("Touch count: " + Input.touchCount);
            
            if (allowPinch && pinchController != null && Input.touchCount >= 2)
            {
                if (pinchController.OnPinchInputReceived(Input.GetTouch(0), Input.GetTouch(1)))
                    return;
            }
            

            for (int i = 0; i < maxTouchCount; i++)
            {
                if (i < Input.touchCount)
                {
                    Touch touch = Input.GetTouch(i);
                    int touchID = touch.fingerId;

                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            RaycasterHit hit;
                            Camera cam;
                            GetRaycastTarget(touch.position, out hit, out cam);
                            if (hit.transform != null && !IsObjectSelected(hit.transform.gameObject))
                            {
                                if (touchInputDic.ContainsKey(touchID))
                                    touchInputDic.Remove(touchID);

                                touchInputDic.Add(touchID, new InputData());
                                touchInputDic[touchID].TouchBegin(hit.transform.gameObject, touch.position, cam);

                                if (selectedGameObjectDic.ContainsKey(touchID))
                                    selectedGameObjectDic[touchID] = hit.transform.gameObject;
                                else
                                    selectedGameObjectDic.Add(touchID, hit.transform.gameObject);
                            }
                            break;
                        case TouchPhase.Moved:
                            if (touchInputDic.ContainsKey(touchID) && selectedGameObjectDic.ContainsKey(touchID))
                                touchInputDic[touchID].TouchMove(touch.position);
                            break;
                        case TouchPhase.Canceled:
                        case TouchPhase.Ended:
                            if (touchInputDic.ContainsKey(touchID) && selectedGameObjectDic.ContainsKey(touchID))
                                touchInputDic[touchID].TouchEnd(touch.position);

                            if (selectedGameObjectDic.ContainsKey(touchID))
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
                RaycasterHit hit;
                Camera cam;
                GetRaycastTarget(Input.mousePosition, out hit, out cam);
                if (hit.transform != null && !IsObjectSelected(hit.transform.gameObject))
                {
                    mouseButtonInputData.TouchBegin(hit.transform.gameObject, Input.mousePosition, cam);
                    selectedGameObjectDic[mouseButtonIndex] = hit.transform.gameObject;
                }
            }
            else if (Input.GetMouseButton(mouseButtonIndex) && selectedGameObjectDic.ContainsKey(mouseButtonIndex))
            {
                mouseButtonInputData.TouchMove(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(mouseButtonIndex) && selectedGameObjectDic.ContainsKey(mouseButtonIndex))
            {
                mouseButtonInputData.TouchEnd(Input.mousePosition);
                selectedGameObjectDic.Remove(mouseButtonIndex);
            }
        }
#endif

        /// <summary>
        /// Look through the registered camera, try to finds the first raycast target
        /// </summary>
        /// <param name="screenPosition"></param>
        /// <returns></returns>
        private void GetRaycastTarget(Vector2 screenPosition, out RaycasterHit hit, out Camera camera)
        {
            foreach (var cam in raycasterList)
            {
                hit = cam.ProcessInput(screenPosition);
                camera = cam.targetCamera;
                if (hit.transform != null)
                    return;
            }
            
            hit = new RaycasterHit();
            camera = null;
        }

        /// <summary>
        /// Cleanup
        /// </summary>
        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
                Instance = null;
            }
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
