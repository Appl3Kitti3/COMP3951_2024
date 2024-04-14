using System;

/// <summary>
/// Player Data is saved and retrieved by a data.json. It is created in the same directory, creates its own Data directory and places the data.json there.
/// 
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 12 2024
/// Source: Applied C# Skills
/// </summary>
[Serializable]
public class PlayerData
{ 
    // Player's highest score.
    public int highScore;
    
    // Player's selected SFX Volume.
    public float sfxVolume = 1f;
    
    // Player's selected Music volume.
    public float bgMusicVolume = 1f;
}
