public class Staff : Weapon
{
    public Staff()
    {
        BaseDamage = Constants.BaseStaff;
    }

    protected override int CalculateDamage()
    {
        var actualDamage = (BaseDamage/3);
        
        // Critical Hit
        if (IsLandedOnCriticalHit())
            return actualDamage + CriticalDamage + Constants.MageCritical;
        return actualDamage;
    }

    protected override int UltimateAbility()
    {
        var actualDamage = (double) BaseDamage / 3 + BaseDamage * (1 / (double)Constants.StaffMultiplier);

        if (IsLandedOnCriticalHit())
            return (int) actualDamage + (CriticalDamage / 3) + Constants.MageCritical;
        return (int) actualDamage;
    }
}
