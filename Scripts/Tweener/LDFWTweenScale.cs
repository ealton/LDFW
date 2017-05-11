using UnityEngine;
using System.Collections;

namespace LDFW.Tween {
    public class LDFWTweenScale : LDFWTweenBase {


        protected override void PreStart () {
            curveCount = 3;
            startingValue = new float[curveCount];
            startingValue[0] = targetTransform.localScale.x;
            startingValue[1] = targetTransform.localScale.y;
            startingValue[2] = targetTransform.localScale.z;

        }

        protected override void PostCurrentValueCalculation()
        {
            targetTransform.localScale = new Vector3(currentValue[0], currentValue[1], currentValue[2]);
        }


    }
}
