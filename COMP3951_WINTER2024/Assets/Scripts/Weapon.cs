/// <summary>
/// Description:
///     A weapon consists of a damage, critical hit chance and its final critical hit damage.
/// Each derived weapon has its own calculation of damage and ultimate ability.
/// 
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: March 19 2024
/// Source: Applied C# Skills
/// </summary>
public abstract class Weapon
{
    // Damage of the weapon.
    private int _dmg;
    
    // Critical chance of the weapon.
    private double _critHit;
    
    // Critical damage (finalized) of the weapon.
    private int _critDmg;

    /// <summary>
    /// Calculates its own damage and contains the random generator for obtaining a critical hit.
    /// </summary>
    /// <returns>Either the normal damage or the damage plus the critical damage once the hit is landed.</returns>
    public abstract int CalculateDamage();

    /// <summary>
    /// Calculates its own damage and contains the random generator for obtaining a critical hit.
    ///     However, there will be a bonus attack buff because it is an ultimate ability.
    /// </summary>
    /// <returns>Normal damage or normal + critical with buff.</returns>
    public abstract int UltimateAbility();
}
