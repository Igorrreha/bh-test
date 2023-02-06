using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Movable : MonoBehaviour
{
    [SerializeField] private bool _rotateWhenMoving;

    private float _movementSpeed;
    private Vector2 _movementVelocity;
    
    private Rigidbody _physicsBody;
    
    public Vector2 MovementDirection => _movementVelocity.normalized;


    public void Setup(float movementSpeed)
    {
        _movementSpeed = movementSpeed;
    }
    

    public void ReceiveMovementDirection(Vector2 direction)
    {
        UpdateMovementVelocity(direction);
    }


    public void MoveInstantly(Vector3 position)
    {
        transform.position = position;
    }
    
    
    private void Awake()
    {
        _physicsBody = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        ProcessMovement();
    }


    private void ProcessMovement()
    {
        if (_movementVelocity.magnitude == 0)
            return;

        var movementVelocity3D = new Vector3(_movementVelocity.x, 0, _movementVelocity.y);
        _physicsBody.MovePosition(transform.localPosition + Time.fixedDeltaTime * movementVelocity3D);

        if (_rotateWhenMoving)
            transform.LookAt(transform.position + movementVelocity3D);
    }
    

    private void UpdateMovementVelocity(Vector2 direction)
    {
        _movementVelocity = _movementSpeed * (direction.magnitude > 1 ? direction.normalized : direction);
    }
}
