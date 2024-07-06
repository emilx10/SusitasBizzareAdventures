using UnityEngine;

public abstract class EntityHealth : MonoBehaviour
{
    [SerializeField] protected float _maxHealth;
    [SerializeField][ReadOnly] protected float _currentHealth;

   public void TakeDamage(float damage)
   {
        _currentHealth -= damage;
        if (_currentHealth < 0)
        {
            Die();
        }
   }

    public abstract void Die();

   public float GetHealthPercentage()
   {
        return _currentHealth/ _maxHealth;
   }
}
