using UnityEngine;
using System.Collections;


namespace LDFW.UserInput
{
    
    public class PinchController : MonoBehaviour
    {
        public static PinchController               Instance;
        public delegate void                        OnPinchDelegate(float distanceDelta);
        public OnPinchDelegate                      onPinchCallback;
        
        private float                               currentDistance;

        private void Awake()
        {
            if (Instance != null)
            {
                DestroyImmediate(this);
                return;
            }
            Instance = this;
        }

        /// <summary>
        /// On pinch input received
        /// </summary>
        /// <param name="touch0"></param>
        /// <param name="touch1"></param>
        /// <returns></returns>
        public virtual bool OnPinchInputReceived(Touch touch0, Touch touch1)
        {
            if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
            {
                currentDistance = Vector2.Distance(touch0.position, touch1.position);
                return true;
            }
            else if (touch0.phase == TouchPhase.Ended || touch1.phase == TouchPhase.Ended)
            {
                return false;
            }
            else
            {
                float newDistance = Vector2.Distance(touch0.position, touch1.position);
                onPinchCallback(newDistance - currentDistance);
                currentDistance = newDistance;
                return true;
            }
            
        }

        /// <summary>
        /// On destroy
        /// </summary>
        private void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }
    }

}
