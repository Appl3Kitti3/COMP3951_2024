using UnityEngine;

/// <summary>
///     This projectile explodes or disappears after impact or in a certain point in time.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 13 2024
/// Source: Applied C# and Unity Skills
/// </summary>
public class UnstableProjectile : Projectile
{
    // Status that the projectile has started impact a wall or sprite.
    private bool _isImpact;
    
    // Turns the named Landed parameter into an integer id.
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

    protected override void CheckColliderBounds(Collider2D structure)
    {
        if (structure.CompareTag("Structure"))
            Animator.SetTrigger(Landed);
    }
}
