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


}
