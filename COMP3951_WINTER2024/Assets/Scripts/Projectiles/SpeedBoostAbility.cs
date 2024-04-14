/// <summary>
///     Gives Nightfall a speed boost.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 13 2024
/// Source: Applied C# and Unity Skills
/// </summary>
public class SpeedBoostAbility : PassiveAbility
{
    protected override void PerformAbility()
    {
        BossCreature.MoveSpeed += 1;
    }
}
