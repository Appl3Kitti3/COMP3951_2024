using System.Collections;
using UnityEngine;

public abstract class PassiveAbility : Ability
{
    [SerializeField] protected float timeBetweenUses;

    private WaitForSeconds _useDelay;

    private bool _hasCoroutineStarted;

    protected override void Init()
    {
        base.Init();
        _useDelay = new WaitForSeconds(timeBetweenUses);
        StartCoroutine(StartChargeUp());
    }

    protected override void HandleCollision(Collider2D other) {}


    private void OnEnable()
    {
        _hasCoroutineStarted = true;
    }

    private void OnDisable()
    {
        _hasCoroutineStarted = false;
    }

    IEnumerator StartChargeUp()
    {
        while (true)
        {
            if (_hasCoroutineStarted)
            {
                PerformAbility();
                yield return _useDelay;
            }
        }
    }

    protected abstract void PerformAbility();
    
}
