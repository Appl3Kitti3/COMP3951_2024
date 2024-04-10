using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

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
        _text.text = $"{_stringTemplate}\n{Player.GetInstance().Score}";
    }
}
