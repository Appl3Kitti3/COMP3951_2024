using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LockController : MonoBehaviour
{ 
    [Header("Related Game Objects")]
    [SerializeField] private GameObject[] lockedDisplays;
    // 0 --> Lock Structure
    // 1 --> Text "Requirement: N Score"

    [SerializeField] private GameObject[] unlockedDisplays;

    
    [Header("Requirements")] 
    [SerializeField] [SerializeAs("Price")] private int scoreRequirement;

    [Header("Type")] [SerializeField] private string type;

    private Toggle _toggle;

    // Start is called before the first frame update
    private void Start()
    {
        _toggle = unlockedDisplays[0].GetComponent<Toggle>();
        if (Player.HighScore >= scoreRequirement)
        {
            foreach (var gO in lockedDisplays)
                gO.SetActive(false);
            
            foreach (var gO in unlockedDisplays)
                gO.SetActive(true);
            
            _toggle.graphic.enabled = DetermineType();
        }
        else
        {
            lockedDisplays[1].GetComponent<TextMeshProUGUI>()
                .text = $"Requires\n{scoreRequirement} Score";
        }
    }

    private void Update()
    {
        _toggle.graphic.enabled = DetermineType();
    }

    private bool DetermineType()
    {
        return type switch
        {
            "LIF" => Player.GetFlag("IFrames"),
            "LDice" => Player.GetFlag("LDice"),
            "BProjectile" => Player.GetFlag("BProjectile"),
            _ => false
        };
    }
}
