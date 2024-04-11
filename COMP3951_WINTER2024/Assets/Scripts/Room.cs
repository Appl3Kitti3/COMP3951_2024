using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Room
{
    public int EnemyCount
    {
        get;
        set;
    }

    private static Room _instance;

    private Room()
    {}

    
    // https://stackoverflow.com/questions/2404815/singleton-properties
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
