using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Source: https://www.youtube.com/watch?v=u8tot-X_RBI --> Player Movement
/// https://www.youtube.com/watch?v=whzomFgjT50
/// </summary>
public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;

    private Vector2 movement;

    [Header("Health")]
    [SerializeField] private int HP;

    [Header("Damage")]
    [SerializeField] private int DMG;
    void Awake()
    {
        Player.GetInstance(HP, DMG, animator);
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    // Physics calculations

    void FixedUpdate()
    {

        //https://www.youtube.com/watch?v=Cr-j7EoM8bg&t=140s
        if (movement.x < 0)
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        else if (movement.x > 0)
            gameObject.transform.localScale = new Vector3(1, 1, 1);

        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // https://www.youtube.com/watch?v=YSzmCf_L2cE
        if (collision.gameObject.tag != "Enemy") return;
        Creature c = collision.gameObject.GetComponent<Creature>();
        StartCoroutine(ImmunityFrames());
        Player.GetInstance().InflictDamage(c.Damage, c.animator);
        
    }

    private IEnumerator ImmunityFrames()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        yield return new WaitForSeconds(2f);
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }
}
