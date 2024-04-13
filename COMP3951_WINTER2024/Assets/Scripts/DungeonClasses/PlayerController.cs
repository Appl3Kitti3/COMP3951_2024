using System.Collections;
using UnityEngine;

// TODO: Merges PlayerAttack.cs and PlayerMovement.cs
// No long comments here because it is not finished and ready for use
public partial class PlayerController : MonoBehaviour
{
    [Header("Player Data")] 
    
    // Holds Weapon
    // Weapon holds base damage

    private Rigidbody2D _rigid;

    private Animator _animator;

    private Vector2 _directedMovement;

    private GameObject _nonProjectileHitBox;

    
    void Init()
    {
        _rigid = gameObject.GetComponent<Rigidbody2D>();
        _animator = gameObject.GetComponent<Animator>();
        
        _nonProjectileHitBox = transform.GetChild(0).gameObject;
        _projectile = transform.GetChild(1).gameObject;

        _originalProjectileScale = _projectile.transform.localScale;


    }
    
    private void Awake()
    {
        _creationDelay = new WaitForSeconds(timeBetweenProjectiles);
        Init();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        /*Vector2 playerSpawn = GameObject.FindGameObjectWithTag("InitialPosition").GetComponent<Transform>().position;
        transform.position = playerSpawn;*/
        Player.Animator = _animator;
    }

    // Update is called once per frame
    void Update()
    {
        
        GetAxisPositionValues();
        HandleAttacks();
    }

    void HandleAttacks()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!_hasUltimateActive)
                EnablePrimary();
        }
        else if (Input.GetMouseButtonUp(0))
            DisablePrimary();
        
        if (Input.GetMouseButtonDown(1))
            if (_isCooldownDone)
                EnableUltimate();
        
        // When the player exceeds the ultimate ability duration
        // stop the ability
        // Todo: Text or call noah because the cool down is actually broken
        /*if (_counter >= ultimateCooldown)
            DisableUltimate();*/
        // Todo: Add another coroutine that does the time duration
        // once clicking on right button, start coroutine
        // where sets a boolean so multiple instances wont be created at once
        // then let that coroutine wait for seconds
        // and have another boolean that checks if it is false, then stop the ultimate
    }

    private void FixedUpdate()
    {
        FlipSelf();
        MovePlayer();
    }

    public void InflictDamage(Animator creatureAnimator)
    {
        if (Player.HasEnteredImmunityFramesRegion)
            return;

        creatureAnimator.SetTrigger(Primary); // change name to attack
        int diceValue = Player.GetLuckyDiceRoll;
        switch (diceValue)
        {
            case 1: // player does not get hit but keeps the immunity frame
                break;
            default: // lose and player does not own Lucky Dice yet
                
                gameObject.GetComponent<AudioSource>().Play();
                _animator.SetTrigger(Player.DamagePlayer());
                break;
        }
        
        StartCoroutine(AttackDelay());
    }
    IEnumerator AttackDelay()
    {
        Player.HasEnteredImmunityFramesRegion = true;
        yield return new WaitForSeconds(Player.GetImmunityFrameTimer);
        Player.HasEnteredImmunityFramesRegion = false;
    }
}
