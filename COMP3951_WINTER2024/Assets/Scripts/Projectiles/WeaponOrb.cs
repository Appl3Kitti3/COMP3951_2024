using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponOrb : MonoBehaviour
{
    private Animator _animator;

    private int _counter;
    private static readonly int Collected = Animator.StringToHash("Collected");

    private AudioSource _pickUp;
    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _pickUp = GetComponent<AudioSource>();
    }

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
