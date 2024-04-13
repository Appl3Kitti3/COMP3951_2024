using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PositionOverlayController : MonoBehaviour
{
    private string _sceneName;
    
    // Start is called before the first frame update
    private void Start()
    {
        _sceneName = GetSceneName();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Additive);
            
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        try
        {
            SceneManager.UnloadSceneAsync(_sceneName);
        } catch(ArgumentException) {}

    }

    protected virtual string GetSceneName()
    {
        // by default
        return "Settings";
    }
}
