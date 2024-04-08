
using System;
using UnityEngine;
using Random = UnityEngine.Random;

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
    
    public int BaseDamage { get; set; }

    // Critical chance of the weapon.

    public int CritHit { get; set; } = Constants.BaseCriticalHit;

    // Critical damage (finalized) of the weapon.

    public int CritDamage { get; set; } = Constants.BaseCriticalDmg;

    public int PerformAttackType(string s = null)
    {
        int indicator;
        if (s != null)
            indicator = ChooseAttackType(s);   
        else
            indicator = ChooseAttackType();
        switch (indicator)
        {
            case 0: return CalculateDamage();
            case 1: return UltimateAbility();
            default:
                throw new Exception();
        }
    }
    
    
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

    protected bool IsLandedOnCriticalHit()
    {
        return Random.Range(0f, 1.0f) < (1 / (double)CritHit);
    }

    public int ChooseAttackType()
    {
        if (Player.GetInstance().Animator.GetBool("Primary"))
            return 0;
        else if (Player.GetInstance().Animator.GetBool("Ultimate"))
            return 1;
        return -1;
    }
    
    public int ChooseAttackType(string s)
    {
        switch (s)
        {
            case "primary-projectile":
                return 0;
            case "ultimate-projectile":
                return 1;
            default:
                return -1;
        }
    }

    public void GainBoost()
    {
        BaseDamage += (int)(BaseDamage * (3d / 10));
        CritDamage += (int)(CritDamage * (2d / 10));
        CritHit += 2;
        Debug.Log($"Player has {BaseDamage} Damage, {CritDamage} Critical Damage and {CritHit}% Chance of Critical Hits");
    }
}
