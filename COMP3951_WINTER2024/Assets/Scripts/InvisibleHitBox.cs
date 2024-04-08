using System.Threading;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

/// <summary>
/// Description:
///     Health system, a GUI shows the amount of hearts left for the player. 
/// Author: Tedrik "Teddy" Dumam-Ag
/// Date: March 27 2024
/// Source: https://blog.logrocket.com/performance-unity-async-await-tasks-coroutines-c-job-system-burst-compiler/
///
/// </summary>
public class InvisibleHitBox : MonoBehaviour
{
    // The x determinant to move the hitbox in front of their gameObject
    public float xTranslate;
    
    // The y determinant to move the hitbox in front of their gameObject
    public float yTranslate;

    // The game object used to place the attack box in front of.
    public GameObject currentObject;
    
    // Start is called before the first frame update
    void Start()
    {
        currentObject = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentObject.CompareTag("Player"))
            HandlePlayerMovementTrigger();
        else if (currentObject.CompareTag("Enemy"))
            HandleCreatureMovementTrigger();
        
    }

    // Handle for player attack box.
    void HandlePlayerMovementTrigger()
    {
        if (xTranslate <= -1 && yTranslate <= -1)
        {
            var mousePos = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            transform.position = mousePos;
        } else
        {
            float x = Input.GetAxisRaw("Horizontal");
            HandleMovementTrigger(x);            
        }

    }
    
    // Handle for creature attack box.
    void HandleCreatureMovementTrigger()
    {
        Creature c = currentObject.GetComponent<Creature>();
        HandleMovementTrigger(c.GetHorizontalDirection);
    }

    // Flips the attack box.
    void HandleMovementTrigger(float xDirection)
    {
        if (xDirection < 0)
            transform.position = currentObject.transform.position + new Vector3(-xTranslate, -yTranslate);
        else if (xDirection > 0)
            transform.position = currentObject.transform.position + new Vector3(xTranslate, -yTranslate);   
    }
    
    // Called once the object is inside the attack box.
    private void OnTriggerStay2D(Collider2D other)
    {
        // Player Attacs Enemy
        if (other.gameObject.CompareTag("Enemy") && currentObject.CompareTag("Player"))
        {
            Vector3 temp = other.transform.position - currentObject.transform.position;
            other.transform.Translate(temp);

            other.gameObject.GetComponent<Creature>().InflictDamage(Player.GetInstance().Damage);
        }
        else if (other.gameObject.CompareTag("Player") && currentObject.CompareTag("Enemy"))
        {
            Creature c = currentObject.GetComponent<Creature>();
            Player.GetInstance().InflictDamage(c.damage, c.animator);
            Player.GetInstance().EnableITRegions();
            
            Task.Run(() =>
            {
                Thread.Sleep(2000);
                Player.GetInstance().DisableITRegions();
            });
            
        }
    }
}
