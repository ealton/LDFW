using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Test7 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        MaskableGraphic[] list = GetComponentsInChildren<MaskableGraphic>();
        foreach (var element in list)
        {
            Debug.Log("element = " + element.transform.name);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
