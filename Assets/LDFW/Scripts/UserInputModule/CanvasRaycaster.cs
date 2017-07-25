using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace LDFW.UserInput
{

    [RequireComponent(typeof(Canvas))]
    public class CanvasRaycaster : BaseRaycaster
    {

        private Canvas                      canvas;
        private MaskableGraphic[]           uiList;
        private int                         uiListCount;


        protected override void Start()
        {
            canvas = GetComponent<Canvas>();
            if (canvas == null)
            {
                DestroyImmediate(this);
                return;
            }

            targetCamera = canvas.worldCamera;
            base.Start();

            GetMaskableGraphicList();
        }

        /// <summary>
        /// Process input, returns RaycastHit
        /// </summary>
        /// <param name="screenPosition"></param>
        /// <returns></returns>
        public override RaycasterHit ProcessInput(Vector2 screenPosition)
        {
            Ray ray = targetCamera.ScreenPointToRay(screenPosition);
            RaycasterHit hit = new RaycasterHit();
            MaskableGraphic currentElement;
            RectTransform currentRectTransform;

            if (isActiveAndEnabled)
            {
                for (int i = uiListCount - 1; i >= 0; i--)
                {
                    currentElement = uiList[i];
                    currentRectTransform = currentElement.GetComponent<RectTransform>();
                    if (currentElement.raycastTarget &&
                        currentRectTransform != null &&
                        currentElement.gameObject.activeSelf &&
                        RectTransformUtility.RectangleContainsScreenPoint(currentRectTransform, screenPosition, targetCamera))
                    {
                        hit.transform = currentRectTransform;
                    }
                }
            }

            return hit;
        }

        /// <summary>
        /// Get MaskableGraphic in all children
        /// </summary>
        /// <param name="uiParent"></param>
        public void GetMaskableGraphicList()
        {
            uiList = canvas.GetComponentsInChildren<MaskableGraphic>();
            uiListCount = uiList.Length;
        }
    }

}