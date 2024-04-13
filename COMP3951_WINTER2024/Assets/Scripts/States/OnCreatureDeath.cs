using UnityEngine;

public class OnCreatureDeath : OnDeathStateExit
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var c = animator.gameObject.GetComponent<Creature>();
        Player.Score += c.PointValue;
        Room.Instance.EnemyCount--;
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
    
    
}
