using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGateController : MonoBehaviour
{
    private AudioSource sfx;
    // Start is called before the first frame update
    void Start()
    {
        Room.Instance.EnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        sfx = transform.parent.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Room.Instance.EnemyCount <= 0)
        {
            sfx.Play();
            Destroy(gameObject);
        }


            
    }
}
