using UnityEngine;

/// <summary>
///     Called on any death state. 
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 13 2024
/// Source: Applied C# and Unity Skills
/// </summary>
public class OnDeathStateExit : StateMachineBehaviour
{
    // Destroy object when death animation ends.
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(animator.gameObject);
    }
}
