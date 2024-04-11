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

    public static int Health {
        get => _health;
        private set {_health = value;}
    }

    private int _maxHealth;

    // Animator object of the player gameObject that represents its animation sprite.
    private Animator _animator;
    
    // Get animator
    public static Animator Animator
    {
        get { return _instance._animator; }
        set { _instance._animator = value;  }
    }
    
    // Singleton object of the Player class.
    private static Player _instance;
    
    // Checks if an enemy has entered the immunity frame region.
    public static bool HasEnteredImmunityFramesRegion { get; set; } = false;
    
    private Playable _playable;

    public static Playable ChosenClass {get => _instance._playable; private set {
        _instance.Health = _instance._maxHealth = value.BaseHealth;
        _instance._playable = value;
        }}

    private int _highscore;
    
    public static int HighScore
    {
        get => _highscore;
        private set 
        {
            if (IsNewHighScore(value)) 
                _highscore = value;
        }
    }

    public static int Level { get; set; } = 1;

    public static int Score
    {
        get;
        set;
    }

    public static float GetImmunityFrameTimer
    {
        get
        {
            if (_instance._abilities[0])
                return 2f;
            return 1f;
        }
    }

    public static int GetLuckyDiceRoll
    {
        get
        {
            if (_instance._abilities[1])
                return Random.Range(0, 2);
            return -1;
        }
    }

    public static float GetProjectileScale
    {
        get
        {
            if (_instance._abilities[2])
                return 1.5f;
            return 1f;
        }
    }

    private readonly bool[] _abilities = new[] { false, false, false };
    
    private PlayerData _playerData;

    /// <summary>
    /// Create the player class.
    /// </summary>
    private Player() {}

    public static void FixFields(Playable c, Animator a)
    {
        ChosenClass = c;
        Animator = a;
    }

    public static void Initialize() {
        _instance = new Player();
    }

    public void IncrementHealth()
    {
        _instance.Health++;
        if (_health > _maxHealth)
            _health = _maxHealth;
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
