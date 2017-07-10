using UnityEngine;
using System.Collections;

public class TransformationBend : MonoBehaviour
{
    public Transform tar;
    public Vector3 vec = Vector3.forward;
    private BendObject bendObj;

    private Vector3 lastPosition;

    private void Start()
    {
        bendObj = GetComponent<BendObject>();
    }

    private void Update()
    {
        var pos = transform.position;
        var diff = pos - lastPosition;
        if ((pos - lastPosition).magnitude > .01f)
        {
            var mtx = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
            var abc = mtx * diff;
            bendObj.rotationVector += (Vector3)abc * 100;
            bendObj.BendImmediately();
        }
        lastPosition = pos;
    }
}