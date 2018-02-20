using UnityEngine;
using System;
using System.Collections;

namespace LDFW.Tween
{

    [System.Serializable]
    public class LDFWTweenPosition : LDFWTweenBase
    {

        protected override void PostCurrentValueCalculation()
        {
            if (target == null)
                return;

            (target as Transform).localPosition = new Vector3(
                currentValue[0],
                currentValue[1],
                currentValue[2]);

            //Debug.Log("current position = " + (target as Transform).localPosition.ToString());
        }
    }

}
