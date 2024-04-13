using System.Collections;
using UnityEngine;

public class Boss : Creature
{
    private bool _isCharging;
    protected override bool ShouldStop()
    {
        return _isCharging;
    }

    protected override void Init()
    {
        Stationary.SetActive(false);
        
    }

    protected override void RenderAbilities()
    {
        StartCoroutine(RunAttacks());
    }

    protected override IEnumerator RunAttacks()
    {
        while (true)
        {
            
            var picker = SelectAbility();
            switch (picker)
            {
                case < 9:
                {
                    if (!Projectile.activeInHierarchy)
                    {
                        Animator.SetTrigger("Primary");
                        PerformPrimary();                    
                    }

                    break;
                }
                case 9:
                    Animator.SetBool("Ultimate", true);
                    _isCharging = true;
                    Stationary.SetActive(true);
                    break;
            }
            BeforeWait();
            yield return TimeDuringAttacks;
            ResetAbility();
        }
    }

    protected virtual void BeforeWait()
    {
        
    }
    private void ResetAbility()
    {
        Stationary.SetActive(false);
        _isCharging = false;
        Animator.SetBool("Ultimate", false);
    }
    protected virtual void PerformPrimary()
    {
        // Assume that one is given for any class except Necro
        CreateProjectile();
        
    }
    private int SelectAbility()
    {
        return Random.Range(0, 10);
    }
}
