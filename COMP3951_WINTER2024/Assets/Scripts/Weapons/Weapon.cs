
using System;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Description:
///     A weapon consists of a damage, critical hit chance and its final critical hit damage.
/// Each derived weapon has its own calculation of damage and ultimate ability.
/// 
/// Author: Tedrik "Teddy" Dumam-Ag 
/// Date: March 19 2024 (Revision April 13 2024)
/// Source: Applied C# Skills
/// </summary>
public abstract class Weapon
{
    // Set the Primary as an integer id.
    private static readonly int Primary = Animator.StringToHash("Primary");

    // Set the Ultimate as an integer id.
    private static readonly int Ultimate = Animator.StringToHash("Ultimate");
    
    // Damage of the weapon.
    protected int BaseDamage { get; set; }

    // Critical chance of the weapon.
    private int CriticalHit { get; set; } = Constants.BaseCriticalHit;

    // Critical damage (finalized) of the weapon.
    protected int CriticalDamage { get; private set; } = Constants.BaseCriticalDmg;

    /// <summary>
    /// Get the number based on the animator's boolean status.
    /// </summary>
    /// <param name="s">Attack type if animation is being held.</param>
    /// <returns>0 for Primary, 1 for Ultimate, throw exception anything else.</returns>
    /// <exception cref="Exception">Try to break the program.</exception>
    public int PerformAttackType(string s = null)
    {
        var indicator = s != null ? ChooseAttackType(s) : ChooseAttackType();
        return indicator switch
        {
            0 => CalculateDamage(),
            1 => UltimateAbility(),
            _ => throw new Exception()
        };
    }
    
    
    /// <summary>
    /// Calculates its own damage and contains the random generator for obtaining a critical hit.
    /// </summary>
    /// <returns>Either the normal damage or the damage plus the critical damage once the hit is landed.</returns>
    protected abstract int CalculateDamage();

    /// <summary>
    /// Calculates its own damage and contains the random generator for obtaining a critical hit.
    ///     However, there will be a bonus attack buff because it is an ultimate ability.
    /// </summary>
    /// <returns>Normal damage or normal + critical with buff.</returns>
    protected abstract int UltimateAbility();

    // Calculated logic of Critical Hits chances.
    protected bool IsLandedOnCriticalHit()
    {
        return Random.Range(0f, 1.0f) < 1 / (double)CriticalHit;
    }

    /// <summary>
    /// Get an integer based on the attack type.
    /// </summary>
    /// <returns>0 for Primary, 1 for Ultimate, -1 otherwise.</returns>
    private static int ChooseAttackType()
    {
        if (Player.Animator.GetBool(Primary))
            return 0;
        if (Player.Animator.GetBool(Ultimate))
            return 1;
        return -1;
    }

    /// <summary>
    /// Returns an integer based on the string projectile type.
    /// </summary>
    /// <param name="s">A string that manually identifies an attack.</param>
    /// <returns>0 for primary, 1 for ultimate, -1 otherwise.</returns>
    private static int ChooseAttackType(string s)
    {
        return s switch
        {
            "primary-projectile" => 0,
            "ultimate-projectile" => 1,
            _ => -1
        };
    }

    // Upgrade the stats (called on Weapon Buff)
    public void GainBoost()
    {
        BaseDamage += (int)(BaseDamage * (1d / 10));
        CriticalDamage += (int)(CriticalDamage * (1d / 10));
        CriticalHit += 2;
    }
}
