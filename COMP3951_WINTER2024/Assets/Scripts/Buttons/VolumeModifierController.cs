using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// Uses an audio mixer approach for changing the volume of Background Music and Sound Effects.
/// 
/// Author: Tedrik "Teddy" Dumam-Ag 
/// Date: April 12 2024
/// Sources: Applied C# and OOP skills.
/// Audio Mixer Tutorial (Changing the volume of a sound type)
/// https://www.youtube.com/watch?v=G-JUp8AMEx0&list=PLf6aEENFZ4FuL4XSo0rEUgecY7FED8p-I&index=4
///
/// Audio Sources Tutorial. (That is how sound is played)
/// https://www.youtube.com/watch?v=BKCsH8mQ-lM
/// </summary>
public class VolumeModifierController : MonoBehaviour
{
    // The two sliders are used to display its value when the player slides the circle.
    // Plus, it is used to display the value as an integer beside it.
    [SerializeField] private Slider[] slider;
    
    // This is used to determine modifying the audio mixer's property.
    [SerializeField] private AudioMixer audioMixer;
    
    /// <summary>
    /// Sets the Music volume value tab in the audio mixer. 
    /// </summary>
    public void SetBgVolume()
    {
        SetVolume(1, "Music");
    }
    
    /// <summary>
    /// Sets the Sfx volume value tab in the audio mixer.
    /// </summary>
    public void SetSfxVolume()
    {
        SetVolume(0, "SFX");
    }

    /// <summary>
    /// Changes the text value of the slider and changes the volume of the audio mixer based on the type.
    /// </summary>
    /// <param name="index">Index, 0 or 1, indicating the music slider used.</param>
    /// <param name="type">A String, Music or SFX, it is used to set the volume in the audio mixer.</param>
    private void SetVolume(int index, string type)
    {
        slider[index].GetComponentInChildren<TextMeshProUGUI>().text = ((int) (slider[index].value * 100)).ToString();
        audioMixer.SetFloat(type, Mathf.Log10(slider[index].value) * 20);
        Player.SetVolume(type, slider[index].value);
    }
}
