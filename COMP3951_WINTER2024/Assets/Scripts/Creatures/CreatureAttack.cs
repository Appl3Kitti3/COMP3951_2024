
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

partial class Creature
{
    
    // https://www.dotnetperls.com/lazy
    // The invisible attack box that is in front of the creature.
    protected GameObject Stationary;

    protected GameObject Projectile;
    
    [SerializeField] private float attackDelay;

    protected WaitForSeconds TimeDuringAttacks;

    private bool _isProjectileLoopActive;

    private Lazy<Projectile> _projectileClass;
    void AttackProjectile()
    {
        Animator.SetTrigger(_projectileClass.Value.projectileType);
        Animator.SetFloat("Speed", 0f);
        CreateProjectile();
    }

    protected void CreateProjectile(float x = 0, float y = 0)
    {
        Transform tmp = transform;
        tmp.Translate(x, y, 0);
        var clone = Instantiate(Projectile, tmp.position, Quaternion.identity);
        /*clone.GetComponent<Projectile>().ParentObject = gameObject;*/
        clone.SetActive(true);
    }
        
    protected virtual IEnumerator RunAttacks()
    {
        while (true)
        {
            if (_isProjectileLoopActive)
            {
                AttackProjectile();
                yield return TimeDuringAttacks;    
            }
                
        }
        
    }


}