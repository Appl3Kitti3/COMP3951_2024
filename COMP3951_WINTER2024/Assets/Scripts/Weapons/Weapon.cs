
using System;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Description:
///     A weapon consists of a damage, critical hit chance and its final critical hit damage.
/// Each derived weapon has its own calculation of damage and ultimate ability.
/// 
/// Author: 
/// Date: March 19 2024
/// Source: Applied C# Skills
/// </summary>
public abstract class Weapon
{
    private static readonly int Primary = Animator.StringToHash("Primary");

    private static readonly int Ultimate = Animator.StringToHash("Ultimate");
    // Damage of the weapon.

    protected int BaseDamage { get; set; }

    // Critical chance of the weapon.

    private int CriticalHit { get; set; } = Constants.BaseCriticalHit;

    // Critical damage (finalized) of the weapon.

    protected int CriticalDamage { get; private set; } = Constants.BaseCriticalDmg;

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

    protected bool IsLandedOnCriticalHit()
    {
        return Random.Range(0f, 1.0f) < 1 / (double)CriticalHit;
    }

    private static int ChooseAttackType()
    {
        if (Player.Animator.GetBool(Primary))
            return 0;
        if (Player.Animator.GetBool(Ultimate))
            return 1;
        return -1;
    }

    private static int ChooseAttackType(string s)
    {
        return s switch
        {
            "primary-projectile" => 0,
            "ultimate-projectile" => 1,
            _ => -1
        };
    }

    public void GainBoost()
    {
        BaseDamage += (int)(BaseDamage * (3d / 10));
        CriticalDamage += (int)(CriticalDamage * (2d / 10));
        CriticalHit += 2;
    }
}
