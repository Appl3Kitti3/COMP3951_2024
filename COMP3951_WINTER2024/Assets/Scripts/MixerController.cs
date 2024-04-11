using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    // Start is called before the first frame update
    void Start()
    {
        float music = Player.GetInstance().GetBGMusicVolume();
        _mixer.SetFloat("Music", Mathf.Log10(music) * 20);
        float sfx = Player.GetInstance().GetSFXVolume();
        _mixer.SetFloat("SFX", Mathf.Log10(sfx) * 20);
    }
}
