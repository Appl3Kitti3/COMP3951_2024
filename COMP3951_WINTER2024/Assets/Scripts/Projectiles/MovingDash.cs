using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDash : Projectile
{
    protected override void SetRotation()
    {
        Target.y = 0f;
        GetComponent<SpriteRenderer>().flipX = !(Target.x < 0);
    }
}
