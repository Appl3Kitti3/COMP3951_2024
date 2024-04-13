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
