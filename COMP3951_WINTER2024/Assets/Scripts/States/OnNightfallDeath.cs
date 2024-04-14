using UnityEngine;
using UnityEngine.Animations;

/// <summary>
///     When Nightfall dies, play sound effect explosion.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 13 2024
/// Source: Applied C# and Unity Skills
/// </summary>
public class OnNightfallDeath : OnCreatureDeath
{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex,
        AnimatorControllerPlayable controller)
    {
        var sfxExplode = animator.gameObject.GetComponents<AudioSource>()[2];
        sfxExplode.PlayDelayed(0.5f);
    }
}
