using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

// make this into controller
public class PlayerShoot : MonoBehaviour
{

    // https://www.youtube.com/watch?v=mgjWA2mxLfI
    public Camera sceneCamera;
    public Animator animator;

    private bool isAttacking;
    
    private GameObject[] enemies;
    // https://www.youtube.com/watch?v=77dWGDFqcps

    void Awake()
    {
        Player.GetInstance();
        // https://stackoverflow.com/questions/49945699/unity-change-all-objects-with-a-certain-tag#:~:text=If%20you%20want%20to%20find,need%20to%20on%20each%20object.
        // https://learn.unity.com/tutorial/getcomponent#5c8a65d5edbc2a001f47d6e6
        /*enemies = GameObject.FindGameObjectsWithTag("Enemy");*/


    }
    void Start()
    {
        

    }
    // Update is called once per frame
    void Update()
    {

        // https://www.youtube.com/watch?v=mgjWA2mxLfI
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Primary");
            Invoke("SetAttackTrue", 0.3f);
            Invoke("SetAttackFalse", 0.5f);
            /*PerformAttack();*/
        }
        else if (Input.GetMouseButtonDown(1)) // first frame prevents multiple loops
        {
            animator.SetTrigger("Ultimate");
            Invoke("SetAttackTrue", 0.5f);
            Invoke("SetAttackFalse", 0.9f);
        }
            
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
    void FixedUpdate()
    {
        if (animator.GetInteger("HP") < 1)
            Invoke("DestroySelf", 3f);           
    }

    private void SetAttackTrue()
    {
        isAttacking = true;
    }

    private void SetAttackFalse()
    {
        isAttacking = false;
    }


    // https://www.youtube.com/watch?v=ND1orPLw5EQ
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Enemy")
            return;
        if (isAttacking)
        {
            Creature c = collision.gameObject.GetComponent<Creature>();
            c.InflictDamage(Player.GetInstance().Damage);
        }
    }

    /*private void PerformAttack()
    {
        animator.SetTrigger("Primary");
        
        
            foreach (GameObject c in enemies)
            {
            Creature creature = c.GetComponent<Creature>();
                float distance = Vector2.Distance(transform.position, c.transform.position);
                if (distance < 2)
                {
                    creature.InflictDamage(Player.GetInstance().Damage);
                }
            }
        
    }*/
    
}
