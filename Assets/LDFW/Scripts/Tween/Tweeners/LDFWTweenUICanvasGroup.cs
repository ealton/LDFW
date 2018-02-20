using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

namespace LDFW.Tween
{

    public class LDFWTweenUICanvasGroup : LDFWTweenBase
    {

        protected override void PostCurrentValueCalculation()
        {
            if (target == null)
                return;

            (target as CanvasGroup).alpha = currentValue[0];
        }

    }
}