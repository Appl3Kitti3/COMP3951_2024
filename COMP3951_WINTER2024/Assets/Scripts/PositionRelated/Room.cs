using Unity.VisualScripting;

/// <summary>
///     A Room is a singleton that tells how many enemies are left in a dungeon room.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 13 2024
/// Source: Applied C# and Unity Skills
///
///     C# Singleton Property
///     https://stackoverflow.com/questions/2404815/singleton-properties
/// </summary>
public class Room
{
    // The amount of enemies left.
    public int EnemyCount
    {
        get;
        set;
    }

    // The private and static instance of the room.
    private static Room _instance;

    // Ensure a private constructor to keep its singleton properties.
    private Room()
    {}
    
    // Get instance of the room.
    public static Room Instance
    {
        get
        {
            if (_instance.IsUnityNull())
                _instance = new Room();
            return _instance;
        }
    }
}
