using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace LDFW.Tween
{
    [RequireComponent(typeof(RectTransform))]
    public class LDFWTweenUIAnchoredPosition : LDFWTweenBaseTwo
    {
        

        protected override void PostCurrentValueCalculation()
        {
            (targetTransform as RectTransform).anchoredPosition = new Vector2(currentValue[0], currentValue[1]);
        }


    }
}