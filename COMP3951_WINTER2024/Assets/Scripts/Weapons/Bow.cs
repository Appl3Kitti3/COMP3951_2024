public class Bow : Weapon
{
    public Bow()
    {
        BaseDamage = Constants.BaseBow;
    }
    public override int CalculateDamage()
    {
        // dmg / 2
        int actualDamage = BaseDamage / 2;
        
        // Critical Hit
        if (IsLandedOnCriticalHit())
            return actualDamage + CritDamage + Constants.ArcherCritical;
        return actualDamage;

    }

    public override int UltimateAbility()
    {
        double actualDamage = BaseDamage + (BaseDamage * (1 / (double)Constants.BowMultiplier));

        if (IsLandedOnCriticalHit())
            return (int) actualDamage + (CritDamage / 2) + Constants.ArcherCritical;
        return (int) actualDamage;
    }
}
