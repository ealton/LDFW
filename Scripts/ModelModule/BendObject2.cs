using UnityEngine;
using System.Collections;

public class BendObject2 : MonoBehaviour {


    // Bend curve used to control shape
    public AnimationCurve xAxisBendCruve;
    public AnimationCurve yAxisBendCurve;
    public AnimationCurve zAxisBendCurve;

    public bool isReady = false;

    // Private and Protected Variables
    protected Mesh mesh;
    public Vector3[] originalVertices;
    public Vector3[] currentVertices;
    public Vector3 meshDimension = Vector3.one;
    private float originalXLength;
    

    protected void Awake () {
        mesh = GetComponent<MeshFilter> ().mesh;
        meshDimension = new Vector3 (mesh.bounds.size.x, mesh.bounds.size.y, mesh.bounds.size.z);
        originalVertices = new Vector3[mesh.vertices.Length];

        for (int i = 0; i < originalVertices.Length; i++) {
            originalVertices[i] = mesh.vertices[i];
        }

        currentVertices = new Vector3[mesh.vertices.Length];
        for (int i = 0; i < currentVertices.Length; i++) {
            currentVertices[i] = mesh.vertices[i];
        }

        originalXLength = mesh.bounds.size.x;
    }

    protected void Start () {
        BendImmediately (xAxisBendCruve, yAxisBendCurve, zAxisBendCurve);
        //BendToNoodleParentAnimation ();
    }
    
    public virtual void BendImmediately (AnimationCurve xaxis, AnimationCurve yaxis, AnimationCurve zaxis) {
        for (int i=0; i<currentVertices.Length; i++) {
            currentVertices[i] = originalVertices[i];
        }
        
        Vector3 currentPoint = Vector3.zero;
        Vector3 translatedPoint = Vector3.zero;
        

        for (int i=0; i<currentVertices.Length; i++) {
            currentVertices[i].x = xaxis.Evaluate (originalVertices[i].x / originalXLength * 2) * originalXLength / 2;
            currentVertices[i].y = yaxis.Evaluate (originalVertices[i].x / originalXLength * 2) + originalVertices[i].y;
            currentVertices[i].z = zaxis.Evaluate (originalVertices[i].x / originalXLength * 2) + originalVertices[i].z;
        }

        mesh.vertices = currentVertices;
        mesh.RecalculateBounds ();
    }

    public void BendToNoodleParentAnimation () {
        //NoodleAnimationCurve animationCurve = NoodleParentController.instance.GetRandomCurve ();
        //BendImmediately (animationCurve.xAxis, animationCurve.yAxis, animationCurve.zAxis);
    }
}