using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class EntityHealth : MonoBehaviour
{
    [SerializeField] protected float _maxHealth;
    [SerializeField][ReadOnly] protected float _currentHealth;

   public void TakeDamage(float damage)
   {
        _currentHealth -= damage;
        if (_currentHealth < 0)
        {
            SceneManager.LoadScene("EndGameScene");
        }
   }

   public float GetHealthPercentage()
   {
        return _currentHealth/ _maxHealth;
   }
}
