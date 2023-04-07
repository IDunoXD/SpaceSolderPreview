using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
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

        if(joystick.input.x > 0.001f && joystick.input.x < rotateArroundJoystickThreshold)
        {
            _movementDirection.x = 0;
            _rotationSpeedPercentage = Mathf.InverseLerp(0.001f, rotateArroundJoystickThreshold, joystick.input.x);
            _rotation = new Vector3(0, rotationSpeed * _rotationSpeedPercentage * Time.deltaTime, 0);
            transform.Rotate(_rotation, Space.Self);
        }
        else if(joystick.input.x < -0.001f && joystick.input.x > -rotateArroundJoystickThreshold)
        {
            _movementDirection.x = 0;
            _rotationSpeedPercentage = Mathf.InverseLerp(-rotateArroundJoystickThreshold, -0.001f, joystick.input.x);
            _rotation = new Vector3(0, -rotationSpeed * _rotationSpeedPercentage * Time.deltaTime, 0);
            transform.Rotate(_rotation, Space.Self);
        }

        _movementDirection = transform.rotation * _movementDirection;
        _rigidbody.velocity = _movementDirection * movementSpeed * Time.deltaTime;

        _animator.SetFloat("Straight_Movement", joystick.input.y);
        _animator.SetFloat("Side_Movement", joystick.input.x);
    }
}
