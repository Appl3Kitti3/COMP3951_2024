/// <summary>
///
/// Necro's class only modifies its primary ability to summon 8 projectiles. And handles a check whenever the original projectile is active.
/// 
/// Author: Tedrik "Teddy" Dumam-Ag 
/// Date: April 12 2024
/// Sources: Applied C# and OOP skills.
/// </summary>
public class Necro : Boss
{
    // Create 8 projectiles.
    protected override void PerformPrimary()
    {
        CreateProjectile(1);
        CreateProjectile(-1);
        
        CreateProjectile(1, 1);
        CreateProjectile(-1, 1);
        
        CreateProjectile(1, -1);
        CreateProjectile(-1, -1);
        
        CreateProjectile(0, 1);
        CreateProjectile(0, -1);
    }

    // If original is active, return, otherwise continue.
    protected override void NecroProjectileHandle()
    {
        if (Projectile.activeInHierarchy) return;
        Animator.SetTrigger(Primary);
        PerformPrimary();
    }
}
