using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Description:
///     Gateways are portals that switches the current scene to the next. Whenever a player enters the region, it teleports them
///         into a new scene.
/// Author: Teddy Dumam-Ag A01329707
/// Date: March 5 2024
/// Sources:
///
///     Discovery of trigger property.
///     https://stackoverflow.com/questions/61279854/unity-getting-an-object-to-pass-through-another-object-but-still-trigger-collisi
///
///
///     Tutorial on how to use triggers
///     https://www.youtube.com/watch?v=Bc9lmHjqLZc
/// 
///     How to pass variables through different scenes. (Idea)
///     https://www.youtube.com/watch?v=pMXJv9zzkGg
///
///     LoadSceneAsync and LoadSceneMode
///     https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadSceneAsync.html
///     https://docs.unity3d.com/ScriptReference/SceneManagement.LoadSceneMode.html
///
///     MoveGameObjectToScene
///     https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.MoveGameObjectToScene.html
///
/// </summary>
public class Gateway : MonoBehaviour
{
    // Called once the player enters the region of the gateway.
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Pen Pineapple Apple Pen");
        if (!other.gameObject.CompareTag("Player"))
            return;
        StartCoroutine(LoadSceneAsync());
        // unload scene asnyc
    }

    // Loads the scene switching logic.
    private IEnumerator LoadSceneAsync()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        // Load Scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SimpleRoom", LoadSceneMode.Additive);

        // While not done
        while (!asyncLoad.isDone)
            yield return null;

        // Example scene right now
        Scene nextScene = SceneManager.GetSceneByName("SimpleRoom");
        
        // TODO: Future plan, move player class to the Player Game Object.
        SceneManager.MoveGameObjectToScene(GameObject.FindGameObjectWithTag("Player"), nextScene);
        SceneManager.MoveGameObjectToScene(GameObject.FindGameObjectWithTag("MainCamera"), nextScene);
        
        SceneManager.UnloadSceneAsync(currentScene);
        
        Vector3 playerSpawn = GameObject.FindGameObjectWithTag("InitialPosition").GetComponent<Transform>().position;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position = playerSpawn;
    }
}