using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenerationAbility : PassiveAbility
{
    protected override void PerformAbility()
    {
        Creature c = transform.parent.gameObject.GetComponent<Creature>();
        if (c.Health == c.MaxHealth)
            return;
        c.Health += (int) (c.MaxHealth * (3d / 20));
        Debug.Log($"Necro is at {c.Health} / {c.MaxHealth}");
    }
}
