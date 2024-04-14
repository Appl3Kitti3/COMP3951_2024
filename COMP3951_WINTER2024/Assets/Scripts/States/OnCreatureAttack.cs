using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

/// <summary>
///     State whenever the creature attacks.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 13 2024
/// Source: Applied C# and Unity Skills
/// </summary>
public class OnCreatureAttack : StateMachineBehaviour
{
    // When attack animation ends, disable the hit-box.
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        var childBoundary = animator.gameObject.transform.GetChild(0).gameObject;
        childBoundary.SetActive(false);
    }

    // When attack animation starts, enable the hit-box.
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var sfx = animator.gameObject.GetComponents<AudioSource>()[1];
        var childBoundary = animator.gameObject.transform.GetChild(0).gameObject;
        sfx.Play();
        childBoundary.SetActive(true);
    }
}
