using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
///     Gives player a weapon buff and has a 50% of obtaining a health back.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 13 2024
/// Source: Applied C# and Unity Skills
/// </summary>
public class WeaponOrb : MonoBehaviour
{
    // Animator of the weapon orb.
    private Animator _animator;
    
    // Turns the animator parameter into an integer id.
    private static readonly int Collected = Animator.StringToHash("Collected");

    // Sound effect played after player picks up the buff.
    private AudioSource _pickUp;
    
    // Called before first frame.
    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _pickUp = GetComponent<AudioSource>();
    }

    // Called when player collects the pick up.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        _pickUp.Play();
        Player.ChosenClass.Weapon.GainBoost();
        var rngFreeHealth = Random.Range(0, 2);
        if (rngFreeHealth == 1)
            Player.IncrementHealth();
        _animator.SetTrigger(Collected);

    }
}
