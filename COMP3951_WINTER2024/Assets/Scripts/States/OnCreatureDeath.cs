using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCreatureDeath : OnDeathStateExit
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Creature c = animator.gameObject.GetComponent<Creature>();
        Player.GetInstance().Score += c.PointValue;
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
