/// <summary>
///     Gets the volume of the sound effect.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 12 2024
/// Source: Applied C# and Unity Skills
/// </summary>
public class SfxSlider : SoundSliders
{
    protected override float GetValue()
    {
        return Player.GetSfxVolume();
    }
}
