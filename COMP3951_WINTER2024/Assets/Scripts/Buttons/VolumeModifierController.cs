using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeModifierController : MonoBehaviour
{
    [SerializeField] private Slider[] slider;
    [SerializeField] private AudioMixer audioMixer;
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
        slider[index].GetComponentInChildren<TextMeshProUGUI>().text = ((int) (slider[index].value * 100)).ToString();
        audioMixer.SetFloat(type, Mathf.Log10(slider[index].value) * 20);
        Player.SetVolume(type, slider[index].value);
    }
    // https://www.youtube.com/watch?v=BKCsH8mQ-lM
    // https://www.youtube.com/watch?v=G-JUp8AMEx0&list=PLf6aEENFZ4FuL4XSo0rEUgecY7FED8p-I&index=4
}
