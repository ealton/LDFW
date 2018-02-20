using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace LDFW.Tween
{
    public class LDFWTweenUIAnchoredPosition : LDFWTweenBase
    {

        protected override void PostCurrentValueCalculation()
        {
            if (target == null)
                return;

            (target as RectTransform).anchoredPosition = new Vector2(
                currentValue[0],
                currentValue[1]
            );
        }

    }
}