using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXSlider : SoundSliders
{
    protected override float GetValue()
    {
        return Player.GetInstance().GetSFXVolume();
    }
}
