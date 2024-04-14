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
    public int Health {get; set;}

    public static int GetHealth => _instance.Health;

    private int _maxHealth;

    public static int GetMaxHealth => _instance._maxHealth;

    // Animator object of the player gameObject that represents its animation sprite.
    private Animator _animator;
    
    // Get animator
    public static Animator Animator
    {
        get => _instance._animator;
        set => _instance._animator = value;
    }
    
    // Singleton object of the Player class.
    private static Player _instance;
    
    // Checks if an enemy has entered the immunity frame region.
    private bool hasEnteredIF;
    public static bool HasEnteredImmunityFramesRegion
    {
        get => _instance.hasEnteredIF;
        set => _instance.hasEnteredIF = value;
    }

    private Playable _playable;
    public static Playable ChosenClass
    {
        get => _instance._playable;
        private set
        {
            _instance.Health = _instance._maxHealth = value.BaseHealth;
            _instance._playable = value;
        }
    }

    public static int GetSpeed => _instance._playable.GetSpeed();
    
    public static int GetUltSpeed => _instance._playable.GetUltimateSpeed();
    
    private int _highScore;
    
    public static int HighScore => _instance._highScore;

    public static int Level { get; set; } = 1;

    private int _score;
    public static int Score
    {
        get => _instance._score;
        set => _instance._score = value;
    }

    public static float GetImmunityFrameTimer => _instance._abilities[0] ? 2f : 1f;

    public static int GetLuckyDiceRoll
    {
        get
        {
            if (_instance._abilities[1])
                return Random.Range(0, 2);
            return -1;
        }
    }

    public static float GetProjectileScale => _instance._abilities[2] ? 1.5f : 1f;

    private readonly bool[] _abilities = { false, false, false };
    
    private PlayerData _playerData;
    
    private Player() {}

    // Get the only singleton instance of the player.
    public static void FixFields(Playable c, Animator a)
    {
        ChosenClass = c;
        Animator = a;
    }

    public static void Initialize()
    {
        _instance = new Player();
        Level = 1;
    }
    
    public static void IncrementHealth()
    {
        _instance.Health++;
        if (GetHealth > GetMaxHealth)
            _instance.Health = GetMaxHealth;
    }

    private void SaveHighScoreData()
    {
        _playerData.highScore = _highScore;
    }
    public static void Reset()
    {
        SaveGameToJson();
        
        _instance = null;
        Initialize();
    }
    public static string DamagePlayer()
    {
        _instance.Health--;
        return GetHealth <= 0 ? "Killed" : "Hit";
    }

    public static void SaveGameToJson()
    {
        if (IsNewHighScore())
            _instance._highScore = Score;
        
        _instance.SaveHighScoreData();
        
        var json = JsonUtility.ToJson(_instance._playerData);
        
        using StreamWriter sw = new StreamWriter(Constants.Path);
        sw.WriteLine(json);
    }

    private void ObtainData()
    {
        try
        {
            // https://learn.microsoft.com/en-us/troubleshoot/developer/visualstudio/csharp/language-compilers/read-write-text-file#write-a-text-file-example-1
            // https://docs.unity3d.com/Manual/JSONSerialization.html
            // https://docs.unity3d.com/ScriptReference/JsonUtility.FromJson.html
            // https://learn.microsoft.com/en-us/dotnet/api/system.io.directory.createdirectory?view=net-8.0#system-io-directory-createdirectory(system-string)
            using StreamReader sr = new StreamReader(Constants.Path);
            var line = sr.ReadToEnd();
            _playerData = JsonUtility.FromJson<PlayerData>(line);
            _highScore = _playerData.highScore;
        }
        catch (Exception ex)
        {
            if (ex is FileNotFoundException or DirectoryNotFoundException)
            {
                Directory.CreateDirectory(".\\Data");
                _playerData = new PlayerData();
                SaveGameToJson();    
            }
            
        }
    }

    public static bool IsNewHighScore()
    {
        return _instance._score >= _instance._highScore;
    }

    public static void ModifyFlag(string dataName, bool status)
    {
        switch (dataName)
        {
            case "IFrames":
            {
                _instance._abilities[0] = status;
                break;
            }
            case "LDice":
            {
                _instance._abilities[1] = status;
                break;
            }
            case "BProjectile":
            {
                _instance._abilities[2] = status;
                break;
            }
        }
    }

    public static bool GetFlag(string dataName)
    {
        switch (dataName)
        {
            case "IFrames": return _instance._abilities[0];
            case "LDice": return _instance._abilities[1];
            case "BProjectile": return _instance._abilities[2];
            default: return false;
        }
    }

    public static float GetBgMusicVolume()
    {
        if (_instance.IsUnityNull())
            Initialize();
        if (_instance._playerData.IsUnityNull())
            _instance.ObtainData();
        return _instance._playerData.bgMusicVolume;
    }

    public static float GetSfxVolume()
    {
        if (_instance._playerData.IsUnityNull())
            _instance.ObtainData();
        return _instance._playerData.sfxVolume;
    }

    public static void SetVolume(string type, float value)
    {
        switch (type)
        {
            case "Music":
            {
                _instance._playerData.bgMusicVolume = value;
                break;
            }
            case "SFX":
            {
                _instance._playerData.sfxVolume = value;
                break;
            }
        }
    }
}
