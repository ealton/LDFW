﻿using UnityEngine;
using System.Collections;

using LDFW.UserInput;
using System;

public class Test3 : MonoBehaviour, ITouchBegin, ITouchDrag, ITouchEnd, ITouchClick
{

    private float distanceToCamera;
    private Camera renderingCamera;
    private Vector3 initialPosition;



    public void OnTouchBegin(InputData input)
    {
        distanceToCamera = (input.camera.transform.position - transform.position).magnitude;
        renderingCamera = input.camera;
        initialPosition = transform.position;
    }

    public void OnTouchClick(InputData input)
    {
        Debug.Log("OuTouchClick!");
    }

    public void OnTouchDrag(InputData input)
    {
        transform.position = renderingCamera.ScreenToWorldPoint(
            new Vector3(input.position.x, input.position.y, distanceToCamera));
    }

    public void OnTouchEnd(InputData input)
    {
        transform.position = initialPosition;
    }
}
