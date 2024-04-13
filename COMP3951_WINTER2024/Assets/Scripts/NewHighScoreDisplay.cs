using UnityEngine;

public class NewHighScoreDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        if (!Player.IsNewHighScore(Player.Score))
            gameObject.SetActive(false);
    }
}
