using UnityEngine;
using System.Collections;

using LDFW.Tween;

public class test1 : MonoBehaviour {

	IEnumerator Start () {

        transform
            .TweenToPosition(new Vector3(1, 1, 1), 1, 0, true)
            .SetTweenStyle(TweenStyle.PingPong);
        transform
            .TweenToScale(Vector3.zero, 1, 0, true)
            .SetTweenStyle(TweenStyle.PingPong);
        transform
            .TweenToEulerAngles(Vector3.one * 360, 1, 0, true)
            .SetTweenStyle(TweenStyle.PingPong);

        yield return null;
    }
	
}
