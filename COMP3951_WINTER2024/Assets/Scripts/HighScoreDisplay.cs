using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = $"Highest Score:\n${Player.GetInstance().HighScore}";
    }
}
