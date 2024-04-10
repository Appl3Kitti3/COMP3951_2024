using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Debug = UnityEngine.Debug;

// TODO make this into controller


/// <summary>
/// Description:
///     Player class is a singleton that represents the current stats of the player. (Damage, Health.)
///         It will have a weapon class to represent its weapon and its playable class to dynamically link
///         and switch to what class should be playing.
/// Author: Teddy Dumam-Ag A01329707
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
partial class PlayerController : MonoBehaviour
{
    private GameObject projectile;

    private Vector2 originalProjectileScale;

    [SerializeField] private string projectile_type;

    [SerializeField] private float timeBetweenProjectiles;

    private WaitForSeconds _creationDelay;

    private bool _isElapsed;
    void CreateProjectile()
    {
        var currTransform = transform;
        var clone = Instantiate(projectile, currTransform.position, Quaternion.identity);

        clone.GetComponent<Projectile>().ParentObject = gameObject;
        clone.GetComponent<SpriteRenderer>().enabled = true;
        clone.SetActive(true);
    }
    void EnablePrimary()
    {
        Player.GetInstance().Animator.SetBool("Primary", true);
        if (projectile_type.Equals("Primary"))
        {
            _isElapsed = true;
            StartCoroutine(CreateMultipleProjectiles());
        }
        else
            stationaryHitBox.SetActive(true);
        
    }

    void DisablePrimary()
    {
        Player.GetInstance().Animator.SetBool("Primary", false);
        if (projectile_type.Equals("Primary"))
            _isElapsed = false;
        else
            stationaryHitBox.SetActive(false);
    }
    
    void EnableUltimate()
    {
        Player.GetInstance().Animator.SetBool("Ultimate", true);
        /*Debug.Log("Ultimate is at Go!");*/
        if (projectile_type.Equals("Ultimate"))
        {
            _isElapsed = true;
            StartCoroutine(CreateMultipleProjectiles());
        }
        else
        {
            stationaryHitBox.SetActive(true);
        }
        StartCoroutine(StartUltimateElapsed());

    }

    void DisableUltimate()
    {
        Player.GetInstance().Animator.SetBool("Ultimate", false);
        Debug.Log("Ultimate has stopped");
        if (projectile_type.Equals("Ultimate"))
            _isElapsed = false;
        else
        {
            stationaryHitBox.SetActive(false);
        }
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
            CreateProjectile();
            yield return _creationDelay;    
        }
    }
    
    void ModifyProjectile()
    {
        float tmpScale = Player.GetInstance().GetProjectileScale;
        projectile.transform.localScale = originalProjectileScale * tmpScale;
    }
}
