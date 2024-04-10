using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{

    public GameObject ParentObject { get; set; }

    [SerializeField] protected float force;
    protected abstract void Init();
    
    // Start is called before the first frame update
    void Start()
    {
        Init();   
    }

    protected virtual int GetAttackTypeDamage()
    {
        return Player.GetInstance().ChosenClass.Weapon.PerformAttackType();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!ParentObject.IsDestroyed())
            HandleCollision(other);
    }

    protected virtual void HandleCollision(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && ParentObject.CompareTag("Player"))
        {
            SetUpForce(other);
            other.gameObject.GetComponent<Creature>().InflictDamage(GetAttackTypeDamage());
            
        }
            
        else if (other.gameObject.CompareTag("Player") && ParentObject.CompareTag("Enemy"))
        {
            SetUpForce(other);
            Creature c = ParentObject.GetComponent<Creature>();
            other.GetComponent<PlayerController>().InflictDamage(c.Damage, c.Animator);
            
        }
        
        /*if (!isWorldWide)
        {
            // https://www.youtube.com/watch?v=u4dG1AmXapI
            Vector2 direction = (other.transform.position - ParentObject.transform.position).normalized;
            Vector2 knockback = direction * 5f;
            other.GetComponent<Rigidbody2D>().AddForce(knockback, ForceMode2D.Impulse);
        }*/
    }
    
    protected virtual void SetUpForce(Collider2D other)
    {
        Vector2 to = other.transform.position;
        Vector2 from = GetStartingPosition();
        Vector2 direction = (to - from).normalized;
        
        BeforeAddingImplodeForce();   
        other.GetComponent<Rigidbody2D>().AddForce(direction * force, ForceMode2D.Impulse);
    }
    
    protected virtual void BeforeAddingImplodeForce() {}
    protected virtual Vector2 GetStartingPosition()
    {
        return ParentObject.transform.position;
    }
    
    
}
