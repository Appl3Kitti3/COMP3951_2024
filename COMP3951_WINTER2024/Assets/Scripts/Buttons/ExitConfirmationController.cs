using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Used for the buttons of Yes and No when clicking or enabling the exit confirmation scene.
/// Each method is an EventHandler for OnClick.
///
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 12 2024
/// Source: Applied Unity Skills for OnClick buttons and MonoBehaviour types. 
/// </summary>
public class ExitConfirmationController : MonoBehaviour
{
    /// <summary>
    /// Called once the yes button is clicked.
    /// </summary>
    public void YesButton()
    {
        // saves the file in the current directory through ./Data/data.json
        Player.SaveGameToJson();
        Application.Quit();
    }

    /// <summary>
    /// Called once the no button is clicked.
    /// </summary>
    public void NoButton()
    {
        var s = SceneManager.GetSceneByName("Exit Confirmation");
        SceneManager.UnloadSceneAsync(s);
    }
}
