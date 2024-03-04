using System;
using System.Collections;
using UnityEngine;

public class Creature : MonoBehaviour
{
    [Header ("Stats")]
    private string Name;
    public int Level;
    public int MaxHealth;
    protected int Health;
    public int Defense;
    public int Damage;

    [Header("Creature Configuration")] 
    [SerializeField]
    private float maximumDistance;
    private GameObject player;
    public float moveSpeed;
    public Animator animator;

    private float distance;

    public Color damageColor;
    private Vector2 direction;
    private SpriteRenderer rend;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void InflictHitColor()
    {
        rend.color = Color.red;
    }
    void RevertColor()
    {
        rend.color = Color.white;
        
    }
    void GetRidOfSelf()
    {
        // https://forum.unity.com/threads/how-would-i-destroy-self-after-collision.353037/
        Debug.Log("dead");
        Destroy(gameObject);
    }

    public void InflictDamage(int damage)
    {

        Health -= (damage - Defense);
        // Mace / Melee class
        
        Invoke("InflictHitColor", 0f);
        Invoke("RevertColor", 0.4f);

        // https://forum.unity.com/threads/changing-colors-every-couple-of-seconds.179708/
        Invoke("InflictHitColor", 0.6f);
        Invoke("RevertColor", 1f);


        // creature death 
        if (Health <= 0)
        {
            animator.SetBool("isDead", true);
            Invoke("GetRidOfSelf", 1f);
        }
        
        // if health <= 0, this dies, set to death animation
    }


    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        Health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            return;
        distance = Vector2.Distance(transform.position, player.transform.position);
        direction = player.transform.position - transform.position;
        direction.Normalize();

        animator.SetFloat("targetDistance", distance);
        if (distance < maximumDistance)
        {
            animator.SetFloat("speed", moveSpeed);
            // move the enemy
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetFloat("speed", -1);
        }
    }

    void FixedUpdate()
    {
        // https://www.youtube.com/watch?v=RuvfOl8HhhM
        if (direction.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (direction.x > 0)
            transform.localScale = new Vector3(1, 1, 1);

/*        if (distance < 2)
        {
            Player.GetInstance().InflictDamage(Damage);
            // please add a cooldown. Enemies are too overpowered.
        }*/
    }


}
