using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PositionOverlayController : MonoBehaviour
{
    private string _sceneName;
    
    // Start is called before the first frame update
    void Start()
    {
        _sceneName = GetSceneName();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Additive);
        }
            
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            try
            {
                SceneManager.UnloadSceneAsync(_sceneName);
            } catch(ArgumentException e)
            {}
            
        }

    }

    protected virtual string GetSceneName()
    {
        // by default
        return "Settings";
    }
}
