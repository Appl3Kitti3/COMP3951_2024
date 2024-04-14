/// <summary>
///     Once Player enters the exit confirmation pit in the lobby.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 12 2024
/// Source: Applied C# and Unity Skills
/// </summary>
public class ExitPosition : PositionOverlayController // for some reason Unity needs a comment here for this to work
{
    protected override string GetSceneName()
    {
        return "Exit Confirmation";
    }
}
