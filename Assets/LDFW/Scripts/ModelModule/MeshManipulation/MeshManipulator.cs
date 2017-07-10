using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

namespace LDFW.Model
{

    public class MeshManipulator : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {

        public Camera                   raycastingCamera;
        private Mesh                    originalMesh;
        private Mesh                    currentMesh;
        private MeshCollider            meshCollider;
        private Vector3[]               originalVertices;
        private Vector3[]               currentVertices;
        private GameObject[]            vertexPositioner;
        private Vector3                 meshSize;
        private int                     vertexCount;

        private void Start()
        {
            MeshFilter meshFilter = GetComponent<MeshFilter>();

            if (meshFilter != null)
            {
                originalMesh = meshFilter.mesh;
                currentMesh = MeshGenerator.DuplicateMesh (originalMesh);
                meshFilter.mesh = currentMesh;
                meshSize = currentMesh.bounds.size;

                originalVertices = originalMesh.vertices;
                currentVertices = currentMesh.vertices;
                vertexCount = originalVertices.Length;

                vertexPositioner = new GameObject[vertexCount];

                foreach (var collider in GetComponents<Collider>())
                    DestroyImmediate (collider);

                meshCollider = gameObject.AddComponent<MeshCollider> ();

                if (raycastingCamera == null)
                    raycastingCamera = Camera.main;
            }
        }
        
        public void OnPointerDown (PointerEventData eventData)
        {
        }

        public void OnPointerUp (PointerEventData eventData)
        {
        }

        public void OnPointerClick (PointerEventData eventData)
        {
            Debug.Log ("OnPointerClick");
            RaycastHit hit;
            Ray ray = raycastingCamera.ScreenPointToRay(eventData.position);
            if (meshCollider.Raycast (ray, out hit, raycastingCamera.farClipPlane))
            {
                int closestVertexIndex = FindClosestVertexIndex(hit.point);
                
                if (vertexPositioner[closestVertexIndex] == null)
                {
                    GameObject newGO = new GameObject("vertex" + closestVertexIndex);
                    newGO.transform.SetParent (transform);
                    newGO.transform.localPosition = currentVertices[closestVertexIndex];
                }
            }
        }

        private int FindClosestVertexIndex(Vector3 position)
        {
            Debug.Log ("position = " + position.ToString ());
            int closestIndex = -1;
            float shortestDistance = float.MaxValue;
            float currentDistance;
            for (int i=0; i< vertexCount; i++)
            {
                currentDistance = (currentVertices[i] - position).magnitude;
                if (currentDistance < shortestDistance)
                {
                    shortestDistance = currentDistance;
                    closestIndex = i;
                }
            }

            return closestIndex;
        }
    }

}