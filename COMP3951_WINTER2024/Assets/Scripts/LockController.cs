using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     Used as a locking display if the player has not reached the high score requirement in the shop.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 13 2024
/// Source: Applied C# and Unity Skills
/// </summary>
public class LockController : MonoBehaviour
{ 
    [Header("Related Game Objects")]
    
    // The locked displays that are initially displayed.
    [SerializeField] private GameObject[] lockedDisplays;
    // 0 --> Lock Structure
    // 1 --> Text "Requirement: N Score"

    // Displays or UI objects displayed after requirement is satisfied.
    [SerializeField] private GameObject[] unlockedDisplays;

    
    [Header("Requirements")] 
    // Score Requirement value.
    [SerializeField] [SerializeAs("Price")] private int scoreRequirement;

    // Type of the ability.
    [Header("Type")] [SerializeField] private string type;

    // Get the Toggle used to toggle the status of the ability.
    private Toggle _toggle;

    // Start is called before the first frame update
    private void Start()
    {
        _toggle = unlockedDisplays[0].GetComponent<Toggle>();
        // If player's high score is enough.
        if (Player.HighScore >= scoreRequirement)
        {
            // Deactivate the locked displays
            foreach (var gO in lockedDisplays)
                gO.SetActive(false);
            
            // Activate the unlocked displays.
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

    // Called on every frame.
    private void Update()
    {
        _toggle.graphic.enabled = DetermineType();
    }

    /// <summary>
    /// Get the boolean status of an ability.
    /// </summary>
    /// <returns>True or False, the status based on the type.</returns>
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
