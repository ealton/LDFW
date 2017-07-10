using UnityEngine;
using System.Collections;

public class Test1 : MonoBehaviour {



    IEnumerator Start()
    {
        transform.localEulerAngles = Vector3.zero;
        Debug.Log (transform.localRotation.ToString ());
        yield return null;
        
        transform.localEulerAngles = new Vector3 (0, 90, 0);
        Debug.Log (transform.localRotation.ToString ());
        yield return null;

        transform.localEulerAngles = new Vector3 (0, 180, 0);
        Debug.Log (transform.localRotation.ToString ());
        yield return null;

        transform.localEulerAngles = new Vector3 (0, 270, 0);
        Debug.Log (transform.localRotation.ToString ());
        yield return null;

        transform.localEulerAngles = new Vector3 (0, 360, 0);
        Debug.Log (transform.localRotation.ToString ());
        yield return null;

    }
}
