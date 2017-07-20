using UnityEngine;
using System.Collections;

using LDFW.UserInput;
using System;

public class Test3 : MonoBehaviour, ITouchBegin, ITouchDrag, ITouchEnd
{

    private float distanceToCamera;
    private Camera renderingCamera;

    public void OnTouchBegin(InputData input)
    {
        distanceToCamera = (input.camera.transform.position - transform.position).magnitude;
        renderingCamera = input.camera;
    }

    public void OnTouchDrag(InputData input)
    {
        transform.position = renderingCamera.ScreenToWorldPoint(
            new Vector3(input.position.x, input.position.y, distanceToCamera));
    }

    public void OnTouchEnd(InputData input)
    {
    }
}
