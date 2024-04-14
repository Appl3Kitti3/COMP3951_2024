using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Used for the respawn buttons found in the respawn scene when the player dies.
///
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 12 2024
/// Source: Applied Unity Skills for OnClick buttons and MonoBehaviour types. 
/// </summary>
public class RespawnButtonController : MonoBehaviour
{
    /// <summary>
    /// Called once the respawn button is clicked.
    /// </summary>
    public void Respawn()
    {
        // 0 is Setup in build index. And setup calls Menu (build index = 1)
        // Single scene loading means it unloads all the scenes active and replace it with this scene.
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        Player.Reset();
    }

    /// <summary>
    /// Called once the exit button is clicked.
    /// </summary>
    public void Exit()
    {
        // Additive scene loading means that it is added as another scene to the current active scenes.
        SceneManager.LoadSceneAsync("Exit Confirmation", LoadSceneMode.Additive);
    }
}
