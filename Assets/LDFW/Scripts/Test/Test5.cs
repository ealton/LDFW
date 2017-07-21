using UnityEngine;
using System;
using System.Collections;

public class Test5 : MonoBehaviour {

    public struct S1
    {
        public int hello;
    }

    public class C1
    {
        public int hello = 100;
    }

	// Use this for initialization
	void Start () {
        S1 s1 = new S1();
        Debug.Log("s1 value = " + s1.hello);

        S1 s2 = s1;
        s1.hello = 200;
        Debug.Log("s1 value = " + s1.hello);
        Debug.Log("s2 value = " + s2.hello);

        Action action = null;
        action();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
