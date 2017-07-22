using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

namespace LDFW.UserInput
{

    public class CameraRaycaster : MonoBehaviour
    {

        [HideInInspector]
        public Camera targetCamera;
        public LayerMask layermask;
        


        private void Start()
        {
            targetCamera = GetComponent<Camera> ();
            if (targetCamera == null)
            {
                Destroy (this);
                return;
            }
            else
            {
                InputModuleController.Instance.RegisterCamera (this);
            }
        }

        /// <summary>
        /// Try to process input, returns RaycastHit
        /// </summary>
        /// <param name="screenPosition"></param>
        /// <returns></returns>
        public RaycastHit TryProcessInput (Vector2 screenPosition)
        {
            Ray ray = targetCamera.ScreenPointToRay(screenPosition);
            RaycastHit hit = new RaycastHit();

            if (isActiveAndEnabled)
                Physics.Raycast(ray, out hit, targetCamera.farClipPlane, layermask);

            return hit;
        }
    }

}