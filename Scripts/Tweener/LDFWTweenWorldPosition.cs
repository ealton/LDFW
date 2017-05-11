using UnityEngine;
using System.Collections;

namespace LDFW.Tween {
    public class LDFWTweenWorldPosition : LDFWTweenBase {

        protected override void PreStart () {
            curveCount = 3;
            startingValue = new float[curveCount];
            startingValue[0] = targetTransform.position.x;
            startingValue[1] = targetTransform.position.y;
            startingValue[2] = targetTransform.position.z;

        }

        protected override void PostCurrentValueCalculation()
        {
            targetTransform.position = new Vector3(currentValue[0], currentValue[1], currentValue[2]);
        }

    }
}
