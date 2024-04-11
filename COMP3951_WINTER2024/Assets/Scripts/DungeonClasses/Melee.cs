namespace EndlessCatacombs
{
    // Inherits from Playable class
    public class Melee : Playable
    {
        public Melee(int speed) : base(speed)
        {
            Weapon = new Mace();
        }

        public override string Name => "Melee";

        public override int BaseHealth => Constants.MaximumHealth;

        public override int GetSpeed()
        {
            return MoveSpeed + MoveSpeed;
        }

        public override int GetUltSpeed() {
            return UltMoveSpeed + UltMoveSpeed;
        }
    }
}