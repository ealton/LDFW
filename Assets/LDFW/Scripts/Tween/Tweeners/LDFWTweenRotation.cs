using UnityEngine;
using System.Collections;

namespace LDFW.Tween
{
    public class LDFWTweenRotation : LDFWTweenBase
    {

        protected override void PostCurrentValueCalculation()
        {
            if (target == null)
                return;

            (target as Transform).localEulerAngles = new Vector3(
                currentValue[0],
                currentValue[1],
                currentValue[2]);
        }
    }

}
