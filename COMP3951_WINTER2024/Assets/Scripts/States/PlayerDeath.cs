using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
///     When player dies, load respawn scene.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 13 2024
/// Source: Applied C# and Unity Skills
/// </summary>
public class PlayerDeath : OnDeathStateExit
{
    // Whenever death animation ends.
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(GameObject.FindGameObjectWithTag("HUD"));
        SceneManager.LoadScene(3, LoadSceneMode.Additive);
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}