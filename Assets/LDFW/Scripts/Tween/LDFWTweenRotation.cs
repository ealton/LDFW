using UnityEngine;
using System.Collections;

namespace LDFW.Tween {
    public class LDFWTweenRotation : LDFWTweenBaseThree
    {
        
        protected override void PostCurrentValueCalculation()
        {
            targetTransform.localEulerAngles = new Vector3(currentValue[0], currentValue[1], currentValue[2]);
        }


    }
}