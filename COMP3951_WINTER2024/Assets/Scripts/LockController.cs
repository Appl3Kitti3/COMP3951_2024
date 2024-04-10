using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class LockController : MonoBehaviour
{ 
    [Header("Related Game Objects")]
    [SerializeField] private GameObject[] _lockedDisplays;
    // 0 --> Lock Structure
    // 1 --> Text "Requirement: N Score"

    [SerializeField] private GameObject[] _unlockedDisplays;

    [Header("Requirements")] 
    [SerializeField] [SerializeAs("Price")] private int _scoreRequirement;
    
    // Start is called before the first frame update
    void Start()
    {
        if (Player.GetInstance().HighScore >= _scoreRequirement)
        {
            foreach (var gO in _lockedDisplays)
                gO.SetActive(false);
            
            foreach (var gO in _unlockedDisplays)
                gO.SetActive(true);
        }
        else
        {
            _lockedDisplays[1].GetComponent<TextMeshProUGUI>()
                .text = $"Requires\n{_scoreRequirement} Score";
        }
    }
}
