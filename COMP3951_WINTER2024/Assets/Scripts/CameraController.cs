using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// The Camera Controller is used to follow the player's
/// object. Dynamically moves with the player.
/// 
/// Author: Teddy Dumam-Ag A01329707
/// Date: March 5 2024 (Created around February)
/// Source:
///
///     Camera Follow Player
///     https://www.youtube.com/watch?v=GOQV688wbU0
///
///     Using mouse position
///     https://www.youtube.com/watch?v=0jTPKz3ga4w
/// </summary>
public class CameraController : MonoBehaviour
{
    // The GameObject seen as a transform object that in which the camera
    // will follow.
    // It is a transform so that it can get its current position.
    // In this case it is passed as the player Game Object's transform.
    public Transform target;

    // Crosshair of the game
    [SerializeField] private GameObject crossHair;
    
    private Camera _camera;

    // Update is called once per frame
    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    void Update()
    {
        // Gets a crosshair and it follows based on the mouse position
        var currMousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        currMousePos.z = 0f;
        crossHair.transform.position = currMousePos;
        
        // Moves the camera to the player mosition.
        Transform curr = transform;
        if (target.IsUnityNull()) return;
        var temp = target.transform.position;
        curr.position = new Vector3(temp.x, temp.y, curr.position.z);
    }   
}
