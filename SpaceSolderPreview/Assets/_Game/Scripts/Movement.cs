using UnityEngine;

public class Movement : MonoBehaviour
{
    private const float MAX_ROTATION_VALUE = 1f;

    [SerializeField] private Joystick joystick;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float rotateArroundJoystickThreshold = 0.7f;

    private Rigidbody _rigidbody;
    private Animator _animator;
    private Vector3 _movementDirection;
    private Vector3 _rotation;
    private float _rotationSpeedPercentage;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _movementDirection = new Vector3(joystick.input.x, 0, joystick.input.y);

        if(joystick.input.x > rotateArroundJoystickThreshold)
        {
            RotateInBounds(rotateArroundJoystickThreshold, MAX_ROTATION_VALUE, rotationSpeed);
        }
        else if(joystick.input.x < -rotateArroundJoystickThreshold)
        {
            RotateInBounds(-rotateArroundJoystickThreshold, -MAX_ROTATION_VALUE, -rotationSpeed);
        }

        _movementDirection = transform.rotation * _movementDirection;
        _rigidbody.velocity = _movementDirection * movementSpeed * Time.deltaTime;

        _animator.SetFloat("Straight_Movement", joystick.input.y);
        _animator.SetFloat("Side_Movement", joystick.input.x);
    }

    private void RotateInBounds(float from, float to, float speed)
    {
        _movementDirection.x = 0;
        _rotationSpeedPercentage = Mathf.InverseLerp(from, to, joystick.input.x);
        _rotation = new Vector3(0, speed * _rotationSpeedPercentage * Time.deltaTime, 0);
        transform.Rotate(_rotation, Space.Self);
    }
}
