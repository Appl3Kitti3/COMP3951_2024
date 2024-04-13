using UnityEngine;
using UnityEngine.Audio;

public class MixerController : MonoBehaviour
{
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
