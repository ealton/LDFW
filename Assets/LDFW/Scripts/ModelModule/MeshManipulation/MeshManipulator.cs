using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

namespace LDFW.Model
{


    public class MeshManipulator : MonoBehaviour, IPointerClickHandler
    {

        public Camera                   raycastingCamera;
        public Transform                draggingSphere;

        private Transform               vertexSphere;
        private int                     vertexSphereReferencingIndex;
        private Mesh                    originalMesh;
        private Mesh                    currentMesh;
        private MeshCollider            meshCollider;
        private Vector3[]               originalVertices;
        private Vector3[]               currentVertices;
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

                foreach (var collider in GetComponents<Collider>())
                    DestroyImmediate (collider);

                meshCollider = gameObject.AddComponent<MeshCollider> ();

                if (raycastingCamera == null)
                    raycastingCamera = Camera.main;
            }
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (vertexSphere != null)
            {
                
                currentVertices[vertexSphereReferencingIndex] = vertexSphere.localPosition;
                currentMesh.vertices = currentVertices;
            }
        }
#endif

        public void OnPointerClick (PointerEventData eventData)
        {
            Debug.Log ("OnPointerClick");
            RaycastHit hit;
            Ray ray = raycastingCamera.ScreenPointToRay(eventData.position);
            if (meshCollider.Raycast (ray, out hit, raycastingCamera.farClipPlane))
            {
                Debug.Log("hit point = " + hit.point);
                if (vertexSphere != null)
                    Destroy(vertexSphere.gameObject);

                //vertexSphereReferencingIndex = closestVertexIndex;
                vertexSphere = Instantiate(draggingSphere.gameObject).transform;
                vertexSphere.name = "vertex";
                vertexSphere.SetParent(transform);
                vertexSphere.position = hit.point;
                vertexSphere.gameObject.SetActive(true);

                vertexSphereReferencingIndex = FindClosestVertexIndex(vertexSphere.localPosition);
                vertexSphere.localPosition = currentVertices[vertexSphereReferencingIndex];
                
                Debug.Log("Closest point = " + currentVertices[vertexSphereReferencingIndex].ToString());
                
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

        public Mesh GetCurrentMesh()
        {
            return currentMesh;
        }
    }

}