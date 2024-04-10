using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : Attack
{

    protected override void Init()
    {
        ParentObject = transform.parent.gameObject;
    }
}

