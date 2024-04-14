using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Boss is a derived class from Creature. It is used to operate the logic of the two bosses (Though they have their own classes, for
/// specific attack operations)
/// The bosses and players are the only ones with two abilities.
/// 
/// Author: Tedrik "Teddy" Dumam-Ag 
/// Date: April 12 2024
/// Sources: Applied C# and OOP skills.
/// </summary>
public class Boss : Creature
{
    // The status when the boss charge up or their ultimate ability is active.
    private bool _isCharging;
    
    // Turns the string name of ultimate into an integer id.
    private static readonly int Ultimate = Animator.StringToHash("Ultimate");
    
    /// <summary>
    /// It stops movement when the boss is charging up.
    /// </summary>
    /// <returns>Based on the status of _isCharging.</returns>
    protected override bool ShouldStop()
    {
        return _isCharging;
    }
    
    /// <summary>
    /// Starts the random number generator and have a chance of a boss using ultimate or its primary. (Necro's primary projectile ability)
    /// </summary>
    protected override void RenderAbilities()
    {
        StartCoroutine(RunAttacks());
    }
    
 
    /// <summary>
    /// This is the RNG and has an operation if the boss should do its ultimate or primary.
    /// </summary>
    /// <returns>A delay in seconds. (Not an assignable value)</returns>
    protected override IEnumerator RunAttacks()
    {
        while (true)
        {
            // Do a random range from 0 to 9.
            var picker = SelectAbility();
            switch (picker)
            {
                // 90% is primary.
                case < 9:
                {
                    NecroProjectileHandle();
                    break;
                }
                // 10% is the ultimate.
                case 9:
                    Animator.SetBool(Ultimate, true);
                    _isCharging = true;
                    Stationary.SetActive(true);
                    break;
            }
            // perform before wait operations.
            yield return TimeDuringAttacks;
            ResetAbility();
        }
    }

    /// <summary>
    /// Resets the ultimate ability.
    /// </summary>
    private void ResetAbility()
    {
        Stationary.SetActive(false);
        _isCharging = false;
        Animator.SetBool(Ultimate, false);
    }
    
    /// <summary>
    /// Performs the primary attack ability. Usually only for Necro.
    /// </summary>
    protected virtual void PerformPrimary()
    {
        // Assume that one is given for any class except Necro
        CreateProjectile();
        
    }
    
    /// <summary>
    /// Randomizes a number between 0 to 9..
    /// </summary>
    /// <returns>A integer value from 0 to 9.</returns>
    private static int SelectAbility()
    {
        return Random.Range(0, 10);
    }

    /// <summary>
    /// If the original projectile is active. Skip the code, otherwise perform Necro attacks.
    /// </summary>
    protected virtual void NecroProjectileHandle() {}
}
