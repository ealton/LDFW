using UnityEngine;
using System.Collections;

namespace LDFW.Tween
{
    public class LDFWTweenWorldRotation : LDFWTweenBase
    {

        protected override void PostCurrentValueCalculation()
        {
            if (target == null)
                return;

            (target as Transform).eulerAngles = new Vector3(
                currentValue[0],
                currentValue[1],
                currentValue[2]);
        }

    }
}
