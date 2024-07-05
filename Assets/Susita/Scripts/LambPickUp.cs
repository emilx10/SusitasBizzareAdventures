using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LambPickUp : Pickup
{
    private GuideQuest _guideQuest;
    public override void Collect()
    {
        _guideQuest.OnLambCollect?.Invoke();
        Destroy(gameObject);
    }

    public void SetGuideQuest(GuideQuest guideQuest)
    {
        _guideQuest = guideQuest;
    }
}
