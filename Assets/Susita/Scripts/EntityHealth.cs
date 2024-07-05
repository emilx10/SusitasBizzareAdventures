using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityHealth : MonoBehaviour
{
    [SerializeField] protected float _maxHealth;
    [SerializeField][ReadOnly] protected float _currentHealth;

   public void TakeDamage(float damage)
   {
        _currentHealth -= damage;
   }

    public float GetHealthPercentage()
    {
        return _currentHealth/ _maxHealth;
    }
}
