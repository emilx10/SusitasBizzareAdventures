using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : EntityDamage
{
    EnemyFollowMovement _enemy => GetComponent<EnemyFollowMovement>();

    protected override void OnImpact()
    {
        _enemy.OnImpact();
    }
}
