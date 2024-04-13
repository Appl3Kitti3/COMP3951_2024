public class Bow : Weapon
{
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
