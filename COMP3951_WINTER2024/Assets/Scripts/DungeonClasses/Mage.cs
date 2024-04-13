namespace EndlessCatacombs
{
    // Inherits from Playable class
    public class Mage : Playable
    {
        public Mage(int speed) : base(speed)
        {
            Weapon = new Staff();
        }
        
        public override string Name => "Mage";

        public override int BaseHealth => Constants.MaximumHealth;

        public override int GetSpeed()
        {
            return (MoveSpeed / 2);
        }

        public override int GetUltSpeed() {
            return (UltMoveSpeed / 2);
        }
    }
}