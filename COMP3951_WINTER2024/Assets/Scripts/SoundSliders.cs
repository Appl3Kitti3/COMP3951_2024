using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    protected abstract float GetValue();
}
