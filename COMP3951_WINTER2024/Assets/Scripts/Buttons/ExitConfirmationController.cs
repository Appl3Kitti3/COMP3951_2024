using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitConfirmationController : MonoBehaviour
{
    public void YesButton()
    {
        Player.GetInstance().SaveGameToJson();
        Application.Quit();
    }

    public void NoButton()
    {
        Scene s = SceneManager.GetSceneByName("Exit Confirmation");
        SceneManager.UnloadSceneAsync(s);
    }
}
