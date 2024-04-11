public class Mace : Weapon
{
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
