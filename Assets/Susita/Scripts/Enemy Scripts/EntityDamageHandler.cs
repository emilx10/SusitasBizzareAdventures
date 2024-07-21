using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDamageHandler : EntityDamage
{
    EnemyMovementHandler _enemy => GetComponent<EnemyMovementHandler>();

    protected override void OnImpact()
    {
        _enemy.OnImpact();
    }
}
