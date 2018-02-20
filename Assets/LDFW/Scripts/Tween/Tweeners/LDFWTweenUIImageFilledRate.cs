using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace LDFW.Tween
{

    public class LDFWTweenUIImageFilledRate : LDFWTweenBase
    {

        protected override void PostCurrentValueCalculation()
        {
            (target as Image).fillAmount = currentValue[0];
        }

    }

}
