public class Staff : Weapon
{
    public Staff()
    {
        BaseDamage = Constants.BaseStaff;
    }
    
    public override int CalculateDamage()
    {
        int actualDamage = (BaseDamage/3);
        
        // Critical Hit
        if (IsLandedOnCriticalHit())
            return actualDamage + CritDamage + Constants.MageCritical;
        return actualDamage;
    }

    public override int UltimateAbility()
    {
        double actualDamage = (double) BaseDamage / 3 + BaseDamage * (1 / (double)Constants.StaffMultiplier);

        if (IsLandedOnCriticalHit())
            return (int) actualDamage + (CritDamage / 3) + Constants.MageCritical;
        return (int) actualDamage;
    }
}
