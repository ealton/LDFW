using UnityEngine;
using System.Collections;

namespace LDFW.UserInput
{
    public class InputConfig : MonoBehaviour
    {
        private static InputConfig _instance;
        public static InputConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = InputModuleController.Instance.gameObject.AddComponent<InputConfig>();
                    return _instance;
                }
                else
                {
                    return _instance;
                }
            }
        }

        public float deltaPositionThresholdPercentage = 0;
        [HideInInspector]
        public float deltaPositionThreshold = 0;

        public bool allowMultipleMouseButtonClicks = false;

        public float consecutiveTouchTimeInterval = 0.5f;


        private void Awake()
        {
            deltaPositionThreshold = Mathf.Sqrt(Screen.height * Screen.height + Screen.width * Screen.width) * deltaPositionThresholdPercentage;
        }
    }

}