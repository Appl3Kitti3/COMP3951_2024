using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player
{
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int Damage { get; set; }

    public Animator Animator {  get; set; }

    private static Player _instance;
    


    private Player(int HP, int DMG, Animator animator) {
        Health = MaxHealth = HP;
        Damage = DMG;
        Animator = animator;
        Animator.SetInteger("HP", Health);
    }

    public static Player GetInstance(int maxhp = 3, int damage = 25, Animator animator = null)
    {
        if (_instance == null)
        {
            _instance = new Player(maxhp, damage, animator);
            Debug.Log("was null now not null");
        }
            
        return _instance;
    }

    public void InflictDamage(int damage, Animator animation)
    {
        animation.SetTrigger("Hit");
        if (Animator.IsDestroyed())
            return;
        Animator.SetTrigger("Hit");
        Health -= damage;
        
        Animator.SetInteger("HP", Health);
        
    }

    

}
