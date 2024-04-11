using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Description:
///     Gateways are portals that switches the current scene to the next. Whenever a player enters the region, it teleports them
///         into a new scene.
/// Author: 
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
public abstract class Gateway : MonoBehaviour
{
    private bool _isRendering;

    protected GameObject[] MovableGameObj;

    private int _nextScene;
    private GameObject _player;
    private GameObject _hud;

    private void Start()
    {

        MovableGameObj = GetMovableObjects();
    }


    private static GameObject[] GetMovableObjects()
    {
        return new[]
        {
            GameObject.FindWithTag("HUD"),
            GameObject.FindWithTag("Player"),
            GameObject.FindWithTag("CrossHair"), 
            GameObject.FindWithTag("MainCamera"),
            null
        };
    }

    // Called once the player enters the region of the gateway.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;
            
        PerformTransitionTask();
        StartCoroutine(LoadSceneAsync());
        // unload scene asnyc
        
        
    }

    // Loads the scene switching logic.
    private IEnumerator LoadSceneAsync()
    {
        if (MovableGameObj[1].IsUnityNull())
            MovableGameObj[1] = GameObject.FindWithTag("Player");
        if (MovableGameObj[0].IsUnityNull())
            MovableGameObj[0] = GameObject.FindWithTag("HUD");
        Scene currentScene = SceneManager.GetActiveScene();

        // Load Scene
        _nextScene = GetNextScene();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_nextScene, LoadSceneMode.Additive);
        
        // While not done
        while (!asyncLoad.isDone)
            yield return null;
        
        
        // TODO: Future plan, move player class to the Player Game Object.
        PerformMove();
        /*SceneManager.MoveGameObjectToScene(GameObject.FindGameObjectWithTag("MainCamera"), nextScene);*/
        /*SceneManager.MoveGameObjectToScene(GameObject.FindGameObjectWithTag("BackgroundSystems"), nextScene);*/
        
        
        SceneManager.UnloadSceneAsync(currentScene);
        

    }



    protected virtual void PerformMove()
    {
        MoveObjectsToScene(_nextScene, MovableGameObj);   
    }

    public static void MoveObjectsToScene(int index, GameObject[] objs)
    {
        var nextScene = SceneManager.GetSceneByBuildIndex(index);
        MoveObjectsToScene(nextScene, objs);
    }

    public static void MoveObjectsToScene(Scene nextScene, GameObject[] objs)
    {
        foreach (var gObject in objs)
        {
            if (!gObject.IsUnityNull())
                SceneManager.MoveGameObjectToScene(gObject, nextScene);
        }
    }

    protected virtual void PerformTransitionTask()
    {}

    protected abstract int GetNextScene();
}