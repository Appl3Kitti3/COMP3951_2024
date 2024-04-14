using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
///     Called on Setup.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 13 2024
/// Source: Applied C# and Unity Skills
/// </summary>
public class OnGameLoad : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        // Launch Menu lobby scene.
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }

}
