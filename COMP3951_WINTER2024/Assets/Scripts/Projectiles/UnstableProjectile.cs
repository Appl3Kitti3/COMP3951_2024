using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// disappears after hit
// used for projectiles
public class UnstableProjectile : Projectile
{
    private bool _isImpact;

    protected override void CheckBounds()
    {
        Animator.SetTrigger("Landed");
        _isImpact = true;
    }

    protected override void BeforeDestroy()
    {
        if (!_isImpact)
            Animator.SetTrigger("Landed");
    }

    protected override void SetRotation()
    {
        transform.rotation = GetRotation(Target);
    }
    
    private Quaternion GetRotation(Vector2 target)
    {
        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, angle);
    }
}
