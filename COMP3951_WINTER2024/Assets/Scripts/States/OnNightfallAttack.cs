using UnityEngine;
using UnityEngine.Animations;

/// <summary>
/// Nightfall's attack animation state.
/// It literally is the same code to its parent but I only changed the index to 1 because
/// that's how the bosses operate.
/// Since Necro does not have a melee ability, we only restrict this to Nightfall.
///
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Source: Learned C# and Unity Skills
/// </summary>
public class OnNightfallAttack : OnCreatureAttack
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        var childBoundary = animator.gameObject.transform.GetChild(1).gameObject;
        childBoundary.SetActive(false);
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex,
        AnimatorControllerPlayable controller)
    {
        var sfx = animator.gameObject.GetComponents<AudioSource>()[1];
        var childBoundary = animator.gameObject.transform.GetChild(1).gameObject;
        sfx.Play();
        childBoundary.SetActive(true);
    }
}
