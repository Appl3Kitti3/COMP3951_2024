using UnityEngine;


// disappears after hit
// used for projectiles
public class UnstableProjectile : Projectile
{
    private bool _isImpact;
    private static readonly int Landed = Animator.StringToHash("Landed");

    protected override void CheckBounds()
    {
        Animator.SetTrigger(Landed);
        _isImpact = true;
    }

    protected override void BeforeDestroy()
    {
        if (!_isImpact)
            Animator.SetTrigger(Landed);
    }

    protected override void SetRotation()
    {
        transform.rotation = GetRotation(Target);
    }
    
    private static Quaternion GetRotation(Vector2 target)
    {
        var angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, angle);
    }
}
