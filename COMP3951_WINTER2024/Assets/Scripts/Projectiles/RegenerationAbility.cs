using UnityEngine;

public class RegenerationAbility : PassiveAbility
{
    protected override void PerformAbility()
    {
        var c = transform.parent.gameObject.GetComponent<Creature>();
        if (c.Health == c.MaxHealth)
            return;
        c.Health += (int) (c.MaxHealth * (3d / 20));
    }
}
