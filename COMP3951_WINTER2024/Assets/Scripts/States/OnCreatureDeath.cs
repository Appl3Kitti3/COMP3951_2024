using UnityEngine;

/// <summary>
///     Called on Creature Animation death.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 13 2024
/// Source: Applied C# and Unity Skills
/// </summary>
public class OnCreatureDeath : OnDeathStateExit
{
    // When death animation exits, destroy self and add points to player and decrement room data enemy count.
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var c = animator.gameObject.GetComponent<Creature>();
        Player.Score += c.PointValue;
        Room.Instance.EnemyCount--;
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
    
    
}
