using UnityEngine;
using System.Collections;

namespace LDFW.Tween
{
    public class LDFWTweenScale : LDFWTweenBaseThree
    {

        protected override void PostCurrentValueCalculation()
        {
            targetTransform.localScale = new Vector3(currentValue[0], currentValue[1], currentValue[2]);
        }


    }
}
