/// <summary>
///     Bow is used for the Archer, gets the base damage on the constants.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 13 2024
/// Source: Applied C# and Unity Skills
/// </summary>
public class Bow : Weapon
{
    // Construct a Bow.
    public Bow()
    {
        BaseDamage = Constants.BaseBow;
    }

    protected override int CalculateDamage()
    {
        // dmg / 2
        var actualDamage = BaseDamage / 2;
        
        // Critical Hit
        if (IsLandedOnCriticalHit())
            return actualDamage + CriticalDamage + Constants.ArcherCritical;
        return actualDamage;

    }

    protected override int UltimateAbility()
    {
        var actualDamage = BaseDamage + (BaseDamage * (1 / (double)Constants.BowMultiplier));

        if (IsLandedOnCriticalHit())
            return (int) actualDamage + (CriticalDamage / 2) + Constants.ArcherCritical;
        return (int) actualDamage;
    }
}
