using System.Collections;
using UnityEngine;

/// <summary>
/// God Function. Controls the Player's MonoBehaviour.
/// 
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 12 2024
/// Source: Applied C# Skills
/// </summary>
public partial class PlayerController : MonoBehaviour
{
    [Header("Player Data")] 
    // Rigid Body of the player.
    private Rigidbody2D _rigid;

    // Animator of the player.
    private Animator _animator;

    // Used to identify direction.
    private Vector2 _directedMovement;

    // The Boundaries or the Ability hit box.
    private GameObject _nonProjectileHitBox;

    // Sounds that need to be played for performed actions.
    private AudioSource[] _sounds;

    // Init Call. Set the variables.
    private void Init()
    {
        _rigid = gameObject.GetComponent<Rigidbody2D>();
        _animator = gameObject.GetComponent<Animator>();
        
        _nonProjectileHitBox = transform.GetChild(0).gameObject;
        _projectile = transform.GetChild(1).gameObject;

        _originalProjectileScale = _projectile.transform.localScale;
        _sounds = GetComponents<AudioSource>();
    }
    
    // Called when object is instantiated.
    private void Awake()
    {
        _creationDelay = new WaitForSeconds(timeBetweenProjectiles);
        Init();
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        Player.Animator = _animator;
        if (Player.ChosenClass.Name.Equals("Melee"))
            _meleeWaitForSeconds = new WaitForSeconds(1);
    }

    // Update is called once per frame
    private void Update()
    {
        GetAxisPositionValues();
        HandleAttacks();
    }

    private void HandleAttacks()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!_hasUltimateActive)
                EnablePrimary();
        }
        else if (Input.GetMouseButtonUp(0))
            DisablePrimary();

        if (!Input.GetMouseButtonDown(1)) return;
        if (_isCooldownDone)
            EnableUltimate();
    }

    // Runs in 50 Frames?
    private void FixedUpdate()
    {
        FlipSelf();
        MovePlayer();
    }

    /// <summary>
    /// Inflict damage to the player.
    /// If player has entered Immunity Frames, return, otherwise continue.
    /// If player has received lucky dice, they can dodge a hit. Otherwise take the hit.
    /// </summary>
    public void InflictDamage()
    {
        if (Player.HasEnteredImmunityFramesRegion)
            return;
        
        var diceValue = Player.GetLuckyDiceRoll;
        switch (diceValue)
        {
            case 1: // player does not get hit but keeps the immunity frame
            {
                _sounds[1].Play();
                break;
            }
                
            default: // lose and player does not own Lucky Dice yet
                
                _sounds[0].Play();
                _animator.SetTrigger(Player.DamagePlayer());
                break;
        }
        
        StartCoroutine(ImmunityFramesElapsed());
    }

    // Make Player Enter Immunity Frames Region.
    private static IEnumerator ImmunityFramesElapsed()
    {
        Player.HasEnteredImmunityFramesRegion = true;
        yield return new WaitForSeconds(Player.GetImmunityFrameTimer);
        Player.HasEnteredImmunityFramesRegion = false;
    }
}
