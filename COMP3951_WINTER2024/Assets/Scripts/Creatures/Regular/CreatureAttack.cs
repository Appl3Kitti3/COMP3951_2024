using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Description:
///     The attack functionality for the Creature.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 12 2024
/// Source: Applied C# and Unity Skills
/// </summary>
partial class Creature
{
    // Used for Projectile Based Attacks.
    
    // The invisible attack box that is in front of the creature.
    protected GameObject Stationary;

    // Usually is meant for bosses.
    protected GameObject Projectile;
    
    // The delay in seconds between projectile creation attacks.
    [SerializeField] 
    private float attackDelay;

    // Then create a WaitForSeconds with that delay.
    protected WaitForSeconds TimeDuringAttacks;

    // Performs a creation of Projectile when the loop is active.
    private bool _isProjectileLoopActive;
    
    // Projectile type that is used to identify the boss' Projectile.
    private string _projectileType;
    
    /// <summary>
    /// Sets animator to trigger the animation based on the ability type and set speed to 0.
    /// </summary>
    private void AttackProjectile()
    {
        Animator.SetTrigger(_projectileType);
        Animator.SetFloat(Speed, 0f);
        CreateProjectile();
    }

    /// <summary>
    /// Creates / Clones a Projectile in a specific position.
    /// </summary>
    /// <param name="x">A float, x init position.</param>
    /// <param name="y">A float, y init position.</param>
    protected void CreateProjectile(float x = 0, float y = 0)
    {
        var tmp = transform;
        tmp.Translate(x, y, 0);
        var clone = Instantiate(Projectile, tmp.position, Quaternion.identity);
        clone.GetComponent<Projectile>().ParentObject = gameObject;
        clone.SetActive(true);
        
    }
        
    /// <summary>
    /// Runs an elapsed coroutine and generates a projectile then gets delayed.
    /// </summary>
    /// <returns>Returns a delay in a true elapsed.</returns>
    protected virtual IEnumerator RunAttacks()
    {
        while (true)
        {
            if (!_isProjectileLoopActive) continue;
            AttackProjectile();
            yield return TimeDuringAttacks;
        }
    }

}