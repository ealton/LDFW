using UnityEngine;
using System.Collections;

namespace LDFW.Tween {
    public class LDFWTweenRotation : LDFWTweenBaseThree
    {


        protected override void PreStart () {
            curveCount = 3;
            startingValue = new float[curveCount];
            startingValue[0] = targetTransform.localEulerAngles.x;
            startingValue[1] = targetTransform.localEulerAngles.y;
            startingValue[2] = targetTransform.localEulerAngles.z;

        }

        protected override void PostCurrentValueCalculation()
        {
            targetTransform.localEulerAngles = new Vector3(currentValue[0], currentValue[1], currentValue[2]);
        }


    }
}