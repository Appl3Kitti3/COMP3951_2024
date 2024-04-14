using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Description:
///     A creature or the enemy of the game. It has its necessary functions to operate a normal dungeon crawler life cycle.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 12 2024
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
///     TryGetComponent
///     file:///C:/Program%20Files/Unity/Hub/Editor/2022.3.19f1/Editor/Data/Documentation/en/ScriptReference/GameObject.TryGetComponent.html
/// </summary>
public partial class Creature : MonoBehaviour
{
    [Header ("Stats")]
    // The max health of the creature. (Assign this to the current health of the player)
    public int maxHealth;

    // Property for maxHealth.
    public int MaxHealth => maxHealth;

    // The current defense of the creature.
    public int def;

    // Value of the enemy once it is defeated.
    [SerializeField] 
    private int points;
    
    // A property for points.
    public int PointValue => points;

    [Header("Creature Configuration")] 
    // The maximum distance a creature can move towards the player before it goes back to idle
    [SerializeField]
    private float maximumDistance;
    
    // The move speed of the creature. (Walking speed)
    [SerializeField]
    private float moveSpeed;

    // Property for moveSpeed.
    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }
    
    // The animator used to animate the sprite.
    public Animator Animator { get; private set; }

    // Used to allow a delay between primary attacks.
    private bool _attackDelayChecker;
    

    // Current health of the creature.
    private int _health;
    
    // Property of _health.
    public int Health
    {
        get => _health;
        // Automatically set it to max health if value exceed.
        set => _health = value >= maxHealth ? maxHealth : value;
    }
    
    // The player game object from the current scene.
    private GameObject _player;
    
    // The direction of the creature facing.
    private Vector2 _direction;

    // Sounds that the creature contains and plays from performed actions.
    private AudioSource[] _sfx;

    // Turns Hit into an integer id.
    private static readonly int Hit = Animator.StringToHash("Hit");

    // Turns Speed into an integer id.
    private static readonly int Speed = Animator.StringToHash("Speed");
    
    // Turns Killed into an integer id.
    private static readonly int Killed = Animator.StringToHash("Killed");
    
    // Turns Primary into an integer id.
    protected static readonly int Primary = Animator.StringToHash("Primary");

    
    // Called once creature is instantiated.
    private void Awake()
    {
        Animator = gameObject.GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player");
        
        var lvl = Player.Level;
        // Logic of their stats. 
        maxHealth *= lvl;
        _health = maxHealth;
        def *= lvl;
        points *= lvl;
        
        TimeDuringAttacks = new WaitForSeconds(attackDelay);
        
        Stationary = transform.GetChild(0).gameObject;
        
        if (transform.childCount > 1)
        {
            Projectile = transform.GetChild(1).gameObject;
            if (Projectile.TryGetComponent(out Projectile p))
                _projectileType = p.ProjectileType;
            StartCoroutine(RunAttacks());
        }

        _sfx = gameObject.GetComponents<AudioSource>();
    }

    // Called on first frame
    private void Start()
    {
        RenderAbilities();   
    }

    // No use in Creature, its the boss' functionality.
    protected virtual void RenderAbilities() {}

    /// <summary>
    /// Inflicts damage to the creature.
    /// </summary>
    /// <param name="dmg">The amount of damaged done by the opponent.</param>
    public void InflictDamage(int dmg)
    {
        Animator.SetTrigger(Hit);
        _sfx[0].Play();
        _health -= (dmg - dmg * (1 / def));
        
    }

    // Update is called once per frame
    private void Update()
    {
        // Make creature stop moving once player is dead.
        if (_player.IsUnityNull())
            Animator.SetFloat(Speed, 0f);
        else
            Move();
        
        // When Death.
        if (_health > 0) return;
        Animator.SetTrigger(Killed);
        Animator.ResetTrigger(Hit);

    }

    // Perform Move.
    private void Move()
    {
        // calculates the direction
        Vector2 curr = transform.position;
        Vector2 playerPosition = _player.transform.position;
        var distance = Vector2.Distance(curr, playerPosition);
        _direction = playerPosition - curr;
        _direction.Normalize();
        
        // moves the creature once its current distance is within
        // the range of the given maximum distance.
        if (distance < maximumDistance && !ShouldStop())
        {
            _isProjectileLoopActive = true;
            Animator.SetFloat(Speed, moveSpeed);
            // move the enemy
            transform.position = Vector2.MoveTowards(curr, playerPosition, Animator.GetFloat(Speed) * Time.deltaTime);
        }
        else
        {
            _isProjectileLoopActive = false;
            Animator.SetFloat(Speed, 0f);
        }
        
        // If the distance is less than 3, attack player.
        if (!(distance < 3)) return;
        if (!_attackDelayChecker)
            StartCoroutine(Attack());

    }

    // Its logic is highly changed in bosses when they are in their ultimate ability state.
    protected virtual bool ShouldStop()
    {
        return false;
    }
    
    // Is called uncommonly in a series of frames. (Not to be confused with Update, that runs once per frame)
    
    // Correction to the above: FixedUpdate() runs at a specific fixed rate regardless of the game's actual framerate.
    // Update() runs once per frame that the game's rate is. For example: FixedUpdate() might run at 60fps, but the game's actual
    // rate is 30fps and that's how often Update() would run. -Noah
    void FixedUpdate()
    {
// -------------------------------------------------------------------------------------------------------------------- (3)
        // Change the direction ( Flip the sprite )
        var localScale = transform.localScale;
        Vector2 scale = localScale;
        // that is a very interesting way of doing switch. Thanks Rider IDE.
        
        localScale = _direction.x switch
        {
            // Flip Scale
            < 0 => Utility.GetInverseScale(scale, -1),
            > 0 => Utility.GetInverseScale(scale, 1),
            _ => localScale
        };
        transform.localScale = localScale;
    }

    // Perform Attack then delay.
    private IEnumerator Attack()
    {
        _attackDelayChecker = true;
        Animator.SetTrigger(Primary);
        yield return new WaitForSeconds(0.5f);
        _attackDelayChecker = false;
    }
}
