using System.Collections.Generic;
using UnityEngine;


namespace Core
{
    public class DashPhysicsProcessor : MonoBehaviour
    {
        [SerializeField] private LayerMask _dashObstaclesLayerMask;
        [SerializeField] private LayerMask _passThruObjectsLayerMask;

        [SerializeField] private float _castColliderClearance;
        
        private CapsuleCollider _collider;
        private CastCapsuleColliderData _castColliderData;
        
        
        public void Setup(CapsuleCollider collider)
        {
            _collider = collider;
            _castColliderData = GetCastColliderData();
        }
        
        
        public void Process(Vector3 direction, float distance, out Vector3 dashDelta, out List<GameObject> passedThruObjects)
        {
            dashDelta = GetDashDelta(direction, distance);
            passedThruObjects = GetDashPassedThruObjects(direction, dashDelta.magnitude);
        }


        private Vector3 GetDashDelta(Vector3 dashDirection, float dashDistance)
        {
            if (ColliderCast(transform.position, dashDirection, dashDistance, _dashObstaclesLayerMask, out var raycastHit))
                dashDistance = raycastHit.distance;

            return dashDirection * dashDistance;
        }


        private List<GameObject> GetDashPassedThruObjects(Vector3 castDirection, float castDistance)
        {
            var passedThruObjects = new List<GameObject>();
            var offsetStep = 0.01f;
            
            var castFromPosition = transform.position;
            var castToPosition = castFromPosition + castDirection * castDistance;
                
            while (ColliderCast(castFromPosition, castDirection, castDistance, _passThruObjectsLayerMask, out var raycastHit))
            {
                var castDistanceWithOffset = raycastHit.distance + offsetStep;
                castDistance -= castDistanceWithOffset;
                castFromPosition += castDirection * castDistanceWithOffset;

                // condition to cut off objects not passed through
                var colliderCenter = raycastHit.collider.bounds.center;
                var colliderCenterFlat = new Vector3(colliderCenter.x, 0, colliderCenter.z);
                var castFromPositionFlat = new Vector3(castFromPosition.x, 0, castFromPosition.z);
                var castToPositionFlat = new Vector3(castToPosition.x, 0, castToPosition.z);
                var distanceToColliderCenterFlat = Vector3.Distance(castFromPositionFlat, colliderCenterFlat);
                var distanceToCastToPositionFlat = Vector3.Distance(castFromPositionFlat, castToPositionFlat);
                if (distanceToColliderCenterFlat > distanceToCastToPositionFlat
                && raycastHit.collider.bounds.Contains(castToPosition))
                    continue;
                
                passedThruObjects.Add(raycastHit.transform.gameObject);
            }

            return passedThruObjects;
        }


        private bool ColliderCast(Vector3 position, Vector3 direction, float distance, LayerMask layerMask, out RaycastHit raycastHit)
        {
            return Physics.CapsuleCast(_castColliderData.TopSphereCenter + position,
                _castColliderData.BottomSphereCenter + position, _castColliderData.Radius, direction,
                out raycastHit, distance, layerMask);
        }


        private CastCapsuleColliderData GetCastColliderData()
        {
            return new CastCapsuleColliderData(_collider, _castColliderClearance);
        }


        private class CastCapsuleColliderData
        {
            public float Radius { get; }
            public Vector3 TopSphereCenter { get; }
            public Vector3 BottomSphereCenter { get; }


            public CastCapsuleColliderData(CapsuleCollider collider, float castClearance)
            {
                var colliderCenter = collider.center;

                Radius = collider.radius - castClearance;
                var capsuleSphereCenterDelta = new Vector3(0, collider.height / 2 - Radius);
                TopSphereCenter = capsuleSphereCenterDelta + colliderCenter;
                BottomSphereCenter = capsuleSphereCenterDelta + colliderCenter;
            }
        }
    }
}
