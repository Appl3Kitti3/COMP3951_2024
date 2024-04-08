using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Description:
///     Health system, a GUI shows the amount of hearts left for the player. 
/// Author: Tedrik "Teddy" Dumam-Ag
/// Date: March 20 2024
/// Source:
///
///     Tutorial on displaying and modifying hearts
///     https://www.youtube.com/watch?v=3uyolYVsiWc
/// </summary>
public class HealthSystem : MonoBehaviour
{

    // Animator of the player
    private Animator _playerAnimator;

    // Health of the player
    private int _health;

    // Maximum health of the player, tells the maximum of life sprites will be drawn
    private int _maxHealth;

    // Amount of heart sprites in the system
    public Image[] hearts;

    // Called at the beginning of the frame once this is created.
    private void Start()
    {
        _playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        _health = _maxHealth = _playerAnimator.GetInteger("HP");
    }
    
    // Update is called once per frame.
    void Update()
    {
        if (_playerAnimator.IsDestroyed())
            return;
        _health = _playerAnimator.GetInteger("HP");
        
        if (_health > _maxHealth)
            _health = _maxHealth;
        
        // Enable a certain amount of hearts.
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < _health)
                hearts[i].color = Color.red;
            else
                hearts[i].color = Color.blue;

            if (i < _maxHealth)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }
    }
}
