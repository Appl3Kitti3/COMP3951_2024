using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (RetainCall.AddAndCheckCounter(ref _counter))
            if (other.CompareTag("Player"))
            {
                Player.GetInstance().ChosenClass.Weapon.GainBoost();
                _animator.SetTrigger("Collected");
            }
            
    }
}
