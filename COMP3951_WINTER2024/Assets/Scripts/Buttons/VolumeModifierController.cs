using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeModifierController : MonoBehaviour
{
    [SerializeField] private Slider[] _slider;
    [SerializeField] private AudioMixer _audioMixer;
    public void SetBGVolume()
    {
        SetVolume(1, "Music");
    }
    
    public void SetSFXVolume()
    {
        SetVolume(0, "SFX");
    }

    private void SetVolume(int index, string type)
    {
        _slider[index].GetComponentInChildren<TextMeshProUGUI>().text = ((int) (_slider[index].value * 100)).ToString();
        _audioMixer.SetFloat(type, Mathf.Log10(_slider[index].value) * 20);
        Player.GetInstance().SetVolume(type, _slider[index].value);
    }
    // https://www.youtube.com/watch?v=BKCsH8mQ-lM
    // https://www.youtube.com/watch?v=G-JUp8AMEx0&list=PLf6aEENFZ4FuL4XSo0rEUgecY7FED8p-I&index=4
}
