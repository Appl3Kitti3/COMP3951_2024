
/// <summary>
///     Ability represents the stationary hit box for a sprite.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 13 2024
/// Source: Applied C# and Unity Skills
/// </summary>
public class Ability : Attack
{
    protected override void Init()
    {
        ParentObject = transform.parent.gameObject;
    }
}

