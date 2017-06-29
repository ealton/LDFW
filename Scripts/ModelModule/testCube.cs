using UnityEngine;
using System.Collections;

public class testCube : MonoBehaviour {

    


    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;

        foreach (var vec in mesh.uv)
        {
            //Debug.Log (ToPreciseString (vec));
        }

        Debug.Log (mesh.vertices.Length);
        Debug.Log (mesh.uv.Length);
    }





    private string ToPreciseString(Vector3 vec)
    {
        return string.Format ("{0,4}, {1,4}, {2,4}", vec.x, vec.y, vec.z);
    }
}
