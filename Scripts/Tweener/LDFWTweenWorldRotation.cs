﻿using UnityEngine;
using System.Collections;

namespace LDFW.Tween {
    public class LDFWTweenWorldRotation : LDFWTweenBaseThree
    {


        protected override void PreStart () {
            curveCount = 3;
            startingValue = new float[curveCount];
            startingValue[0] = targetTransform.eulerAngles.x;
            startingValue[1] = targetTransform.eulerAngles.y;
            startingValue[2] = targetTransform.eulerAngles.z;

        }

        protected override void PostCurrentValueCalculation()
        {
            targetTransform.eulerAngles = new Vector3(currentValue[0], currentValue[1], currentValue[2]);
        }

    }
}