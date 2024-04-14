using UnityEngine;

/// <summary>
///     Used for Mage's ultimate. Disable knock back force so the enemies can stay in the region constantly.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 13 2024
/// Source: Applied C# and Unity Skills
/// </summary>
public class ScreenWideAttack : Ability
{
    protected override void SetUpForce(Collider2D other) {}
}
