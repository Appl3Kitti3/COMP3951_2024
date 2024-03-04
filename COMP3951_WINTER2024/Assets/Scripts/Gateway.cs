using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gateway : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // https://stackoverflow.com/questions/61279854/unity-getting-an-object-to-pass-through-another-object-but-still-trigger-collisi
    // https://www.youtube.com/watch?v=Bc9lmHjqLZc
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Pen Pineapple Apple Pen");
        if (!other.gameObject.CompareTag("Player"))
            return;
        
        
        StartCoroutine(LoadSceneAsync());

        // unload scene asnyc
    }

    private IEnumerator LoadSceneAsync()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        // Load Scene
        // https://www.youtube.com/watch?v=pMXJv9zzkGg
        // https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadSceneAsync.html
        // https://docs.unity3d.com/ScriptReference/SceneManagement.LoadSceneMode.html
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SimpleRoom", LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        Scene nextScene = SceneManager.GetSceneByName("SimpleRoom");
        // https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.MoveGameObjectToScene.html
        SceneManager.MoveGameObjectToScene(GameObject.FindGameObjectWithTag("Player"), nextScene);
        SceneManager.MoveGameObjectToScene(GameObject.FindGameObjectWithTag("MainCamera"), nextScene);
        SceneManager.UnloadSceneAsync(currentScene);
        Vector3 playerSpawn = GameObject.FindGameObjectWithTag("InitialPosition").GetComponent<Transform>().position;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position = playerSpawn;
    }
}