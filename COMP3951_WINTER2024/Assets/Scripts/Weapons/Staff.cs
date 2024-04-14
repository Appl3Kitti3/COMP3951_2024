/// <summary>
///     Used for Mage and has its own base damage.
/// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
/// Date: April 13 2024
/// Source: Applied C# and Unity Skills
/// </summary>
public class Staff : Weapon
{
    // Construct a Staff.
    public Staff()
    {
        BaseDamage = Constants.BaseStaff;
    }

    protected override int CalculateDamage()
    {
        var actualDamage = BaseDamage;
        
        // Critical Hit
        if (IsLandedOnCriticalHit())
            return actualDamage + CriticalDamage + Constants.MageCritical;
        return actualDamage;
    }

    protected override int UltimateAbility()
    {
        var actualDamage = (double) BaseDamage / 3 + BaseDamage * Constants.StaffMultiplier;

        if (IsLandedOnCriticalHit())
            return (int) actualDamage + (CriticalDamage / 3) + Constants.MageCritical;
        return (int) actualDamage;
    }
}
