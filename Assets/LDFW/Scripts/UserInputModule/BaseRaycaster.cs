using UnityEngine;
using System.Collections;

namespace LDFW.UserInput
{
    
    public abstract class BaseRaycaster : MonoBehaviour
    {
        [HideInInspector]
        public Camera targetCamera;
        public LayerMask layermask;


        protected virtual void Start()
        {
            if (targetCamera == null)
            {
                Destroy(this);
                return;
            }
            else
            {
                InputModuleController.Instance.RegisterCamera(this);
            }
        }
        public abstract RaycasterHit ProcessInput(Vector2 screenPosition);
    }

}