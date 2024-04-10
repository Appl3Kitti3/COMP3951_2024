using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnGameLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        /*x = GameObject.FindObjectsOfType<AudioSource>(true);
        Debug.Log(x.Length);*/
    }

}
