using UnityEngine;
using System.Collections;

public class test1 : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () {
        yield return new WaitForSeconds(2);


        Debug.Log("Set time scale = 0");
        Time.timeScale = 0;
        yield return new WaitForSeconds(2);


        Debug.Log("Set time scale =1");
        Time.timeScale = 1;

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
