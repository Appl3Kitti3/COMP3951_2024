public class SfxSlider : SoundSliders
{
    protected override float GetValue()
    {
        return Player.GetSfxVolume();
    }
}
