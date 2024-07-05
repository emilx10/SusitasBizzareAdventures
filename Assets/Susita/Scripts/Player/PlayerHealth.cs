using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : EntityHealth
{
    [SerializeField] private float _heatDamage, _chillTime, _heatDamageRepeatTime;

    void Start()
    {
        HealPlayer();
        InvokeRepeating(nameof(OverHeating), 10,1);
    }

    [ContextMenu("Heal")]
    private void HealPlayer()
    {
        _currentHealth = _maxHealth;
    }

    private void OverHeating()
    {
       _currentHealth -= _maxHealth * 0.05f;
    }

    [ContextMenu("Chill Bruh")]
    private void HappyHappyJoyJoy()
    {
        CancelInvoke(nameof(OverHeating));
        InvokeRepeating(nameof(OverHeating), 10, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tree"))
        {
            TakeDamage(5);
        }
    }
}
