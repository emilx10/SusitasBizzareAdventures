using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb => GetComponent<Rigidbody2D>();

    [Header("Movement")]
    [SerializeField] private float _movementSpeed;
    [SerializeField][ReadOnly] private float _currentSpeed;
    [SerializeField] private float _movementSpeedAccel, _movementSpeedLose, _maxMovementSpeed;
    [Header("Rotation")]
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _turnSpeedLose, _maxTurnSpeed;
    private float _vertical, _horizontal;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        LoseMovementSpeed();
        LoseRotation();
    }

    private void FixedUpdate()
    {
        Movement();
        RotateCar();
    }

    private void Movement()
    {
        _currentSpeed += _vertical * _movementSpeedAccel * Time.fixedDeltaTime;
        _rb.velocity = transform.up * _currentSpeed * _movementSpeed;
    }

    private void RotateCar()
    {
        _rb.AddTorque(_horizontal * _turnSpeed * Time.fixedDeltaTime);
    }

    private void LoseMovementSpeed()
    {
        if (Mathf.Abs(_currentSpeed) > _maxMovementSpeed)
        {
            _currentSpeed = Mathf.Sign(_currentSpeed) * _maxMovementSpeed;
        }
        if (_vertical == 0)
        {
            if (_currentSpeed > 1)
            {
                _currentSpeed -= _movementSpeedLose*Time.deltaTime;
            }
            else if (_currentSpeed < -1) 
            {
                _currentSpeed += _movementSpeedLose * Time.deltaTime;
            }
            else
            {
                _currentSpeed = 0;
            }
        }
    }

    private void LoseRotation()
    {
        if (Mathf.Abs(_rb.angularVelocity) > _maxTurnSpeed)
        {
            _rb.angularVelocity = Mathf.Sign(_rb.angularVelocity) * _maxTurnSpeed;
        }
        _rb.angularDrag = _horizontal == 0 ? _turnSpeedLose : 0;
    }

    private void GetInput()
    {
        _vertical = Input.GetAxis("Vertical");
        _horizontal = -Input.GetAxis("Horizontal");
    }
}
