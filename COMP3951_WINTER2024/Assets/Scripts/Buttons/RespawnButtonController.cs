using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnButtonController : MonoBehaviour
{
    public void Respawn()
    {
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        Player.Reset();
    }

    public void Exit()
    {
        SceneManager.LoadSceneAsync("Exit Confirmation", LoadSceneMode.Additive);
    }
}
