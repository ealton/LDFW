using UnityEngine;
using System.Collections;

namespace LDFW.UserInput
{
    public class InputData
    {
        
        public float                    radius;
        public float                    radiusVariance;
        
        public int                      inputID;

        public InputDataPhase           phase;
        public Vector2                  deltaPosition;
        public Vector2                  position;

        public Camera                   camera;


        private GameObject              selectedGameObject = null;
        private GameObject              previousSelectedGameObject = null;
        private ITouchBegin             selectedTouchBegin = null;
        private ITouchDrag              selectedTouchDrag = null;
        private ITouchEnd               selectedTouchEnd = null;
        private ITouchClick             selectedTouchClick = null;
        private ITouchDoubleClick       selectedTouchDoubleClick = null;
        private float                   lastTouchBeginTime = 0;
        private float                   currentTouchBeginTime = 0;
        private int                     consecutiveTouchBeginCount = 0;


        public InputData()
        {
            Init();
        }
        
        public InputData(Vector2 position)
        {
            Init();
            this.position = position;
        }

        public void Init()
        {
            this.selectedGameObject = null;
            this.phase = InputDataPhase.Init;
            this.deltaPosition = Vector2.zero;
            this.position = Vector2.zero;
        }

        public void UpdateData(Touch touch)
        {
            //deltaTime = touch.deltaTime;
            radius = touch.radius;
            radiusVariance = touch.radiusVariance;
            inputID = touch.fingerId;
            phase = TouchPhaseToInputDataPhase(touch.phase);
            deltaPosition = touch.deltaPosition;
            position = touch.position;
        }

        #region MouseInputs

        public void TouchBegin(GameObject selectedObject, Vector2 pos, Camera cam)
        {
            radius = 1;
            radiusVariance = 0;
            phase = InputDataPhase.Begin;
            deltaPosition = Vector2.zero;
            position = pos;
            lastTouchBeginTime = currentTouchBeginTime;
            currentTouchBeginTime = Time.realtimeSinceStartup;

            selectedGameObject = selectedObject;
            if (selectedGameObject != null)
            {
                selectedTouchBegin = selectedGameObject.GetComponent<ITouchBegin>();
                selectedTouchDrag = selectedGameObject.GetComponent<ITouchDrag>();
                selectedTouchEnd = selectedGameObject.GetComponent<ITouchEnd>();
                selectedTouchClick = selectedGameObject.GetComponent<ITouchClick>();
                selectedTouchDoubleClick = selectedGameObject.GetComponent<ITouchDoubleClick>();
            }
            camera = cam;


            if (currentTouchBeginTime - lastTouchBeginTime > InputConfig.Instance.consecutiveTouchTimeInterval)
            {
                consecutiveTouchBeginCount = 0;
            }
            else
            {
                consecutiveTouchBeginCount++;
            }


            if (selectedTouchBegin != null)
                selectedTouchBegin.OnTouchBegin(this);
        }

        public void TouchMove(Vector2 position)
        {
            deltaPosition = position - this.position;
            this.position = position;

            if (deltaPosition.magnitude <= InputConfig.Instance.deltaPositionThreshold)
            {
                // do nothing
                phase = InputDataPhase.Stationary;
            }
            else
            {
                phase = InputDataPhase.Move;
                if (selectedTouchDrag != null)
                    selectedTouchDrag.OnTouchDrag(this);
            }
        }

        public void TouchEnd(Vector2 position)
        {
            phase = InputDataPhase.End;
            deltaPosition = Vector2.zero;
            this.position = position;

            if (selectedTouchEnd != null)
                selectedTouchEnd.OnTouchEnd(this);

            previousSelectedGameObject = selectedGameObject;
            selectedGameObject = null;

            float currentTime = Time.realtimeSinceStartup;
            if (currentTime - lastTouchBeginTime < InputConfig.Instance.consecutiveTouchTimeInterval)
            {
                if (consecutiveTouchBeginCount == 0 && selectedTouchClick != null)
                    selectedTouchClick.OnTouchClick(this);
                else if (selectedTouchDoubleClick != null)
                    selectedTouchDoubleClick.OnTouchDoubleClick(this);

                consecutiveTouchBeginCount = 0;
            }
        }
        
        #endregion


        #region HelperFunctions

        public static InputDataPhase TouchPhaseToInputDataPhase(TouchPhase phase)
        {
            switch (phase)
            {
                case TouchPhase.Began:
                    return InputDataPhase.Begin;
                case TouchPhase.Moved:
                    return InputDataPhase.Move;
                case TouchPhase.Stationary:
                    return InputDataPhase.Stationary;
                case TouchPhase.Ended:
                    return InputDataPhase.End;
                case TouchPhase.Canceled:
                    return InputDataPhase.Cancel;
                default:
                    return InputDataPhase.Init;
            }
        }

        #endregion

    }

    public enum InputDataPhase
    {
        Begin,
        Move,
        Stationary,
        End,
        Cancel,
        Init,
    }
    
}