using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

namespace LDFW.Tween {
    public class LDFWTweenUICanvasGroup : LDFWTweenBaseOne {

        public new CanvasGroup targetTransform;

        protected override void PreStart () {
            curveCount = 1;
            startingValue = new float[curveCount];
            startingValue[0] = targetTransform.alpha;

        }

        protected override void PostCurrentValueCalculation()
        {
            targetTransform.alpha = currentValue[0];
        }

    }
}