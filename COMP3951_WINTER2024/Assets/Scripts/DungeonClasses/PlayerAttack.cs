using System.Collections;
using UnityEngine;
// TODO make this into controller


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
    private GameObject _projectile;

    private Vector2 _originalProjectileScale;

    [SerializeField] private string projectileType;

    [SerializeField] private float timeBetweenProjectiles;

    [Header("Time Based Attacks")] 
    
    [SerializeField] private float timeDuringUltimateSeconds;

    [SerializeField] private float ultimateCooldown;

    private bool _isFirst = true;
    private WaitForSeconds _creationDelay;

    private bool _isElapsed;
    
    private bool _isCooldownDone = true;

    private bool _hasUltimateActive;
    private static readonly int Primary = Animator.StringToHash("Primary");
    private static readonly int Ultimate = Animator.StringToHash("Ultimate");

    void CreateProjectile()
    {
        var currTransform = transform;
        var clone = Instantiate(_projectile, currTransform.position, Quaternion.identity);
        clone.GetComponent<Projectile>().ParentObject = gameObject;
        clone.GetComponent<SpriteRenderer>().enabled = true;
        clone.SetActive(true);
    }
    void EnablePrimary()
    {
        Player.Animator.SetBool(Primary, true);
        if (projectileType.Equals("Primary"))
        {
            _isElapsed = true;
            StartCoroutine(CreateMultipleProjectiles());
        }
        else
            _nonProjectileHitBox.SetActive(true);
        
    }

    void DisablePrimary()
    {
        Player.Animator.SetBool(Primary, false);
        if (projectileType.Equals("Primary"))
            _isElapsed = false;
        else
            _nonProjectileHitBox.SetActive(false);
    }
    
    void EnableUltimate()
    {
        
            
        Player.Animator.SetBool(Ultimate, true);
        if (projectileType.Equals("Ultimate"))
        {

            _isElapsed = true;
            StartCoroutine(CreateMultipleProjectiles());
        }
        else
        {
            _nonProjectileHitBox.SetActive(true);
        }
        StartCoroutine(StartUltimateElapsed());

    }

    void DisableUltimate()
    {
        
        Player.Animator.SetBool(Ultimate, false);
        if (projectileType.Equals("Ultimate"))
            _isElapsed = false;
        else
        {
            _nonProjectileHitBox.SetActive(false);
        }
        _isFirst = true;
        _hasUltimateActive = false;

        StartCoroutine(StartCooldown());
        
        
    }

    IEnumerator StartUltimateElapsed()
    {
        _isCooldownDone = false;
        _hasUltimateActive = true;
        yield return new WaitForSeconds(timeDuringUltimateSeconds);
        DisableUltimate();

    }

    IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(ultimateCooldown);
        _isCooldownDone = true; // playSound in here
    }

    IEnumerator CreateMultipleProjectiles()
    {
        // before launching 
        ModifyProjectile();
        while (_isElapsed)
        {
            yield return GetDelay();
            CreateProjectile();
                
        }
    }

    private WaitForSeconds GetDelay()
    {
        if (!_isFirst || !Player.ChosenClass.Name.Equals("Melee")) return _creationDelay;
        _isFirst = false;
        return new WaitForSeconds(timeBetweenProjectiles * 2);

    }
    void ModifyProjectile()
    {
        float tmpScale = Player.GetProjectileScale;
        _projectile.transform.localScale = _originalProjectileScale * tmpScale;
    }
}
