using System.Collections;
using UnityEngine;

/// <summary>
/// Description:
///     Player class is a singleton that represents the current stats of the player. (Damage, Health.)
///         It will have a weapon class to represent its weapon and its playable class to dynamically link
///         and switch to what class should be playing.
/// Author: Teddy Dumam-Ag A01329707
/// Date: March 6 2024 (Created around mid February)
/// Sources:
///
///     Tutorial on 2D Top Down movement.
///     https://www.youtube.com/watch?v=u8tot-X_RBI
///
///     Tutorial on 2D Top Down movement alternative.
///     https://www.youtube.com/watch?v=whzomFgjT50
///
///     Tutorial on how to flip a sprite. (Line 70)
///     https://www.youtube.com/watch?v=Cr-j7EoM8bg&t=140s
///
///     Tutorial on collisions
///     https://www.youtube.com/watch?v=YSzmCf_L2cE
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [Header("Base Stats")]
    
    // Health points, the amount of base health points the player has.
    // Used to instantiate the singleton. (Might be migrated into a different class)
    [SerializeField] private int healthPoints;
    
    // Base damage of the player.
    // Used to instantiate the singleton. (Might be migrated into a different class)
    [SerializeField] private int baseDamage;
    
    [Header ("Controller Configuration")]
    
    // Move speed of the player (Used as the movement speed when the player walks)
    [SerializeField] private float moveSpeed = 5f; // TODO: Idea, on shift hold, increase speed to 10f.
    
    // RigidBody component of the player game object.
    private Rigidbody2D _rigid;
    
    // Animator component of the player game object.
    private Animator _animator;

    // The current position of the player (ish).
    private Vector2 _movement;
    
    // Once player game object has been instantiated.
    void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
        _rigid = gameObject.GetComponent<Rigidbody2D>();
        Player.GetInstance(healthPoints, baseDamage, _animator);
    }

    // Update is called once per frame
    void Update()
    {
        // Move logic of the player.
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");

        _animator.SetFloat("Speed", _movement.sqrMagnitude);
    }

    // Is called uncommonly in a series of frames. (Not to be confused with Update, that runs once per frame)
    void FixedUpdate()
    {
        // Flip image
        if (_movement.x < 0)
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        else if (_movement.x > 0)
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        
        // Player movement
        _rigid.MovePosition(_rigid.position + _movement * moveSpeed * Time.deltaTime);
    }

    // Called once an enemy collides with the player.
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy")) return;
        Creature c = collision.gameObject.GetComponent<Creature>();
        StartCoroutine(ImmunityFrames());
        Player.GetInstance().InflictDamage(c.damage, c.animator);
        
    }

    // Allows for immunity frames. A time traversal is set.
    private IEnumerator ImmunityFrames()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        yield return new WaitForSeconds(2f);
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }
}
