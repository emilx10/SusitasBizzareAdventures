using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  EntityDamage : MonoBehaviour
{
    [SerializeField] protected float _damage;

    PlayerHealthHandler _playerHealth;

    GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _playerHealth = _gameManager.GetPlayerHealth();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnImpact();
            _playerHealth.TakeDamage(_damage);
        }
    }

    protected abstract void OnImpact();
}
