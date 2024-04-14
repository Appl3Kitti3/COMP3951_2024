using System.Collections;
using UnityEngine;

/// <summary>
///     Used mainly for bosses. They are the charging up abilities.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 13 2024
/// Source: Applied C# and Unity Skills
/// </summary>
public abstract class PassiveAbility : Ability
{
    // Float in seconds, time between each charge up.
    [SerializeField] protected float timeBetweenUses;

    // Create a WaitForSeconds based on timeBetweenUses.
    private WaitForSeconds _useDelay;

    // Checks if the ultimate has begun elapsing.
    private bool _hasCoroutineStarted;

    // Ability's user.
    protected Creature BossCreature;
    
    protected override void Init()
    {
        base.Init();
        BossCreature = transform.parent.gameObject.GetComponent<Creature>();
        _useDelay = new WaitForSeconds(timeBetweenUses);
        StartCoroutine(StartChargeUp());
    }
    
    // Disable handle collision for passive ability.
    protected override void HandleCollision(Collider2D other) {}
    
    // Continue coroutine.
    private void OnEnable()
    {
        _hasCoroutineStarted = true;
    }

    // Stop coroutine.
    private void OnDisable()
    {
        _hasCoroutineStarted = false;
    }

    // Start the boss' charging up ability.
    private IEnumerator StartChargeUp()
    {
        while (true)
        {
            if (!_hasCoroutineStarted) continue;
            PerformAbility();
            yield return _useDelay;
        }
    }

    /// <summary>
    /// Perform the logic of the two bosses. Regeneration ability and speed boost.
    /// </summary>
    protected abstract void PerformAbility();
    
}
