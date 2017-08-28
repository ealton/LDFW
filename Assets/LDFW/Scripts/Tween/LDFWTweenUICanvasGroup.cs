using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

namespace LDFW.Tween
{

    public class LDFWTweenUICanvasGroup : LDFWTweenBaseOne
    {

        public CanvasGroup targetCanvasGroup;

        protected override void PostCurrentValueCalculation()
        {
            targetCanvasGroup.alpha = currentValue[0];
        }

    }
}