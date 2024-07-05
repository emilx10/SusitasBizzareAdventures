using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolerPickup : Pickup
{
    [SerializeField] float _chillAmount, _immunityTime;
    public override void Collect()
    {
        GameManager.Instance.GetPlayerHealth().ChillAmount(_chillAmount,_immunityTime);
        Destroy(gameObject); 
    }
}
