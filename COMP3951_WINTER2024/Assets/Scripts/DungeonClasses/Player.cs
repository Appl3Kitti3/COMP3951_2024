using System;
using System.IO;
using EndlessCatacombs;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Description:
///     Player class is a singleton that represents the current stats of the player. (Damage, Health.)
///         It will have a weapon class to represent its weapon and its playable class to dynamically link
///         and switch to what class should be playing.
/// Author: Teddy Dumam-Ag A01329707
/// Date: March 6 2024 (Created around February)
/// Sources: Applied C# and OOP skills.
/// </summary>
public class Player
{
    // Health of the player.
    private int _health;

    private int maxHealth;
    public int Health => _health;
    // Animator object of the player gameObject that represents its animation sprite.
    private Animator _animator;
    
    // Get animator
    public Animator Animator
    {
        get { return _animator; }
        set { _animator = value;  }
    }
    
    // Singleton object of the Player class.
    private static Player _instance;
    
    // Checks if an enemy has entered the immunity frame region.
    public bool HasEnteredImmunityFramesRegion { get; set; } = false;
    
    public Playable ChosenClass { get; set; }

    private int _highscore;
    
    public int HighScore
    {
        get => _highscore;
        set 
        {
            if (IsNewHighScore(value)) 
                _highscore = value;
        }
    }

    public int Level { get; set; } = 1;

    public int Score
    {
        get;
        set;
    }

    public float GetImmunityFrameTimer
    {
        get
        {
            if (abilities[0])
                return 2f;
            return 1f;
        }
    }

    public int GetLuckyDiceRoll
    {
        get
        {
            if (abilities[1])
                return Random.Range(0, 2);
            return -1;
        }
    }

    public float GetProjectileScale
    {
        get
        {
            if (abilities[2])
                return 1.5f;
            return 1f;
        }
    }

    private bool[] abilities = new[] { false, false, false };
    
    private PlayerData _playerData;
    /// <summary>
    /// Create the player class.
    /// </summary>
    /// <param name="hp">Health points.</param>
    /// <param name="dmg">Base damage of the player.</param>
    /// <param name="animator">Animation of the gameObject.</param>
    private Player(int hp = 0, Animator animator = null) {
        _health = maxHealth = hp;
        /*Damage = dmg;*/
        _animator = animator;
        
        string line;
        try
        {
            // https://learn.microsoft.com/en-us/troubleshoot/developer/visualstudio/csharp/language-compilers/read-write-text-file#write-a-text-file-example-1
            // https://docs.unity3d.com/Manual/JSONSerialization.html
            // https://docs.unity3d.com/ScriptReference/JsonUtility.FromJson.html
            // https://learn.microsoft.com/en-us/dotnet/api/system.io.directory.createdirectory?view=net-8.0#system-io-directory-createdirectory(system-string)
            using StreamReader sr = new StreamReader(Constants.Path);
            line = sr.ReadToEnd();
            _playerData = JsonUtility.FromJson<PlayerData>(line);
            _highscore = _playerData.Highscore;
        }
        catch (Exception x)
        {
            Directory.CreateDirectory(".\\Data");
            _playerData = new PlayerData();
            SaveGameToJson();
        }
        


    }

    // Get the only singleton instance of the player.
    public static Player GetInstance()
    {
        if (_instance.IsUnityNull()) 
            _instance = new Player();
        return _instance;
    }

    public static Player FixFields(int maxhp, Animator animator)
    {
        _instance._health = _instance.maxHealth = maxhp;
        _instance._animator = animator;
        return _instance;
    }


    public void IncrementHealth()
    {
        _health++;
        if (_health > maxHealth)
            _health = maxHealth;
    }
    public static void Reset()
    {
        _instance.HighScore = _instance.Score;
        _instance._playerData.Highscore = _instance.HighScore;
        _instance.SaveGameToJson();
        
        _instance = null;
    }
    public void DamagePlayer(int dmg)
    {
        Debug.Log(_health);
        _health -= dmg;
        if (_health <= 0)
            _animator.SetTrigger("Killed");
    }

    public void SaveGameToJson()
    {

        string json = JsonUtility.ToJson(_playerData);
        
        Debug.Log($"{json}");
        using StreamWriter sw = new StreamWriter(Constants.Path);
        sw.WriteLine(json);
            /*sw.Close();*/
        

    }


    public bool IsNewHighScore(int score)
    {
        return score >= _highscore;
    }

    public void ModifyFlag(string dataName, bool status)
    {
        switch (dataName)
        {
            case "IFrames":
            {
                abilities[0] = status;
                break;
            }
            case "LDice":
            {
                abilities[1] = status;
                break;
            }
            case "BProjectile":
            {
                abilities[2] = status;
                break;
            }
        }
    }

    public bool GetFlag(string dataName)
    {
        switch (dataName)
        {
            case "IFrames": return abilities[0];
            case "LDice": return abilities[1];
            case "BProjectile": return abilities[2];
            default: return false;
        }
    }

    public float GetBGMusicVolume()
    {
        return _playerData.BGMusicVolume;
    }

    public float GetSFXVolume()
    {
        return _playerData.SFXVolume;
    }


    public void SetVolume(string type, float value)
    {
        switch (type)
        {
            case "Music":
            {
                _playerData.BGMusicVolume = value;
                break;
            }
            case "SFX":
            {
                _playerData.SFXVolume = value;
                break;
            }
        }
    }
}
