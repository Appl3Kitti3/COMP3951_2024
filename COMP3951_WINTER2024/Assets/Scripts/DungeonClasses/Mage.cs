namespace EndlessCatacombs
{
    /// <summary>
    /// Description:
    ///     Mage uses a staff and five maximum health.
    /// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
    /// Date: April 12 2024
    /// Source: Applied C# and Unity Skills
    /// </summary>
    public class Mage : Playable
    {
        // Create a Mage.
        public Mage()
        {
            Weapon = new Staff();
        }

        public override int BaseHealth => Constants.MaximumHealth;
        public override string Name => "Mage";

        public override int GetSpeed()
        {
            const int tmpSpeed = Constants.MageSpeed;
            return tmpSpeed - (tmpSpeed / 3);
        }

        public override int GetUltimateSpeed()
        {
            return Constants.MageUltSpeed + 3;
        }
    }
}