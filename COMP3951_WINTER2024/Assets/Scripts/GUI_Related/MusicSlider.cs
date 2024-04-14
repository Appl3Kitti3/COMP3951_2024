/// <summary>
///     Derived Class from Sound Slider. Gets the background music volume.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 12 2024
/// Source: Applied C# and Unity Skills
/// </summary>
public class MusicSlider : SoundSliders
{
    protected override float GetValue()
    {
        return Player.GetBgMusicVolume();
    }
}
