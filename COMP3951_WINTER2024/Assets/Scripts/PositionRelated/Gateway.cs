using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Description:
///     Gateways are portals that switches the current scene to the next. Whenever a player enters the region, it teleports them
///         into a new scene.
/// Author: 
/// Date: March 5 2024 (Revision 04/13/2024)
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

    // All the game objects that needs to be moved in every scene switch.
    protected GameObject[] MovableGameObj;

    // The build index of the next scene.
    private int _nextScene;

    // Called on first frame. Assign the movable objects.
    private void Start()
    {
        MovableGameObj = GetMovableObjects();
    }


    // Get the game objects to be moved.
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
        // This is only meant for Player game objects.
        if (!other.gameObject.CompareTag("Player"))
            return;
        
        // Level up player (See RoomGateway.cs)
        PerformTransitionTask();
        
        // Start the switch.
        StartCoroutine(LoadSceneAsync());
        
        
    }

    // Loads the scene switching logic.
    private IEnumerator LoadSceneAsync()
    {
        // Usually, this is checking the case at the first Gateway in the lobby menu where the HUD and Player is still being chosen by the user.
        if (MovableGameObj[1].IsUnityNull())
            MovableGameObj[1] = GameObject.FindWithTag("Player");
        if (MovableGameObj[0].IsUnityNull())
            MovableGameObj[0] = GameObject.FindWithTag("HUD");
        
        // Get current active scene.
        var currentScene = SceneManager.GetActiveScene();

        // Load Scene
        _nextScene = GetNextScene();
        var asyncLoad = SceneManager.LoadSceneAsync(_nextScene, LoadSceneMode.Additive);
        
        // While not done
        while (!asyncLoad.isDone)
            yield return null;
        
        // Moves game object to the next scene.
        PerformMove();
        
        SceneManager.UnloadSceneAsync(currentScene);
        

    }

    /// <summary>
    /// <p>Moves the current movable game objects into the next scene.
    /// </p>
    /// <see cref="RoomGateway"/> for its overriden functionality.
    /// </summary>
    /// 
    protected virtual void PerformMove()
    {
        MoveObjectsToScene(_nextScene, MovableGameObj);   
    }

    /// <summary>
    /// Overloaded function for moving the game objects into a scene.
    /// Index is the build index number.
    /// </summary>
    /// <param name="index">Build index of the scene. Identified by Unity's build settings.</param>
    /// <param name="objs">Game objects to be moved.</param>
    public static void MoveObjectsToScene(int index, GameObject[] objs)
    {
        var nextScene = SceneManager.GetSceneByBuildIndex(index);
        MoveObjectsToScene(nextScene, objs);
    }

    /// <summary>
    /// Overloaded function for moving the game objects into a scene.
    /// Scene is the next scene to be used ot move the objects to.
    /// </summary>
    /// <param name="nextScene">Scene type, the next scene.</param>
    /// <param name="objs">The game objects that need to be moved.</param>
    protected static void MoveObjectsToScene(Scene nextScene, GameObject[] objs)
    {
        foreach (var gObject in objs)
        {
            if (!gObject.IsUnityNull())
                SceneManager.MoveGameObjectToScene(gObject, nextScene);
        }
    }

    /// <summary>
    /// Only uses RoomGateway's functionality. Increments the player level.
    /// </summary>
    protected virtual void PerformTransitionTask()
    {}

    /// <summary>
    /// Returns the build index of the next scene.
    /// </summary>
    /// <returns>As an integer, the build index of the next scene.</returns>
    protected abstract int GetNextScene();
}