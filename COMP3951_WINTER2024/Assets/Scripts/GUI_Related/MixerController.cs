using UnityEngine;
using UnityEngine.Audio;

/// <summary>
///     Used to control the sounds with AudioMixer.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 12 2024
/// Source: Applied C# and Unity Skills
/// </summary>
public class MixerController : MonoBehaviour
{
    // Audio Mixer used to manipulate the sounds volume.
    [SerializeField] private AudioMixer mixer;
    // Start is called before the first frame update
    private void Start()
    {
        var music = Player.GetBgMusicVolume();
        mixer.SetFloat("Music", Mathf.Log10(music) * 20);
        var sfx = Player.GetSfxVolume();
        mixer.SetFloat("SFX", Mathf.Log10(sfx) * 20);
    }
}
