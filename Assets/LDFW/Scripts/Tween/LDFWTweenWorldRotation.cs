using UnityEngine;
using System.Collections;

namespace LDFW.Tween
{
    public class LDFWTweenWorldRotation : LDFWTweenBaseThree
    {

        protected override void PostCurrentValueCalculation()
        {
            targetTransform.eulerAngles = new Vector3(currentValue[0], currentValue[1], currentValue[2]);
        }

    }
}
