using UnityEngine;

/// <summary>
///     Called in the respawn screen. Only shows up if the Player's score is greater than the current high score.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 12 2024
/// Source: Applied C# and Unity Skills
/// </summary>
public class NewHighScoreDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        if (!Player.IsNewHighScore())
            gameObject.SetActive(false);
    }
}
