using UnityEngine;


namespace Core
{
    [RequireComponent(typeof(Player), typeof(DashPhysicsProcessor), 
        typeof(CapsuleCollider))]
    public class Character : MonoBehaviour
    {
        [SerializeField] private Movable _movable;
        [SerializeField] private ThirdPersonCamera _thirdPersonCamera;
        
        private Player _player;
        private DashPhysicsProcessor _dashPhysicsProcessor;
        private CapsuleCollider _collider;
        
    
        public void ReceiveMovementInput(Vector2 input)
        {
            if (input.magnitude == 0)
            {
                _movable.ReceiveMovementDirection(input);
                return;
            }
        
            var cameraLookDirection3D = _thirdPersonCamera.LookDirection;
            var cameraLookDirectionFlat = new Vector3(cameraLookDirection3D.x, 0, cameraLookDirection3D.z).normalized;
            var inputDirection3D = new Vector3(input.x, 0, input.y);
            var inputRotationAngle = Vector3.SignedAngle(Vector3.forward, inputDirection3D, Vector3.up);

            var movementDirection3D = Quaternion.AngleAxis(inputRotationAngle, Vector3.up) * cameraLookDirectionFlat;
            var movementDirectionFlat = new Vector2(movementDirection3D.x, movementDirection3D.z);
            _movable.ReceiveMovementDirection(movementDirectionFlat * input.magnitude);
        }


        public void Dash()
        {
            var dashDirection = _movable.MovementDirection.magnitude > 0
                ? new Vector3(_movable.MovementDirection.x, 0, _movable.MovementDirection.y)
                : _movable.transform.forward;
        
            _dashPhysicsProcessor.Process(dashDirection, _player.DashDistance, out var dashDelta,
                out var passedThruObjects);

            _movable.MoveInstantly(transform.position + dashDelta);
            
            foreach (var passedThruObject in passedThruObjects)
            {
                if (!passedThruObject.TryGetComponent<Player>(out var hittableComponent))
                    continue;
                
                hittableComponent.TakeHit();
            }
        }


        private void Awake()
        {
            _player = GetComponent<Player>();
            _dashPhysicsProcessor = GetComponent<DashPhysicsProcessor>();
            _collider = GetComponent<CapsuleCollider>();
        }


        private void Start()
        {
            _movable.Setup(_player.MoveSpeed);
            _dashPhysicsProcessor.Setup(_collider);
        }
    }
}