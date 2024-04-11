using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponOrb : MonoBehaviour
{
    private Animator _animator;

    private int _counter;
    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player.GetInstance().ChosenClass.Weapon.GainBoost();
            int rngFreeHealth = Random.Range(0, 2);
            if (rngFreeHealth == 1)
                Player.GetInstance().IncrementHealth();
            _animator.SetTrigger("Collected");
        }
        
    }
}
