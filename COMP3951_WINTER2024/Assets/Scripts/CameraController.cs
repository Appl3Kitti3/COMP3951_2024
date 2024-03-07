
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// The Camera Controller is used to follow the player's
/// object. Dynamically moves with the player.
/// 
/// Author: Teddy Dumam-Ag A01329707
/// Date: March 5 2024 (Created around February)
/// Source: https://www.youtube.com/watch?v=GOQV688wbU0
/// </summary>
public class CameraController : MonoBehaviour
{
    // The GameObject seen as a transform object that in which the camera
    // will follow.
    // It is a transform so that it can get its current position.
    // In this case it is passed as the player Game Object's transform.
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        
        Transform curr = transform;
        if (!target.IsUnityNull())
        {
            Vector3 temp = target.transform.position;
            curr.position = new Vector3(temp.x, temp.y, curr.position.z);
        }
            
    }
}
