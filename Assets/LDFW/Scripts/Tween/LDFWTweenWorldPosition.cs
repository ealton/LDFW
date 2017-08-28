using UnityEngine;
using System.Collections;

namespace LDFW.Tween
{
    public class LDFWTweenWorldPosition : LDFWTweenBaseThree
    {


        protected override void PostCurrentValueCalculation()
        {
            targetTransform.position = new Vector3(currentValue[0], currentValue[1], currentValue[2]);
        }

    }
}
