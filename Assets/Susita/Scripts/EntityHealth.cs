using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    private int _health;

    private void TakingDamage(int health , int damage)
    {
        health-=damage;
    }
}
