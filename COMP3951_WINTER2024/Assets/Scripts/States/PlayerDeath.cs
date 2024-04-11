using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : OnDeathStateExit
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(GameObject.FindGameObjectWithTag("HUD"));
        SceneManager.LoadScene(3, LoadSceneMode.Additive);
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}

// if Respawn Button clicked
//     if Current Score >= High score
//        Set High score
//      
//
//       Save High Score to file
//