using System;
using UnityEngine;


public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Camera _camera;
    
    [SerializeField] private IntRange _verticalAngleOfViewLimit;
    [SerializeField] private Vector2 _orbitalMovementSensitivity;
    
    [SerializeField] private bool _invertX;
    [SerializeField] private bool _invertY;

    private Vector2 _orbitalMovementVelocity;

    public Vector3 LookDirection => _camera.transform.forward;


    public void UpdateOrbitalMovementVelocity(Vector2 inputDirection)
    {
        _orbitalMovementVelocity = inputDirection * _orbitalMovementSensitivity;
    }


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void LateUpdate()
    {
        ProcessOrbitalMovement();
        _camera.transform.LookAt(_target);
    }


    private void ProcessOrbitalMovement()
    {
        if (_orbitalMovementVelocity.magnitude == 0)
            return;

        var inversionVector = new Vector2(_invertX ? -1 : 1, _invertY ? -1 : 1);
        var rotationDelta = new Vector3(_orbitalMovementVelocity.y * inversionVector.y,
            _orbitalMovementVelocity.x * inversionVector.x, 0);
        
        var newRotation = transform.localRotation.eulerAngles + rotationDelta;
        newRotation.x = Mathf.Clamp(newRotation.x, _verticalAngleOfViewLimit.Min, _verticalAngleOfViewLimit.Max);

        transform.localRotation = Quaternion.Euler(newRotation);
    }


    [Serializable]
    private class IntRange
    {
        [SerializeField] private int _min;
        [SerializeField] private int _max;
        
        public int Min => _min;
        public int Max => _max;
    }
}
