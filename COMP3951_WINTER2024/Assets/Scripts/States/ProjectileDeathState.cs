using UnityEngine;
using UnityEngine.Animations;

public class ProjectileDeathState : OnDeathStateExit
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex,
        AnimatorControllerPlayable controller)
    {
        animator.gameObject.GetComponents<AudioSource>()[1].Play();
    }
}
