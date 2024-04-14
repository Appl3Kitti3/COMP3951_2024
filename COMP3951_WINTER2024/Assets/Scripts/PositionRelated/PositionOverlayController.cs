using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
///     Used in the lobby. Once the player moves to the grate looking structure, it shows settings. The bottomless pit shows exit.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 13 2024
/// Source: Applied C# and Unity Skills
/// </summary>
public class PositionOverlayController : MonoBehaviour // for some reason Unity needs a comment here for this to work
{
    // Scene name of the next scene as overlay.
    private string _sceneName;
    
    // Start is called before the first frame update
    private void Start()
    {
        _sceneName = GetSceneName();
    }

    // Once player moves to that area, load the scene.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Additive);
            
    }

    // When player moves out of the region, unload.
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        try
        {
            SceneManager.UnloadSceneAsync(_sceneName);
        } catch(ArgumentException) {}

    }

    // Gets the scene name.
    protected virtual string GetSceneName()
    {
        // by default
        return "Settings";
    }
}
