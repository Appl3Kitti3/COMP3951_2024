using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nightfall : Boss
{
    private WaitForSeconds _delay = new WaitForSeconds(0.24f);
    protected override void PerformPrimary()
    {
        Projectile.SetActive(true);
    }

    protected override void BeforeWait()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return _delay;
        Projectile.SetActive(false);
    }
}
