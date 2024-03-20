using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Description:
///     A creature or the enemy of the game. It has its necessary functions to operate a normal dungeon crawler life cycle.
/// Author: Teddy Dumam-Ag A01329707
/// Date: March 5 2024 (Created around February)
/// Sources:
///
///     Destroy self once an operation is called.
///     https://forum.unity.com/threads/how-would-i-destroy-self-after-collision.353037/
///     Location (1)
///
///     Introduced the idea of invoking functions as threads but async to the game updates.
///     Each invoke is a timer to a function call.
///     https://forum.unity.com/threads/changing-colors-every-couple-of-seconds.179708/
///     Location (2)
///
///     Introduced the way of flipping the sprite image horizontally.
///     https://www.youtube.com/watch?v=RuvfOl8HhhM
///     Location (3)
///
///     Tutorial of EnemyAI following the player.
///     https://www.youtube.com/watch?v=2SXa10ILJms
///
///     Knock back tutorial
///     https://youtu.be/RE0aWe7ByAI
/// </summary>
public class Creature : MonoBehaviour
{
    [Header ("Stats")]
 
    // Current level of the dungeon creature.
    [FormerlySerializedAs("Level")] public int level;
    
    // The max health of the creature. (Assign this to the current health of the player)
    [FormerlySerializedAs("MaxHealth")] public int maxHealth;
    
    // The current defense of the creature.
    [FormerlySerializedAs("Defense")] public int def;

    // The current damage of the creature.
    // in Future stand points, this will be the bae damage of the creature, and its true damage
    // is calculated based on the level and something else.
    [FormerlySerializedAs("Damage")] public int damage;
    
    [Header("Creature Configuration")] 
    // The maximum distance a creature can move towards the player before it goes back to
    // idle.
    [SerializeField]
    private float maximumDistance;
    
    // The move speed of the creature. (Walking speed)
    [SerializeField]
    private float moveSpeed;
    
    // The animator used to animate the sprite.
    public Animator animator;

    // TODO: Keep commented code for now.
    /*private string Name;*/
    
    // Current health of the creature.
    private int _health;
    
    // The player game object from the current scene.
    private GameObject _player;
    
    // The distance calculated between the player and the creature.
    private float _distance;
    
    // The direction of the creature facing.
    private Vector2 _direction;
    
    // The sprite renderer of the creature. Used to manipulate its colors and have a damage
    // color effect.
    private SpriteRenderer _rend;

    private bool _isInflicted;
    // Called once creature is instantiated.
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Called as a timer invoke function, changes the sprite color to red.
    void InflictHitColor()
    {
        _rend.color = Color.red;
    }
    
    // Called as a timer invoke function, reverts back the sprite color.
    void RevertColor()
    {
        _rend.color = Color.white;
    }
    
    // TODO (Death does not match with animation.)
    // Destroys itself.
    void GetRidOfSelf()
    {
// -------------------------------------------------------------------------------------------------------------------- (1)
        Debug.Log("dead");
        Destroy(gameObject);
    }

    /// <summary>
    /// Inflicts damage to the creature.
    /// </summary>
    /// <param name="dmg">The amount of damaged done by the opponent.</param>
    public void InflictDamage(int dmg)
    {
        // TODO (Keep this for melee weapons)
        // Move this logic to the Melee class
        _health -= (dmg - def);
        
        
        // Double change color because the animation hits the enemy twice.
        _isInflicted = true;
        Invoke(nameof(InflictHitColor), 0f);
        Invoke(nameof(RevertColor), 0.4f);
// -------------------------------------------------------------------------------------------------------------------- (2)
        Invoke(nameof(InflictHitColor), 0.6f);
        Invoke(nameof(RevertColor), 1f);
        // Mace / Melee class
        
        // creature death 
        // TODO if health <= 0, this dies, set to death animation
        if (_health <= 0)
        {
            // We will change our variables name after Milestone Coding 1
            animator.SetBool("isDead", true);
            Invoke("GetRidOfSelf", 1f);
        }
    }

    // Called once the frame starts.
    // Might change this with Awake() instead.
    void Start()
    {
        _rend = GetComponent<SpriteRenderer>();
        _health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Skips the logic of the creature to prevent
        // further issues
        if (_player.IsUnityNull())
            return;
        
        // calculates the direction
        Vector3 curr = transform.position;
        Vector3 playerPosition = _player.transform.position;
        _distance = Vector2.Distance(curr, playerPosition);
        _direction = playerPosition - curr;
        _direction.Normalize();

        animator.SetFloat("targetDistance", _distance);
        // moves the creature once its current distance is within
        // the range of the given maximum distance.
        if (_distance < maximumDistance)
        {
            animator.SetFloat("speed", moveSpeed);
            // move the enemy
            transform.position = Vector2.MoveTowards(curr, playerPosition, moveSpeed * Time.deltaTime);
        }
        else
            animator.SetFloat("speed", -1);
    }

    // Is called uncommonly in a series of frames. (Not to be confused with Update, that runs once per frame)
    void FixedUpdate()
    {
// -------------------------------------------------------------------------------------------------------------------- (3)
        // Change the direction ( Flip the sprite )
        if (_direction.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (_direction.x > 0)
            transform.localScale = new Vector3(1, 1, 1);

/* TODO (Keep, this partial code might be a future use)
    if (distance < 2)
        {
            Player.GetInstance().InflictDamage(Damage);
            // please add a cooldown. Enemies are too overpowered.
        }        
*/
    }

    // Ensures knock-back logic from the player.
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        if (_isInflicted)
        {
            Vector3 currPos = transform.position;
            Vector2 diff = currPos - other.transform.position;
            transform.position = new Vector2(currPos.x + diff.x, currPos.y + diff.y);
            _isInflicted = false;
        }
            
    }
}
