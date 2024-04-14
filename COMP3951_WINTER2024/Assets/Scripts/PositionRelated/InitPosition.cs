using UnityEngine;

/// <summary>
///     It is the invisible square that belongs in every room and scene. Determines the placement of the player's spawn.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 13 2024
/// Source: Applied C# and Unity Skills
/// </summary>
public class InitPosition : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        GameObject.FindWithTag("Player").transform.position = transform.position;
    }
}
