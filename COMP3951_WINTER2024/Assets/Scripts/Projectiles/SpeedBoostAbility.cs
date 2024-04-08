using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostAbility : PassiveAbility
{
    
    
    protected override void PerformAbility()
    {
        Creature c = transform.parent.gameObject.GetComponent<Creature>();
        c.MoveSpeed += (0.5f);
    }
}
