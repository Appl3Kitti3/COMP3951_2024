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
    
    // Get the damage of the player instance. Currently used as a example prototype
    // however, it will be calculated through the weapon class.
    public int Damage { get; set; }
    
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

    public int Score
    {
        get;
        set;
    }

    public float GetImmunityFrameTimer
    {
        get
        {
            if (_playerData.LongerImmunityFrames)
                return 2f;
            return 1f;
        }
    }

    public int GetLuckyDiceRoll
    {
        get
        {
            if (_playerData.LuckyDice)
                return Random.Range(0, 2);
            return -1;
        }
    }

    public float GetProjectileScale
    {
        get
        {
            if (_playerData.BigProjectiles)
                return 1.5f;
            return 1f;
        }
    }
    private PlayerData _playerData;
    /// <summary>
    /// Create the player class.
    /// </summary>
    /// <param name="hp">Health points.</param>
    /// <param name="dmg">Base damage of the player.</param>
    /// <param name="animator">Animation of the gameObject.</param>
    private Player(int hp, Animator animator) {
        _health = hp;
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
        }
        catch (Exception x)
        {
            Directory.CreateDirectory(".\\Data");
            _playerData = new PlayerData();
            WriteToJson();
        }

        HighScore = 3000;

    }

    // Get the only singleton instance of the player.
    public static Player GetInstance(int maxhp = 9999, Animator animator = null)
    {
        if (_instance.IsUnityNull()) 
            _instance = new Player(maxhp, animator);
        else if (_instance._animator.IsUnityNull())
            _instance._animator = animator;
        return _instance;
    }

    public static void Reset()
    {
        _instance.HighScore = _instance.Score;
        _instance._playerData.Highscore = _instance.HighScore;
        _instance.WriteToJson();
        
        _instance = null;
    }
    public void DamagePlayer(int dmg)
    {
        _health -= dmg;
        if (_health <= 0)
            _animator.SetTrigger("Killed");
    }

    void WriteToJson()
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
                _playerData.LongerImmunityFrames = status;
                break;
            }
            case "LDice":
            {
                _playerData.LuckyDice = status;
                break;
            }
            case "BProjectile":
            {
                _playerData.BigProjectiles = status;
                break;
            }
                default:
                    break;
        }
    }

    public bool GetFlag(string dataName)
    {
        switch (dataName)
        {
            case "IFrames": return _playerData.LongerImmunityFrames;
            case "LDice": return _playerData.LuckyDice;
            case "BProjectile": return _playerData.BigProjectiles;
            default: return false;
        }
    }
}
