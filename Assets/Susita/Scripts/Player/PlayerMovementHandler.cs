using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementHandler : MonoBehaviour
{
    protected Rigidbody2D _rb;

    [Header("Settings")]
    [SerializeField] private SOPlayerMovement settings;

    [Header("Movement")]
    [SerializeField][ReadOnly] protected float _currentSpeed;
    [SerializeField] private Collider2D _runOverCollider;

    [Header("Rotation")]
    protected float _vertical, _horizontal;
    private float _speedMultiplier = 1f;
    private float _targetSpeedMultiplier = 1f;
    private Dictionary<string, float> _speedModifiers = new Dictionary<string, float>();

    [SerializeField] private GameObject _exitPoint;

    [SerializeField] ChangeScenesManager _changeScenesManager;

    protected void Start()
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
        _currentSpeed += _vertical * settings.movementSpeedAccel * Time.fixedDeltaTime;
        float effectiveSpeedMultiplier = _speedMultiplier * (_currentSpeed > 0 ? 1 : settings.reverseModifier);
        _rb.velocity = transform.up * _currentSpeed * settings.movementSpeed * effectiveSpeedMultiplier;
    }

    public float GetSpeedPercentage()
    {
        return Mathf.Abs(_currentSpeed) / settings.maxMovementSpeed;
    }

    private void RotateCar()
    {
        if (_currentSpeed != 0)
        {
            _rb.AddTorque(_vertical >= 0 ? _horizontal : -_horizontal * settings.turnSpeed * Time.fixedDeltaTime * _speedMultiplier);
        }
    }

    private void PlayerRunOver()
    {
        _runOverCollider.enabled = _currentSpeed >= settings.runOverSpeed;
    }

    private void LoseMovementSpeed()
    {
        if (Mathf.Abs(_currentSpeed) > settings.maxMovementSpeed)
        {
            _currentSpeed = Mathf.Sign(_currentSpeed) * settings.maxMovementSpeed;
        }
        if (_vertical == 0 || _vertical>0 && _currentSpeed<0 || _vertical < 0 && _currentSpeed > 0)
        {
            if (_currentSpeed > 1)
            {
                _currentSpeed -= settings.movementSpeedLose * Time.deltaTime;
            }
            else if (_currentSpeed < -1)
            {
                _currentSpeed += settings.movementSpeedLose * Time.deltaTime;
            }
            else
            {
                _currentSpeed = 0;
            }
        }
    }

    public void DivideSpeedInstantly(float mult)
    {
        _currentSpeed *= mult;
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

    private void OnDestroy()
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
        if (Mathf.Abs(_rb.angularVelocity) > settings.maxTurnSpeed * _speedMultiplier)
        {
            _rb.angularVelocity = Mathf.Sign(_rb.angularVelocity) * settings.maxTurnSpeed * _speedMultiplier;
        }
        _rb.angularDrag = (_horizontal == 0 || _currentSpeed==0) ? settings.turnSpeedLose : 0;
    }

    protected virtual void GetInput()
    {
        _vertical = Input.GetAxis("Vertical");
        _horizontal = -Input.GetAxis("Horizontal");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PickUp"))
        {
            if (collision.gameObject.TryGetComponent<Pickup>(out Pickup pickup))
            {
                if (gameObject.CompareTag("Player"))
                {
                    pickup.Collect();
                }
            }
        }

        if (collision.gameObject.CompareTag("Mud"))
        {
            AddSpeedModifier("Mud", settings.mudModifier); // Example modifier for mud
        }

        if (collision.gameObject.CompareTag("Exit"))
        {
            _changeScenesManager.ViewPanels(1);
        }

        if (collision.gameObject.CompareTag("ExitLevel2"))
        {
            _changeScenesManager.ViewPanels(3);
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
