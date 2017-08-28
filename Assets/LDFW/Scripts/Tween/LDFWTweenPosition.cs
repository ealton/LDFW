using UnityEngine;
using System;
using System.Collections;

namespace LDFW.Tween {

    public class LDFWTweenPosition : LDFWTweenBaseThree
    {
        
        protected override void PostCurrentValueCalculation()
        {
            targetTransform.localPosition = new Vector3(currentValue[0], currentValue[1], currentValue[2]);
        }

    }
}
