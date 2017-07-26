using UnityEngine;
using System.Collections;

using LDFW.UserInput;

public class Test8 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PinchController.Instance.onPinchCallback = OnPinchCallBack;
	}

    public void OnPinchCallBack(float distance)
    {
        ScreenPrinter.instance.Log("distance = " + distance);
    }
}
