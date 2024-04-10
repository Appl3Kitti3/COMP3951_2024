using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSlider : SoundSliders
{
    protected override float GetValue()
    {
        return Player.GetInstance().GetBGMusicVolume();
    }
}
