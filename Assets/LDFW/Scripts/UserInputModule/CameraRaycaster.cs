using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

namespace LDFW.UserInput
{

    [RequireComponent(typeof(Camera))]
    public class CameraRaycaster : BaseRaycaster
    {

        protected override void Start()
        {
            targetCamera = GetComponent<Camera> ();
            base.Start();
        }

        /// <summary>
        /// Process input, returns RaycastHit
        /// </summary>
        /// <param name="screenPosition"></param>
        /// <returns></returns>
        public override RaycasterHit ProcessInput(Vector2 screenPosition)
        {
            Ray ray = targetCamera.ScreenPointToRay(screenPosition);
            RaycastHit hit = new RaycastHit();

            if (isActiveAndEnabled)
                Physics.Raycast(ray, out hit, targetCamera.farClipPlane, layermask);

            return new RaycasterHit(hit);
        }
    }

}