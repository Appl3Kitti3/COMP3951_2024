/// <summary>
///     Mace is used for the Melee. Has its own base damage.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 13 2024
/// Source: Applied C# and Unity Skills
/// </summary>
public class Mace : Weapon
{
    // Construct a Mace.
    public Mace()
    {
        BaseDamage = Constants.BaseMace;
    }

    protected override int CalculateDamage()
    {
        var actualDamage = BaseDamage + Constants.MaceBonus;
        
        // Critical Hit
        if (IsLandedOnCriticalHit())
            return actualDamage + CriticalDamage + Constants.BerserkerCritical;
        return actualDamage;
    }

    protected override int UltimateAbility()
    {
        double actualDamage = BaseDamage + BaseDamage + Constants.MaceBonus;

        if (IsLandedOnCriticalHit())
            return (int) actualDamage + (CriticalDamage / 2) + Constants.BerserkerCritical;
        return (int) actualDamage;
        
    }
}
