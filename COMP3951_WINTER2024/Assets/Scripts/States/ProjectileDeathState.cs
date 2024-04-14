using UnityEngine;
using UnityEngine.Animations;

/// <summary>
///     When projectile death animation enters, blow up itself.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 13 2024
/// Source: Applied C# and Unity Skills
/// </summary>
public class ProjectileDeathState : OnDeathStateExit
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex,
        AnimatorControllerPlayable controller)
    {
        animator.gameObject.GetComponents<AudioSource>()[1].Play();
    }
}
