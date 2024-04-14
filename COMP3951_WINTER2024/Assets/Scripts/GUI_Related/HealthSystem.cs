using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Description:
///     Health system, a GUI shows the amount of hearts left for the player. 
/// Author: 
/// Date: March 20 2024
/// Source:
///
///     Tutorial on displaying and modifying hearts
///     https://www.youtube.com/watch?v=3uyolYVsiWc
/// </summary>
public class HealthSystem : MonoBehaviour
{
    
    // Amount of heart sprites in the system
    [SerializeField]
    public Image[] hearts;

    // Called at the beginning of the frame once this is created.

    // Update is called once per frame.
    void Update()
    {
        var health = Player.GetHealth;
        var maxHealth = Player.GetMaxHealth;
        if (health > maxHealth)
            health = maxHealth;
        
        // Enable a certain amount of hearts.
        for (var i = 0; i < hearts.Length; i++)
        {
            hearts[i].color = i < health ? Color.red : Color.blue;
            hearts[i].enabled = i < maxHealth;
        }
    }
}
