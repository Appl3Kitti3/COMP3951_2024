using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private string _stringTemplate;
    
    private void Start()
    {
        _text = gameObject.GetComponent<TextMeshProUGUI>();
        _stringTemplate = _text.text;
    }

    private void Update()
    {
        _text.text = $"{_stringTemplate}\n{Player.Score}";
    }
}
