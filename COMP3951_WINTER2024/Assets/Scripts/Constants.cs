using UnityEngine;

/// <summary>
///     Constant values used for Endless Catacombs.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 13 2024
/// Source: Applied C# and Unity Skills
/// </summary>
public static class Constants
{
    // Mace's passive bonus.
    public const int MaceBonus = 15;

    // Berserker's critical passive bonus.
    public const int BerserkerCritical = 5;
    
    // Bow's multiplier on ultimate.
    public const int BowMultiplier = 150;

    // Archer's critical passive bonus.
    public const int ArcherCritical = 7;
    
    // Staff's multiplier on ultimate.
    public const int StaffMultiplier = 60;

    // Mage's passive bonus on Critical.
    public const int MageCritical = 3;
    
    // Weapons Attributes
    // Base damage of Mace.
    public const int BaseMace = 25;
    
    // Base damage of Bow.
    public const int BaseBow = 34;
    
    // Base damage of Staff.
    public const int BaseStaff = 50;

    // Base Critical Damage.
    public const int BaseCriticalDmg = 50;

    // Base Critical Hit.
    public const int BaseCriticalHit = 12;

    // Melee's Regular Speed.
    public const int MeleeSpeed = 6;

    // Melee's Reduced Ultimate Speed.
    public const int MeleeUltSpeed = 4;
    
    // Archer's Regular Speed.
    public const int ArcherSpeed = 6;

    // Archer's Reduced Ultimate Speed.
    public const int ArcherUltSpeed = 3;
    
    // Mage's Regular Speed.
    public const int MageSpeed = 10;

    // Mage's Reduced Ultimate Speed.
    public const int MageUltSpeed = 2;
    
    // Health for Mage and Melee.
    public const int MaximumHealth = 5;

    // Health for Archer.
    public const int MinimumHealth = 3;
    
    // Path used to create for saving player's data.
    public const string Path = @".\Data\data.json";
}

/// <summary>
///     Functions that are globally available on calculating a similar procedure.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 13 2024
/// Source: Applied C# and Unity Skills
/// </summary>
internal static class Utility
{
    /// <summary>
    /// Get the inverse scale of the game Object's vector.
    /// </summary>
    /// <param name="localScale">Game Object's scale..</param>
    /// <param name="one">Represent as a positive or negative one.</param>
    /// <returns>Vector2 of changed sign of Local Scale x.</returns>
    public static Vector2 GetInverseScale(Vector2 localScale, int one)
    {
        return new Vector2(Mathf.Abs(localScale.x)*one, localScale.y);
    }
}