using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
///     Projectile is a child class from attack and it is to represent the moving sprite attacks.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 13 2024
/// Source: Applied C# and Unity Skills
/// </summary>
public abstract class Projectile : Attack
{

    // Parent's position.
    private Vector2 _parent;
    
    // Target's position.
    protected Vector2 Target;

    // Establish a wait before destroying.
    private readonly WaitForSeconds _destroyDelay = new(1);
    
    // Projectile's animator.
    protected Animator Animator;

    // Projectile type as a string.
    [SerializeField] private string projectileType;

    // Get the projectile type.
    public string ProjectileType => projectileType;
    
    // How far should the projectile go.
    [FormerlySerializedAs("_rangeFloat")] [SerializeField] private float rangeFloat;
    
    // Sets all local values and adds a force in order to move the projectile from the player
    protected override void Init()
    {
        var currTransform = transform;
        _parent = currTransform.position;

        Animator = GetComponent<Animator>();
        Target = GetTargetPosition();
        SetRotation();
        GetComponent<Rigidbody2D>().AddForce(
            Target.normalized * rangeFloat, ForceMode2D.Impulse
        );
        // Start destroying self because they are very fragile when they spawn.
        StartCoroutine(DestroySelf());
    }
    
    /// <summary>
    /// Set the rotation of the projectile.
    /// </summary>
    protected abstract void SetRotation();
    
    protected override void BeforeAddingImplodeForce()
    {
        CheckBounds();
    }

    protected override Vector2 GetStartingPosition()
    {
        return transform.position;
    }
    
    /// <summary>
    /// Enables impact status and destroy itself.
    /// </summary>
    protected virtual void CheckBounds() {}
    
    /// <summary>
    /// Get the target's position.
    /// </summary>
    /// <returns>Vector2, the distance of target minus parent, otherwise parent object.</returns>
    private Vector2 GetTargetPosition()
    {
        Vector2 to;
        if (ParentObject.CompareTag("Player") ) {
            if (Player.ChosenClass.Name == "Melee") {
                // Melee
                to.x = ParentObject.transform.localScale.x;
                to.y = 0;
                return to;
            }
            else {
                // Archer or Mage
                Vector3 temp = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
                to.x = temp.x;
                to.y = temp.y;
                Vector2 diff = to - _parent;
                if (diff.x < 0) ParentObject.transform.localScale = new Vector3(-1, ParentObject.transform.localScale.y, 1);
                else ParentObject.transform.localScale = new Vector3(1, ParentObject.transform.localScale.y, 1);
                return diff;
            }

        } else {
            // Creature
            Vector3 temp = GameObject.FindWithTag("Player").transform.position;
            to.x = temp.x;
            to.y = temp.y;
            return to - _parent;
        }
        // try // test after testing the above
        // {
        //     // TODO: NOAH'S MELEE IDEA IS PLACED HERE (REMOVE #TODO ONCE WRITTEN HERE)
        //     Vector2 to = ParentObject.CompareTag("Player") ? 
        //         GameObject.FindWithTag("MainCamera").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition) 
        //         : GameObject.FindWithTag("Player").transform.position;
        //     return to - _parent;
        // }
        // catch (NullReferenceException)
        // {
        //     BeforeDestroy();
        // }
        // return _parent;
    }

    /// <summary>
    /// Perform a task before being destroyed.
    /// </summary>
    protected virtual void BeforeDestroy() {}

    /// <summary>
    /// Wait for one second and do task before destroying.
    /// </summary>
    /// <returns></returns>
    private IEnumerator DestroySelf()
    {
        yield return _destroyDelay;
        BeforeDestroy();

    }

    protected override int GetAttackTypeDamage()
    {
        return Player.ChosenClass.Weapon.PerformAttackType(projectileType);
    }
}
