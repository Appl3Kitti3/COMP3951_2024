using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewHighScoreDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!Player.GetInstance().IsNewHighScore(Player.GetInstance().Score))
            gameObject.SetActive(false);
    }
}
