/// <summary>
///     Necro's ability rejuvenates 15% of its max health.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 13 2024
/// Source: Applied C# and Unity Skills
/// </summary>
public class RegenerationAbility : PassiveAbility
{
    protected override void PerformAbility()
    {
        if (BossCreature.Health == BossCreature.MaxHealth)
            return;
        BossCreature.Health += (int) (BossCreature.MaxHealth * (3d / 20));
    }
}
