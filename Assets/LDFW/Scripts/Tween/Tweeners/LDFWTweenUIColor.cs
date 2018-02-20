using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace LDFW.Tween
{

    public class LDFWTweenUIColor : LDFWTweenBase
    {

        private Color currentColor;


        protected override void PostCurrentValueCalculation()
        {
            if (target == null)
                return;

            currentColor.r = currentValue[0] / 255f;
            currentColor.g = currentValue[0] / 255f;
            currentColor.b = currentValue[0] / 255f;
            currentColor.a = currentValue[0] / 255f;

            (target as MaskableGraphic).color = currentColor;
        }

    }

}
