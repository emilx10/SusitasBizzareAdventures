using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : EntityHealth
{
    [SerializeField] private float _playerHealth;
    [SerializeField] private int _overHeatTimer;
    [SerializeField] private bool isCooledDown = true;
    void Start()
    {
        
    }

    void Update()
    {
        if (Time.deltaTime>=_overHeatTimer)
        {
            while (!isCooledDown)
            {
                OverHeating();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _playerHealth -= 10;
        }
    }

    private void OverHeating()
    {
       _playerHealth -= _playerHealth*0.05f;
    }
}
