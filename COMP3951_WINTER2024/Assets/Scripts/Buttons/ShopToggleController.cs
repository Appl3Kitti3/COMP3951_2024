using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopToggleController : MonoBehaviour
{

    public void OnAbilityOneToggled()
    {
        Debug.Log("Immunity Frames");
        bool tmp = Player.GetInstance().GetFlag("IFrames");
        Player.GetInstance().ModifyFlag("IFrames", !tmp);
    }
    
    public void OnAbilityTwoToggled()
    {
        Debug.Log("Lucky Dice");
        bool tmp = Player.GetInstance().GetFlag("LDice");
        Player.GetInstance().ModifyFlag("LDice", !tmp);
    }
    
    public void OnAbilityThreeToggled()
    {
        Debug.Log("Big Projectile");
        bool tmp = Player.GetInstance().GetFlag("BProjectile");
        Player.GetInstance().ModifyFlag("BProjectile", !tmp);
    }
}
