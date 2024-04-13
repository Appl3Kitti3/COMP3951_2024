namespace EndlessCatacombs
{
    // Inherits from Playable class
    public class Archer : Playable
    {
        public Archer(int speed) : base(speed)
        {
            Weapon = new Bow();
        }

        public override int BaseHealth => Constants.MinimumHealth;
        
        public override string Name => "Archer";

        public override int GetSpeed()
        {
            return MoveSpeed + (MoveSpeed / 2);
        }

        public override int GetUltSpeed() {
            return UltMoveSpeed + (UltMoveSpeed / 2);
        }
    }
}