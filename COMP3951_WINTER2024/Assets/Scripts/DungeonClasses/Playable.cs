namespace EndlessCatacombs
{
    /// <summary>
    /// Description:
    ///     An abstract class that represents a playable class that a player can select.
    ///     [Melee, Mage, Archer]
    /// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
    /// Source: Applied C# Skills
    /// </summary>
    public abstract class Playable
    {
        public abstract string Name { get; }
        
        // Move speed of the player
        public int MoveSpeed { get; set; }

        public Weapon Weapon { get; set; }

        protected Playable(int speed)
        {
            MoveSpeed = speed;
        }
        // Returns a basic value of the move speed.
        public abstract int GetSpeed();
        
    }
}