using UnityEngine;
using System;
using System.Collections;

namespace LDFW.UserInput
{
    
    public class RaycasterHit
    {

        public Transform transform;
        public Collider collider;
        public Rigidbody rigidbody;

        public Vector3 point;
        public Vector3 normal;
        public Vector3 barycentricCoordinate;
        public Vector2 lightmapCoord;
        public Vector2 textureCoord;
        public Vector2 textureCoord2;
        
        public float distance;
        public int triangleIndex;



        public RaycasterHit(RaycastHit raycastHit)
        {
            transform = raycastHit.transform;
            collider = raycastHit.collider;
            rigidbody = raycastHit.rigidbody;

            point = raycastHit.point;
            normal = raycastHit.normal;
            barycentricCoordinate = raycastHit.barycentricCoordinate;
            textureCoord = raycastHit.textureCoord;
            textureCoord2 = raycastHit.textureCoord2;
            distance = raycastHit.distance;
            triangleIndex = raycastHit.triangleIndex;

            try
            {
                lightmapCoord = raycastHit.lightmapCoord;
            } 
            catch (Exception e)
            {
            }
        }

        public RaycasterHit()
        {
        }
    }

}