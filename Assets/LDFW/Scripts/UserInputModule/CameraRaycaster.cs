using UnityEngine;
using System.Collections;

namespace LDFW.UserInput
{

    public class CameraRaycaster : MonoBehaviour
    {

        [HideInInspector]
        public Camera targetCamera;


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

        public RaycastHit TryProcessInput (Vector2 screenPosition)
        {
            Ray ray = targetCamera.ScreenPointToRay(screenPosition);
            RaycastHit hit = new RaycastHit();
            Physics.Raycast(ray, out hit);
            return hit;
        }
    }

}