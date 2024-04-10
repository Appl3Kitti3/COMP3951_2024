using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Image = UnityEngine.UIElements.Image;

public class LockController : MonoBehaviour
{ 
    [Header("Related Game Objects")]
    [SerializeField] private GameObject[] _lockedDisplays;
    // 0 --> Lock Structure
    // 1 --> Text "Requirement: N Score"

    [SerializeField] private GameObject[] _unlockedDisplays;

    [Header("Requirements")] 
    [SerializeField] [SerializeAs("Price")] private int _scoreRequirement;

    [Header("Type")] [SerializeField] private string type;
    // Start is called before the first frame update
    void Start()
    {
        if (Player.GetInstance().HighScore >= _scoreRequirement)
        {
            foreach (var gO in _lockedDisplays)
                gO.SetActive(false);
            
            foreach (var gO in _unlockedDisplays)
                gO.SetActive(true);
            
            _unlockedDisplays[0].GetComponent<Toggle>().graphic.enabled = DetermineType();
        }
        else
        {
            _lockedDisplays[1].GetComponent<TextMeshProUGUI>()
                .text = $"Requires\n{_scoreRequirement} Score";
        }
    }

    private void Update()
    {
        _unlockedDisplays[0].GetComponent<Toggle>().graphic.enabled = DetermineType();
    }

    bool DetermineType()
    {
        switch (type)
        {
            case "LIF":
                return Player.GetInstance().GetFlag("IFrames");
            case "LDice":
                return Player.GetInstance().GetFlag("LDice");
            case "BProjectile":
                return Player.GetInstance().GetFlag("BProjectile");
            default:
                return false;
        }
    }
}
