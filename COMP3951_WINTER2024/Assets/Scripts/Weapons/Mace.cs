public class Mace : Weapon
{
    public Mace()
    {
        BaseDamage = Constants.BaseMace;
    }
    public override int CalculateDamage()
    {
        int actualDamage = BaseDamage + Constants.MaceBonus;
        
        // Critical Hit
        if (IsLandedOnCriticalHit())
            return actualDamage + CritDamage + Constants.BerserkerCritical;
        return actualDamage;
    }

    public override int UltimateAbility()
    {
        double actualDamage = BaseDamage + BaseDamage + Constants.MaceBonus;

        if (IsLandedOnCriticalHit())
            return (int) actualDamage + (CritDamage / 2) + Constants.BerserkerCritical;
        return (int) actualDamage;
        
    }
}
