using UnityEngine;
using System.Collections;

public class BendObject : MonoBehaviour {
    
    // Public Variables
    public Vector3 bendAxisFlag = Vector3.zero;
    public Vector3 rotationVector = Vector3.one * 90;
    public Vector3 bendReferencePoint = Vector3.zero;

    public Vector2 bendXLimit = new Vector2 (-90f, 90f);
    public Vector2 bendYLimit = new Vector2 (-90f, 90f);
    public Vector2 bendZLimit = new Vector2 (-90f, 90f);
    
    public bool isReady = false;

    // Private and Protected Variables
    protected Mesh mesh;
    public Vector3[] originalVertices;
    public Vector3[] currentVertices;
    public Vector3 meshDimension = Vector3.one;
    public GameObject temp;
    

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
    }

    protected void Start () {
        /*
        Debug.Log ("Vertices count = " + originalVertices.Length);
        for (int i = 0; i < originalVertices.Length; i++) {
            Debug.Log ("Vertex " + i + " = " + mesh.vertices[i].x + ", " + mesh.vertices[i].y + ", " + mesh.vertices[i].z);
            GameObject newGameObject = Instantiate (temp.gameObject) as GameObject;
            newGameObject.name = "Vertex" + i;
            newGameObject.SetActive (true);
            newGameObject.transform.parent = transform;
            newGameObject.transform.localPosition = originalVertices[i];
        }
        */

        isReady = true;
        //InvokeRepeating ("BendImmediately", 1f, 0.5f);
    }
    

    public void SetBendingAxis (bool useXAxis, bool useYAxis, bool useZAxis) {
        bendAxisFlag.x = useXAxis ? 1 : 0;
        bendAxisFlag.y = useYAxis ? 1 : 0;
        bendAxisFlag.z = useZAxis ? 1 : 0;
    }

    public virtual void BendImmediately () {
        for (int i=0; i<currentVertices.Length; i++) {
            currentVertices[i] = originalVertices[i];
        }
        
        Vector3 currentPoint = Vector3.zero;
        Vector3 translatedPoint = Vector3.zero;
        

        for (int i=0; i<currentVertices.Length; i++) {
            currentPoint = currentVertices[i];

            if (bendAxisFlag.x != 0) {
                
                float referenceZValue = meshDimension.z * bendReferencePoint.z;
                float currentDistance = 0f;
                float rotateValue = 0f;
                
                currentDistance = originalVertices[i].z + referenceZValue;
                rotateValue = rotationVector.x * (currentDistance / meshDimension.z);
                rotateValue = Mathf.Min (bendXLimit.y, Mathf.Max (bendXLimit.x, rotateValue));

                translatedPoint.x = currentPoint.x;
                translatedPoint.z = currentPoint.x * Mathf.Cos (rotateValue * Mathf.Deg2Rad) - currentPoint.y * Mathf.Sin (rotateValue * Mathf.Deg2Rad);
                translatedPoint.y = currentPoint.y * Mathf.Cos (rotateValue * Mathf.Deg2Rad) + currentPoint.x * Mathf.Sin (rotateValue * Mathf.Deg2Rad);

                currentPoint = translatedPoint;
            }

            if (bendAxisFlag.y != 0) {
                
                float referenceXValue = meshDimension.x * bendReferencePoint.x;
                float currentDistance = 0f;
                float rotateValue = 0f;
                
                currentDistance = originalVertices[i].x - referenceXValue;
                rotateValue = rotationVector.y * (currentDistance / meshDimension.x);
                rotateValue = Mathf.Min (bendYLimit.y, Mathf.Max (bendYLimit.x, rotateValue));

                translatedPoint.x = currentPoint.x * Mathf.Cos (rotateValue * Mathf.Deg2Rad) - currentPoint.z * Mathf.Sin (rotateValue * Mathf.Deg2Rad);
                translatedPoint.z = currentPoint.z * Mathf.Cos (rotateValue * Mathf.Deg2Rad) + currentPoint.x * Mathf.Sin (rotateValue * Mathf.Deg2Rad);
                translatedPoint.y = currentPoint.y;

                currentPoint = translatedPoint;
            }


            if (bendAxisFlag.z != 0) {
                
                float referenceZValue = meshDimension.x * bendReferencePoint.x;
                float currentDistance = 0f;
                float rotateValue = 0f;
                
                currentDistance = originalVertices[i].x + referenceZValue;
                
                rotateValue = rotationVector.z * (currentDistance / meshDimension.x);
                rotateValue = Mathf.Min (bendZLimit.y, Mathf.Max (bendZLimit.x, rotateValue));

                translatedPoint.x = currentPoint.x * Mathf.Cos (rotateValue * Mathf.Deg2Rad) - currentPoint.y * Mathf.Sin (rotateValue * Mathf.Deg2Rad);
                translatedPoint.y = currentPoint.y * Mathf.Cos (rotateValue * Mathf.Deg2Rad) + currentPoint.x * Mathf.Sin (rotateValue * Mathf.Deg2Rad);
                translatedPoint.z = currentPoint.z;

                currentPoint = translatedPoint;
            }

            currentVertices[i] = currentPoint;
        }

        mesh.vertices = currentVertices;
        mesh.RecalculateBounds ();
    }
}