using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace LDFW.Tween {
    [RequireComponent (typeof (RectTransform))]
    public class LDFWTweenUIAnchoredPosition : LDFWTweenBaseTwo
    {
        
        protected override void PreStart () {
            curveCount = 2;
            startingValue = new float[curveCount];
            startingValue[0] = (targetTransform as RectTransform).anchoredPosition.x;
            startingValue[1] = (targetTransform as RectTransform).anchoredPosition.y;
        }

        protected override void PostCurrentValueCalculation()
        {
            (targetTransform as RectTransform).anchoredPosition = new Vector2(currentValue[0], currentValue[1]);
        }


    }
}