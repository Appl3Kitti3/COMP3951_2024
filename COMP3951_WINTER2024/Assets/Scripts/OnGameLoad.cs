using UnityEngine;
using UnityEngine.SceneManagement;

public class OnGameLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Huh");
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }

}
