using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     Sets the value and the text beside the slider based on the player's data.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 12 2024
/// Source: Applied C# and Unity Skills
/// </summary>
public abstract class SoundSliders : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        var tmp = GetComponent<Slider>();
        tmp.value = GetValue();
        var textTmp = GetComponentInChildren<TextMeshProUGUI>();
        textTmp.text = ((int)(tmp.value * 100)).ToString();
    }

    // Get the value of the volume based on the sound type.
    protected abstract float GetValue();
}
