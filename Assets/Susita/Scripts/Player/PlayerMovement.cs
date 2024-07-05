using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;

    [Header("Movement")]
    [SerializeField] private float _movementSpeed;
    [SerializeField][ReadOnly] private float _currentSpeed;
    [SerializeField] private float _movementSpeedAccel, _movementSpeedLose, _maxMovementSpeed, _runOverSpeed, _reverseModifier, _mudModifier;
    [SerializeField] private Collider2D _runOverCollider;
    [Header("Rotation")]
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _turnSpeedLose, _maxTurnSpeed;
    private float _vertical, _horizontal;
    private float _speedMultiplier = 1f;
    private float _targetSpeedMultiplier = 1f;
    private Dictionary<string, float> _speedModifiers = new Dictionary<string, float>();

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetInput();
        LoseMovementSpeed();
        LoseRotation();
        PlayerRunOver();
    }

    private void FixedUpdate()
    {
        Movement();
        RotateCar();
    }

    private void Movement()
    {
        _currentSpeed += _vertical * _movementSpeedAccel * Time.fixedDeltaTime;
        float effectiveSpeedMultiplier = _speedMultiplier * (_currentSpeed > 0 ? 1 : _reverseModifier);
        _rb.velocity = transform.up * _currentSpeed * _movementSpeed * effectiveSpeedMultiplier;
    }

    public float GetSpeedPercentage()
    {
        return Mathf.Abs(_currentSpeed)/_maxMovementSpeed;
    }

    private void RotateCar()
    {
        _rb.AddTorque(_horizontal * _turnSpeed * Time.fixedDeltaTime * _speedMultiplier);
    }

    private void PlayerRunOver()
    {
        _runOverCollider.enabled = _currentSpeed >= _runOverSpeed;
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
                _currentSpeed -= _movementSpeedLose * Time.deltaTime;
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

    public void AddSpeedModifier(string modifierName, float modifierValue)
    {
        if (!_speedModifiers.ContainsKey(modifierName))
        {
            _speedModifiers.Add(modifierName, modifierValue);
            UpdateSpeedMultiplier();
        }
    }

    public void RemoveSpeedModifier(string modifierName)
    {
        if (_speedModifiers.ContainsKey(modifierName))
        {
            _speedModifiers.Remove(modifierName);
            UpdateSpeedMultiplier();
        }
    }

    private void UpdateSpeedMultiplier()
    {
        _targetSpeedMultiplier = 1f;
        foreach (float modifierValue in _speedModifiers.Values)
        {
            _targetSpeedMultiplier *= modifierValue;
        }

        StopAllCoroutines();
        StartCoroutine(LerpSpeedMultiplier(_speedMultiplier, _targetSpeedMultiplier, 1f));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator LerpSpeedMultiplier(float startValue, float endValue, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            _speedMultiplier = Mathf.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        _speedMultiplier = endValue;
    }

    private void LoseRotation()
    {
        if (Mathf.Abs(_rb.angularVelocity) > _maxTurnSpeed * _speedMultiplier)
        {
            _rb.angularVelocity = Mathf.Sign(_rb.angularVelocity) * _maxTurnSpeed * _speedMultiplier;
        }
        _rb.angularDrag = _horizontal == 0 ? _turnSpeedLose : 0;
    }

    private void GetInput()
    {
        _vertical = Input.GetAxis("Vertical");
        _horizontal = -Input.GetAxis("Horizontal");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PickUp"))
        {
            if (collision.gameObject.TryGetComponent<Pickup>(out Pickup pickup))
            {
                pickup.Collect();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mud"))
        {
            AddSpeedModifier("Mud", _mudModifier); // Example modifier for mud
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mud"))
        {
            RemoveSpeedModifier("Mud");
        }
    }
}
