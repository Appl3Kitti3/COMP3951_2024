using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Description:
///     Player class is a singleton that represents the current stats of the player. (Damage, Health.)
///         It will have a weapon class to represent its weapon and its playable class to dynamically link
///         and switch to what class should be playing.
/// Author: Teddy Dumam-Ag A01329707
/// Date: March 6 2024 (Created around February)
/// Sources: Applied C# and OOP skills.
/// </summary>
public class Player
{
    // Health of the player.
    private int _health;
    
    // Animator object of the player gameObject that represents its animation sprite.
    private Animator _animator;
    
    // Get animator
    public Animator Animator
    {
        get { return _animator; }
    }
    
    // Singleton object of the Player class.
    private static Player _instance;
    
    // Get the damage of the player instance. Currently used as a example prototype
    // however, it will be calculated through the weapon class.
    public int Damage { get; set; }
    
    // Checks if an enemy has entered the immunity frame region.
    public bool HasEnteredImmunityFramesRegion { get; set; }

    /// <summary>
    /// Create the player class.
    /// </summary>
    /// <param name="hp">Health points.</param>
    /// <param name="dmg">Base damage of the player.</param>
    /// <param name="animator">Animation of the gameObject.</param>
    private Player(int hp, int dmg, Animator animator) {
        _health = hp;
        Damage = dmg;
        _animator = animator;
        _animator.SetInteger("HP", _health);
    }

    // Get the only singleton instance of the player.
    public static Player GetInstance(int maxhp = 9999, int damage = 25, Animator animator = null)
    {
        if (_instance.IsUnityNull()) 
            _instance = new Player(maxhp, damage, animator);
        else if (_instance._animator.IsUnityNull())
            _instance._animator = animator;
        return _instance;
    }   

    // Inflict damage to the player.
    public void InflictDamage(int damage, Animator animation)
    {
        animation.SetTrigger("Hit");
        if (_animator.IsDestroyed())
            return;
        _animator.SetTrigger("Hit");
        _health -= damage;
        _animator.SetInteger("HP", _health);
    }
    
    // Enable Immunity Frame Region
    public void EnableITRegions()
    {
        HasEnteredImmunityFramesRegion = true;
        
    }
    
    // Disable Immunity Frame Region
    public void DisableITRegions()
    {
        HasEnteredImmunityFramesRegion = false;
    }
}
