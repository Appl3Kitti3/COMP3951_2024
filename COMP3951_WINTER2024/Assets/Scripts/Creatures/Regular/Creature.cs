using System;
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
///
///     Expression Bodied Members
///     https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/expression-bodied-members
/// 
/// </summary>
public partial class Creature : MonoBehaviour /* TODO: Implements IDamagable*/
{
    [Header ("Stats")]
 
    /**
     * TODO: On Creature Load like OnEnable or On Start. Make the level same as the player.
     * From there, manipulate the MaxHealth, Defense, and Damage.
     */
    
    
    // The max health of the creature. (Assign this to the current health of the player)
    [FormerlySerializedAs("MaxHealth")] public int maxHealth;

    public int MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }
    // The current defense of the creature.
    [FormerlySerializedAs("Defense")] public int def;

    // The current damage of the creature.
    // in Future stand points, this will be the bae damage of the creature, and its true damage
    // is calculated based on the level and something else.

    public int Damage { get; set; } = 1;

    [SerializeField] private int points;
    
    public int PointValue
    {
        get => points;
        set => points = value;
    }
    
    [Header("Creature Configuration")] 
    // The maximum distance a creature can move towards the player before it goes back to
    // idle.
    [SerializeField]
    private float maximumDistance;
    
    // The move speed of the creature. (Walking speed)
    [SerializeField]
    private float moveSpeed;

    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }
    // The animator used to animate the sprite.
    public Animator Animator { get; set; }

    // TODO: Keep commented code for now.
    /*private string Name;*/

    private int health;
    // Current health of the creature.
    public int Health
    {
        get => health;
        set
        {
            if (value >= maxHealth)
                health = maxHealth;
            else
                health = value;
        }
    }
    
    // The player game object from the current scene.
    private GameObject _player;
    
    // The direction of the creature facing.
    private Vector2 _direction;

    private AudioSource[] sfx;
    // Called once creature is instantiated.
    private void Awake()
    {
        Animator = gameObject.GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player");
        
        int lvl = Player.GetInstance().Level;
        maxHealth *= lvl;
        health = maxHealth;
        def *= lvl;
        points *= lvl;
        
        TimeDuringAttacks = new WaitForSeconds(attackDelay);
        
        Stationary = transform.GetChild(0).gameObject;
        if (transform.childCount > 1)
        {
            Projectile = transform.GetChild(1).gameObject;
            // file:///C:/Program%20Files/Unity/Hub/Editor/2022.3.19f1/Editor/Data/Documentation/en/ScriptReference/GameObject.TryGetComponent.html
            if (Projectile.TryGetComponent(out Projectile p))
            {
                p.ParentObject = gameObject;
                _projectileClass = new Lazy<Projectile>(p);

            }
            StartCoroutine(RunAttacks());
        }

        sfx = gameObject.GetComponents<AudioSource>();
        Init();
    }

    protected virtual void Init()
    {
    }

    private void Start()
    {
        RenderAbilities();   
    }

    protected virtual void RenderAbilities()
    {
        
    }
    // TODO (Death does not match with animation.)
    // Destroys itself.

    /// <summary>
    /// Inflicts damage to the creature.
    /// </summary>
    /// <param name="dmg">The amount of damaged done by the opponent.</param>
    public void InflictDamage(int dmg)
    {
        Animator.SetTrigger("Hit");
        sfx[0].Play();
        // TODO (Keep this for melee weapons)
        // Move this logic to the Melee class
        health -= (dmg - dmg * (1 / def));
        // Mace / Melee class
        
    }

    // Update is called once per frame
    void Update()
    {

        HandleAudioSFX();
        // Skips the logic of the creature to prevent
        // further issues
        if (_player.IsUnityNull())
            Animator.SetFloat("Speed", 0f);
        else
            Move();
        
        // creature death 
        // TODO if health <= 0, this dies, set to death animation
        if (health <= 0)
        {
            Animator.SetTrigger("Killed");
            
            Animator.ResetTrigger("Hit");
        }
            
    }

    void HandleAudioSFX()
    {
        if (Animator.GetBool("Primary"))
            if (sfx.Length > 1)
                sfx[1].Play();
    }

    void Move()
    {
        // calculates the direction
        // create a class or another collider for range
        Vector2 curr = transform.position;
        Vector2 playerPosition = _player.transform.position;
        float distance = Vector2.Distance(curr, playerPosition);
        _direction = playerPosition - curr;
        _direction.Normalize();
        
        // moves the creature once its current distance is within
        // the range of the given maximum distance.
        if (distance < maximumDistance && !ShouldStop())
        {
            _isProjectileLoopActive = true;
            Animator.SetFloat("Speed", moveSpeed);
            // move the enemy
            transform.position = Vector2.MoveTowards(curr, playerPosition, Animator.GetFloat("Speed") * Time.deltaTime);
        }
        else
        {
            _isProjectileLoopActive = false;
            Animator.SetFloat("Speed", 0f);
        }
    }

    protected virtual bool ShouldStop()
    {
        return false;
    }
    // Is called uncommonly in a series of frames. (Not to be confused with Update, that runs once per frame)
    void FixedUpdate()
    {
// -------------------------------------------------------------------------------------------------------------------- (3)
        // Change the direction ( Flip the sprite )

        Vector2 scal = transform.localScale;
            // Flip image
            if (_direction.x < 0)
                transform.localScale = new Vector2(-Math.Abs(scal.x), scal.y);
            else if (_direction.x > 0)
                transform.localScale = new Vector2(Math.Abs(scal.x), scal.y);

    }
    
}
