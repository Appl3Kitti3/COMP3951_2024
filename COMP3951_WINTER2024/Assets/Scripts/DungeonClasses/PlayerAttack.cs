using System.Collections;
using UnityEngine;

/// <summary>
/// Description:
///     Player class is a singleton that represents the current stats of the player. (Damage, Health.)
///         It will have a weapon class to represent its weapon and its playable class to dynamically link
///         and switch to what class should be playing.
/// Author: 
/// Date: March 6 2024 (Created around February)
/// Sources: Applied C# and OOP skills.
///
///     Tutorial on how to make player shoot or attack.
///     https://www.youtube.com/watch?v=mgjWA2mxLfI
///
///     State process of the animation tree in Unity.
///     https://www.youtube.com/watch?v=77dWGDFqcps
///
///     Detecting enemy or other game sprite collisions.
///     https://www.youtube.com/watch?v=ND1orPLw5EQ
///
///     Cancelling a Task using a Cancel Token Source
///     https://www.c-sharpcorner.com/UploadFile/80ae1e/canceling-a-running-task/
///
/// </summary>
partial class PlayerController
{
    // Player's projectile
    private GameObject _projectile;

    // Used for operating with Big Projectile Ability.
    private Vector2 _originalProjectileScale;

    // Player's Projectile type.
    [SerializeField] private string projectileType;

    // Float value of time delay between creation of projectiles
    [SerializeField] private float timeBetweenProjectiles;

    [Header("Time Based Attacks")] 
    
    // Float value of elapsed time ultimate duration.
    [SerializeField] private float timeDuringUltimateSeconds;

    // Float value of ultimate cooldown.
    [SerializeField] private float ultimateCooldown;

    // Used for Melee class usually.
    private bool _isFirst = true;
    
    // Create a WaitForSeconds class for time between projectiles.
    private WaitForSeconds _creationDelay;

    // Status if the player's ultimate is active.
    private bool _isElapsed;
    
    // Status whenever the player's ultimate cooldown is finished.
    private bool _isCooldownDone = true;

    // Used for avoiding Primary attacks when Ultimate is active.
    private bool _hasUltimateActive;
    
    // Used for Archer and Mage projectile delay status.
    private bool _shouldDelay;

    // Used for Melee, delaying the first animation.
    private bool _shouldDelayPrimary;
    
    // Integer ID for Primary
    private static readonly int Primary = Animator.StringToHash("Primary");
    
    // Integer ID for Ultimate.
    private static readonly int Ultimate = Animator.StringToHash("Ultimate");

    // Create a WaitForSeconds on Melee delay
    private WaitForSeconds _meleeWaitForSeconds;
    
    // Creates a projectile.
    private void CreateProjectile()
    {
        var currTransform = transform;
        var clone = Instantiate(_projectile, currTransform.position, Quaternion.identity);
        clone.GetComponent<Projectile>().ParentObject = gameObject;
        clone.SetActive(true);
    }

    // Enables Primary.
    private void EnablePrimary()
    {
        if (projectileType.Equals("Primary"))
        {
            if (_shouldDelay) return;
            Player.Animator.SetBool(Primary, true);
            _isElapsed = true;
            StartCoroutine(CreateMultipleProjectiles());
        }
        else
        {
            // this is always Melee
            if (_shouldDelayPrimary) return;
            Player.Animator.SetBool(Primary, true);
            StartCoroutine(DelayPrimaryBetweenClicks());
            _nonProjectileHitBox.SetActive(true);
        }
    }

    // Disable Primary.
    private void DisablePrimary()
    {
        Player.Animator.SetBool(Primary, false);
        if (projectileType.Equals("Primary"))
            _isElapsed = false;
        else
            _nonProjectileHitBox.SetActive(false);
    }

    // Enable Ultimate.
    private void EnableUltimate()
    {
        Player.Animator.SetBool(Ultimate, true);
        if (projectileType.Equals("Ultimate"))
        {
            _isElapsed = true;
            StartCoroutine(CreateMultipleProjectiles());
        }
        else
            _nonProjectileHitBox.SetActive(true);
        // Start Ultimate Duration.
        StartCoroutine(StartUltimateElapsed());

    }

    // Disable Ultimate.
    private void DisableUltimate()
    {
        Player.Animator.SetBool(Ultimate, false);
        if (projectileType.Equals("Ultimate"))
            _isElapsed = false;
        else
            _nonProjectileHitBox.SetActive(false);
        _isFirst = true;
        _hasUltimateActive = false;

        // Start Ultimate Cooldown.
        StartCoroutine(StartCooldown());
    }

    // During Elapsed, Wait until Duration is done and Disable the ultimate.
    private IEnumerator StartUltimateElapsed()
    {
        _isCooldownDone = false;
        _hasUltimateActive = true;
        
        yield return new WaitForSeconds(timeDuringUltimateSeconds);
        DisableUltimate();
        _sounds[3].Play();

    }

    // Wait until cooldown is done and set the status.
    private IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(ultimateCooldown);
        _isCooldownDone = true;
        _sounds[2].Play();
    }

    // Disables Auto-Hold for Melee class.
    private IEnumerator DelayPrimaryBetweenClicks()
    {
        _shouldDelayPrimary = true;
        yield return _meleeWaitForSeconds;
        DisablePrimary();
        _shouldDelayPrimary = false;
    }

    // Create Amount of Projectiles
    private IEnumerator CreateMultipleProjectiles()
    {
        // before launching 
        ModifyProjectile();
        while (_isElapsed)
        {
            _shouldDelay = true;
            CreateProjectile();
            yield return GetDelay();
            _shouldDelay = false;
        }
    }
    
    // Get the delay. If its first projectile and player is Melee, double the seconds
    // else return the normal delay.
    private WaitForSeconds GetDelay()
    {
        if (!_isFirst || !Player.ChosenClass.Name.Equals("Melee")) return _creationDelay;
        _isFirst = false;
        return new WaitForSeconds(timeBetweenProjectiles * 2);

    }

    // Modify the projectile's scale.
    // logic for Big Projectile Ability.
    private void ModifyProjectile()
    {
        var tmpScale = Player.GetProjectileScale;
        _projectile.transform.localScale = _originalProjectileScale * tmpScale;
    }
}
