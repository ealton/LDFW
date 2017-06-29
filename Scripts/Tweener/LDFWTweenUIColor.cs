using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace LDFW.Tween {
    
    public class LDFWTweenUIColor : LDFWTweenBaseFour {
        
        private MaskableGraphic uiImageTarget;
        private Color currentColor;

        protected override void PreStart () {
            uiImageTarget = targetTransform.GetComponent<MaskableGraphic>();
            currentColor = new Color();

            curveCount = 4;
            startingValue = new float[curveCount];
            startingValue[0] = uiImageTarget.color.r * 255f;
            startingValue[1] = uiImageTarget.color.g * 255f;
            startingValue[2] = uiImageTarget.color.b * 255f;
            startingValue[3] = uiImageTarget.color.a * 255f;

        }

        protected override void PostCurrentValueCalculation()
        {
            currentColor.r = currentValue[0] / 255f;
            currentColor.g = currentValue[1] / 255f;
            currentColor.b = currentValue[2] / 255f;
            currentColor.a = currentValue[3] / 255f;

            uiImageTarget.color = currentColor;
        }
    }

}