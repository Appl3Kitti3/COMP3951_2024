namespace EndlessCatacombs
{
    /// <summary>
    /// Description:
    ///     Melee has a mace and has five health points.
    /// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
    /// Date: April 12 2024
    /// Source: Applied C# and Unity Skills
    /// </summary>
    public class Melee : Playable
    {
        // Create a Melee.
        public Melee()
        {
            Weapon = new Mace();
        }

        public override int BaseHealth => Constants.MaximumHealth;
        public override string Name => "Melee";

        public override int GetSpeed()
        {
            return Constants.MeleeSpeed * 2;
        }

        public override int GetUltimateSpeed()
        {
            return Constants.MeleeUltSpeed * 2;
        }
    }
}