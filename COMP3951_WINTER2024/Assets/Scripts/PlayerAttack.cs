using UnityEngine;
// TODO make this into controller


/// <summary>
/// Description:
///     Player class is a singleton that represents the current stats of the player. (Damage, Health.)
///         It will have a weapon class to represent its weapon and its playable class to dynamically link
///         and switch to what class should be playing.
/// Author: Teddy Dumam-Ag A01329707
/// Date: March 6 2024 (Created around February)
/// Sources: Applied C# and OOP skills.
///
///     Tutorial on how to make player shoot or attack.
///     https://www.youtube.com/watch?v=mgjWA2mxLfI
///
///     State process of the animation tree in Unity.
///     https://www.youtube.com/watch?v=77dWGDFqcps
///
///     Detecting enemy or other game sprite collisions.
///     https://www.youtube.com/watch?v=ND1orPLw5EQ
///
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    // The animator used to modify the triggers and handle the animations.
    private Animator _animator;

    // called once the player has clicked the two mouse buttons.
    private bool _isAttacking;
    
    // Called once the controller is instantiated.
    void Awake()
    {
        Player.GetInstance();
        _animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Left Button
        if (Input.GetMouseButtonDown(0))
        {
            _animator.SetTrigger("Primary");
            Invoke(nameof(SetAttackTrue), 0.3f);
            Invoke(nameof(SetAttackFalse), 0.5f);
        }
        // Right Button
        else if (Input.GetMouseButtonDown(1))
        {
            _animator.SetTrigger("Ultimate");
            Invoke(nameof(SetAttackTrue), 0.5f);
            Invoke(nameof(SetAttackFalse), 0.9f);
        }
            
    }

    // Destroys the object by itself.
    void DestroySelf()
    {
        Destroy(gameObject);
    }
    
    // Update once or several times in a sequence of frames. (Not updated every frame)
    void FixedUpdate()
    {
        // Once the health points is less than 1, kill the player. Set the death animation.
        if (_animator.GetInteger("HP") < 1)
            Invoke(nameof(DestroySelf), 3f);           
    }

    // Set the attack boolean to true.
    private void SetAttackTrue()
    {
        _isAttacking = true;
    }

    // Set the attack boolean to false.
    private void SetAttackFalse()
    {
        _isAttacking = false;
    
    }
    // Player inflicts damage to the enemies when both are colliding.
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy"))
            return;
        if (_isAttacking)
        {
            Creature c = other.gameObject.GetComponent<Creature>();
            c.InflictDamage(Player.GetInstance().Damage);
        }
    }
    
    // Enemy only attacks when the player moves
    // Advantage to players
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy"))
            return;
        Creature c = other.gameObject.GetComponent<Creature>();
            
        Player.GetInstance().InflictDamage(c.damage, c.animator);
        StartCoroutine(Player.GetInstance().ImmunityFrames());   
    }
}
