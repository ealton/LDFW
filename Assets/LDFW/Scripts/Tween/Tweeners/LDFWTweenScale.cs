using UnityEngine;
using System.Collections;

namespace LDFW.Tween
{
    public class LDFWTweenScale : LDFWTweenBase
    {

        protected override void PostCurrentValueCalculation()
        {
            if (target == null)
                return;

            (target as Transform).localScale = new Vector3(
                currentValue[0],
                currentValue[1],
                currentValue[2]);
        }


    }
}
