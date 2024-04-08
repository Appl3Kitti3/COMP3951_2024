using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

// use AddForce
public abstract class Projectile : Attack
{

    private Vector2 _parent;
    
    protected Vector2 Target;

    private readonly WaitForSeconds _wait = new WaitForSeconds(1);

    protected Animator Animator;

    [FormerlySerializedAs("projectile_type")] public string projectileType;

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
    private void Awake()
    {
        

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
        Vector2 to;
        to = ParentObject.CompareTag("Player") ? 
            GameObject.FindWithTag("MainCamera").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition) 
            : GameObject.FindWithTag("Player").transform.position;
            
        return to - _parent;
    }

    protected virtual void BeforeDestroy() {}
    IEnumerator DestroySelf()
    {
        yield return _wait;
        BeforeDestroy();

    }

    protected override int GetAttackTypeDamage()
    {
        return Player.GetInstance().ChosenClass.Weapon.PerformAttackType(projectileType);
    }
}
