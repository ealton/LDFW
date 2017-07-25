using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Test6 : MonoBehaviour {

    public Transform image;


    private void Start()
    {
        Ray ray = new Ray(image.position + new Vector3(0, 0, -1), new Vector3(0, 0, 1));
        RaycastHit2D[] hit = Physics2D.RaycastAll(image.position + new Vector3(0, 0, -1), new Vector3(0, 0, 1), 10);
        if (hit.Length > 0)
            Debug.Log("HIT: " + hit[0].transform.name);
        else
            Debug.Log("NOT HIT");
    }
    
    public void Test()
    {
        Canvas canvas = GetComponent<Canvas>();
    }
}
