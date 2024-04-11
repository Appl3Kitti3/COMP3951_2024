using TMPro;
using UnityEngine;

public class HighScoreDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        GetComponent<TextMeshProUGUI>().text = $"Highest Score:\n${Player.HighScore}";
    }
}
