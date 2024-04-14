using TMPro;
using UnityEngine;

/// <summary>
///     Displays the score GUI.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 12 2024
/// Source: Applied C# and Unity Skills
/// </summary>
public class ScoreCounter : MonoBehaviour
{
    // Text Content / Component of the Score.
    private TextMeshProUGUI _text;

    // String template used to add with the score to.
    private string _stringTemplate;
    
    // Called on first frame.
    // Initialize objects.
    private void Start()
    {
        _text = gameObject.GetComponent<TextMeshProUGUI>();
        _stringTemplate = _text.text;
    }

    // Called in every update.
    private void Update()
    {
        _text.text = $"{_stringTemplate}\n{Player.Score}";
    }
}
