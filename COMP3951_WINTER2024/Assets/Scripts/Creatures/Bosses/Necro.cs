public class Necro : Boss
{
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
}
