using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class SoundSliders : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Slider tmp = GetComponent<Slider>();
        tmp.value = GetValue();
        TextMeshProUGUI textTmp = GetComponentInChildren<TextMeshProUGUI>();
        textTmp.text = ((int)(tmp.value * 100)).ToString();
    }

    protected abstract float GetValue();
}
