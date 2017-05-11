using UnityEngine;
using System.Collections;

namespace LDFW.Tween {
    public class LDFWTweenPosition : LDFWTweenBase {
        
        protected override void PreStart () {
            curveCount = 3;
            startingValue = new float[curveCount];
            startingValue[0] = targetTransform.localPosition.x;
            startingValue[1] = targetTransform.localPosition.y;
            startingValue[2] = targetTransform.localPosition.z;

        }

        protected override void PostCurrentValueCalculation()
        {
            targetTransform.localPosition = new Vector3(currentValue[0], currentValue[1], currentValue[2]);
        }

    }
}
