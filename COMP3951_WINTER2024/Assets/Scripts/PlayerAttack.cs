using UnityEngine;

// TODO make this into controller


/// <summary>
/// Description:
///     Player class is a singleton that represents the current stats of the player. (Damage, Health.)
///         It will have a weapon class to represent its weapon and its playable class to dynamically link
///         and switch to what class should be playing.
/// Author: Teddy Dumam-Ag A01329707
/// Date: March 6 2024 (Created around February)
/// Sources: Applied C# and OOP skills.
///
///     Tutorial on how to make player shoot or attack.
///     https://www.youtube.com/watch?v=mgjWA2mxLfI
///
///     State process of the animation tree in Unity.
///     https://www.youtube.com/watch?v=77dWGDFqcps
///
///     Detecting enemy or other game sprite collisions.
///     https://www.youtube.com/watch?v=ND1orPLw5EQ
///
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    // The attack box that's in front of the player.
    public GameObject attackBox;
    
    // Called once the controller is instantiated.
    private void Start()
    {
        attackBox = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // Left Button
        if (Input.GetMouseButtonDown(0))
        {
            Player.GetInstance().Animator.SetBool("Primary", true);
            attackBox.SetActive(true);
            
        }
        // Right Button
        else if (Input.GetMouseButtonDown(1))
        {
            Player.GetInstance().Animator.SetBool("Ultimate", true);
            // TODO: ultimateBox.SetActive(true)
        }

        if (Input.GetMouseButtonUp(0))
        {
            Player.GetInstance().Animator.SetBool("Primary", false);
            attackBox.SetActive(false);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            Player.GetInstance().Animator.SetBool("Ultimate", false);   
            // TODO: ultimateBox.SetActive(false)
        }
            
    }

    // Destroys the object by itself.
    void DestroySelf()
    {
        Destroy(gameObject);
    }
    
    // Update once or several times in a sequence of frames. (Not updated every frame)
    void FixedUpdate()
    {
        // Once the health points is less than 1, kill the player. Set the death animation.
        if (Player.GetInstance().Animator.GetInteger("HP") < 1)
            Invoke(nameof(DestroySelf), 3f);        
    }
}
