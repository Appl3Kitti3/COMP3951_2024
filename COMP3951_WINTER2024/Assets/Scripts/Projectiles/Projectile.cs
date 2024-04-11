using System;
using System.Collections;
using UnityEngine;

public abstract class Projectile : Attack
{

    private Vector2 _parent;
    
    protected Vector2 Target;

    private readonly WaitForSeconds _wait = new(1);

    protected Animator Animator;

    public string projectileType;

    protected override void Init()
    {
        var currTransform = transform;
        _parent = currTransform.position;

        Animator = GetComponent<Animator>();
        Target = GetTargetPosition();
        SetRotation();
        GetComponent<Rigidbody2D>().AddForce(
            Target.normalized * 75f, ForceMode2D.Impulse
        );
        StartCoroutine(DestroySelf());
    }
    protected abstract void SetRotation();

    protected override void BeforeAddingImplodeForce()
    {
        CheckBounds();
    }

    protected override Vector2 GetStartingPosition()
    {
        return transform.position;
    }
    protected virtual void CheckBounds() {}
    
    private Vector2 GetTargetPosition()
    {
        try
        {
            Vector2 to = ParentObject.CompareTag("Player") ? 
                GameObject.FindWithTag("MainCamera").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition) 
                : GameObject.FindWithTag("Player").transform.position;
            return to - _parent;
        }
        catch (NullReferenceException)
        {
            BeforeDestroy();
        }

        return _parent;

    }

    protected virtual void BeforeDestroy() {}
    IEnumerator DestroySelf()
    {
        yield return _wait;
        BeforeDestroy();

    }

    protected override int GetAttackTypeDamage()
    {
        return Player.ChosenClass.Weapon.PerformAttackType(projectileType);
    }
}
