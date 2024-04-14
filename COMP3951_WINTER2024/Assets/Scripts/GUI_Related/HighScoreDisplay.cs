using TMPro;
using UnityEngine;

/// <summary>
///     Shows High Score display on Shop.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 12 2024
/// Source: Applied C# and Unity Skills
/// </summary>
public class HighScoreDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        GetComponent<TextMeshProUGUI>().text = $"Highest Score:\n{Player.HighScore}";
    }
}
