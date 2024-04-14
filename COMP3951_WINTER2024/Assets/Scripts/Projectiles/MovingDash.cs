using UnityEngine;

/// <summary>
///     Meant for Melee's flaming dash. Flips the direction and rotation based on the target - player position direction.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 13 2024
/// Source: Applied C# and Unity Skills
/// </summary>
public class MovingDash : Projectile
{
    protected override void SetRotation()
    {
        Target.y = 0f;
        GetComponent<SpriteRenderer>().flipX = !(Target.x < 0);
    }
}
