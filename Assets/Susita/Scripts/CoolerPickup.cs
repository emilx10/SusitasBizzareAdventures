using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolerPickup : Pickup
{
    public override void Collect()
    {
        GameManager.Instance.GetPlayerHealth().Chill();
        Destroy(gameObject); 
    }
}
