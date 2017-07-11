using UnityEngine;
using System.Collections;

public class Test2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Vector3[] vec1 = new Vector3[] { Vector3.zero };
        Vector3[] vec2 = vec1;
        Debug.Log(vec1[0].ToString() + ", " + vec2[0].ToString());

        vec1[0] = Vector3.one;

        Debug.Log(vec1[0].ToString() + ", " + vec2[0].ToString());
	}
	
}
