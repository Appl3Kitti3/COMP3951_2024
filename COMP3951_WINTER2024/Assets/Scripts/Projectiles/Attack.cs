using Unity.VisualScripting;
using UnityEngine;


/// <summary>
///     Attack represents the child boundaries or projectiles of a sprite.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 13 2024
/// Source: Applied C# and Unity Skills
/// </summary>
public abstract class Attack : MonoBehaviour
{

    // ParentObject property. Used in a lot of logic to check its tag and gets its current position.
    public GameObject ParentObject { get; set; }

    // Allows how far and strong the attack should inflict knock-back.
    [SerializeField] protected float force;
    
    /// <summary>
    /// Initial logic on first frame.
    /// </summary>
    protected abstract void Init();
    
    // Start is called before the first frame update
    void Start()
    {
        Init();   
    }

    /// <summary>
    /// Get the player's attack type.
    /// </summary>
    /// <returns>Returns the integer 0 for primary, 1 for ultimate and -1 for anything else.</returns>
    protected virtual int GetAttackTypeDamage()
    {
        return Player.ChosenClass.Weapon.PerformAttackType();
    }
    
    /// <summary>
    /// Called once a creature / player enters the attack. Also handles the bounds if it impacts the walls.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!ParentObject.IsDestroyed())
            HandleCollision(other);
        CheckColliderBounds(other);
    }

    /// <summary>
    /// Handles on damaging the player or enemy.
    /// </summary>
    /// <param name="other">The object that entered the attack and the target of the parent's object.</param>
    protected virtual void HandleCollision(Collider2D other)
    {
        // Player hits Enemy.
        if (other.gameObject.CompareTag("Enemy") && ParentObject.CompareTag("Player"))
        {
            SetUpForce(other);
            other.gameObject.GetComponent<Creature>().InflictDamage(GetAttackTypeDamage());
            
        }
        
        // Enemy hits player.
        else if (other.gameObject.CompareTag("Player") && ParentObject.CompareTag("Enemy"))
        {
            SetUpForce(other);
            other.GetComponent<PlayerController>().InflictDamage();
            
        }
    }
    
    /// <summary>
    /// Sets up the knock back force of the attack.
    /// </summary>
    /// <param name="other"></param>
    protected virtual void SetUpForce(Collider2D other)
    {
        Vector2 to = other.transform.position;
        var from = GetStartingPosition();
        var direction = (to - from).normalized;
        
        BeforeAddingImplodeForce();   
        other.GetComponent<Rigidbody2D>().AddForce(direction * force, ForceMode2D.Impulse);
    }
    
    /// <summary>
    /// Called only for projectile. It blows up when it impacts a wall or structure.
    /// </summary>
    /// <param name="structure">GameObject that is a wall or structure.</param>
    protected virtual void CheckColliderBounds(Collider2D structure) {}
    
    /// <summary>
    /// Meant for projectile. Blow up the projectile when it is hit.
    /// </summary>
    protected virtual void BeforeAddingImplodeForce() {}
    
    /// <summary>
    /// Get the starting position or current position of its parent.
    /// </summary>
    /// <returns>A vector2 that represents the Parent's position.</returns>
    protected virtual Vector2 GetStartingPosition()
    {
        return ParentObject.transform.position;
    }
    
    
}
