using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Test4 : MonoBehaviour {

    private RawImage image;
    private Texture2D texture;
    private float rand1;
    private float rand2;
    private Vector2 currentPixel;
    private Vector2 point0 = Vector2.zero;
    private Vector2 point1 = new Vector2(100, 200);
    private Vector2 point2 = new Vector2(200, 100);

    private void Awake()
    {
        image = GetComponent<RawImage>();
        texture = new Texture2D(200, 200);
        for (int i = 0; i < 200; i++)
        {
            for (int j = 0; j < 200; j++)
            {
                texture.SetPixel(i, j, Color.white);
            }
        }
        texture.Apply();
        image.texture = texture as Texture;
    }


    private void Update()
    {
        rand1 = Random.value;
        rand2 = Random.value;

        currentPixel = (1 - Mathf.Sqrt(rand1)) * point0 + Mathf.Sqrt(rand1) * (1 - rand2) * point1 + Mathf.Sqrt(rand1) * rand2 * point2;
        texture.SetPixel(Mathf.RoundToInt(currentPixel.x), Mathf.RoundToInt(currentPixel.y), Color.black);
        texture.Apply();
    }



}
