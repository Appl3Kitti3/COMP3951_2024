using UnityEngine;

/// <summary>
///     Used to open the gate once Room's enemy count is at 0.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 13 2024
/// Source: Applied C# and Unity Skills
/// </summary>
public class RoomGateController : MonoBehaviour
{
    // Audio that is played once the Room enemy count is empty.
    private AudioSource _sfx;
    
    // Start is called before the first frame update
    private void Start()
    {
        Room.Instance.EnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        _sfx = transform.parent.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Room.Instance.EnemyCount > 0) return;
        _sfx.Play();
        // Destroy the lock.
        Destroy(gameObject);
        
    }
}
