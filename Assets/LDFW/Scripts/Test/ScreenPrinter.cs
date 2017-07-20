using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenPrinter : MonoBehaviour {

    public static ScreenPrinter     instance;
    public Text                     uiText;
    private string                  temp;


    private void Awake()
    {
        instance = this;
    }

    public void Log(string text)
    {
        temp = text + "\n" + uiText.text;
        uiText.text = temp.Substring(0, Mathf.Min(200, temp.Length));
    }
    
}
