using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Rigidbody2D _rb => GetComponent<Rigidbody2D>();
    private SpriteRenderer _spriteRenderer=> GetComponent<SpriteRenderer>();
    private PlayerHealthHandler _playerHealth => GetComponent<PlayerHealthHandler>();

    [SerializeField] private float _angularSpeedMin;

    [SerializeField] private PlayerMovementAnimations[] _playerMovementAnimations = new PlayerMovementAnimations[3];

    // Update is called once per frame
    void Update()
    {
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        _spriteRenderer.sprite = SelectMovementState(SelectHealthState());
    }

    private PlayerMovementAnimations SelectHealthState()
    {
        if (_playerHealth == null) return _playerMovementAnimations[0];

        float n = _playerHealth.GetHealthPercentage();
        if (n > 0.666f)
        {
            return _playerMovementAnimations[0];
        }
        else if(n < 0.333f)
        {
            return _playerMovementAnimations[2];
        }
        else
        {
            return _playerMovementAnimations[1];
        }
    }

    private Sprite SelectMovementState(PlayerMovementAnimations pma)
    {
        if (_rb.angularVelocity > _angularSpeedMin)
        {
            return pma.Left; 
        }
        else if (_rb.angularVelocity < -_angularSpeedMin)
        {
            return pma.Right;
        }
        else
        {
            return pma.Forward;
        }
    }

    [System.Serializable]
    private class PlayerMovementAnimations
    {
        [SerializeField] private string _name;
        [SerializeField] public Sprite Forward, Left, Right;
    }
}
