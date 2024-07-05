using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairPickup : Pickup
{
    [SerializeField] private float _healAmount;
    public override void Collect()
    {
        GameManager.Instance.GetPlayerHealth().HealPlayerAmount(_healAmount);
        Destroy(gameObject);
    }
}
