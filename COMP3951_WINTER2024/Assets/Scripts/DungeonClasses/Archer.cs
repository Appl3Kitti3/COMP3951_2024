namespace EndlessCatacombs
{
    /// <summary>
    /// Description:
    ///     Archer uses a bow and has three maximum health.
    /// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
    /// Date: April 12 2024
    /// Source: Applied C# and Unity Skills
    /// </summary>
    public class Archer : Playable
    {
        
        // Create an Archer.
        public Archer()
        {
            Weapon = new Bow();
        }

        public override int BaseHealth => Constants.MinimumHealth;
        
        public override string Name => "Archer";

        public override int GetSpeed()
        {
            const int tmpSpeed = Constants.ArcherSpeed;
            return tmpSpeed + (tmpSpeed / 2);
        }
        public override int GetUltimateSpeed()
        {
            return Constants.ArcherUltSpeed * 2;
        }
    }
}