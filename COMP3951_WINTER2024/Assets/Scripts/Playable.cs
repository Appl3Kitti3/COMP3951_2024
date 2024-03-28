namespace DefaultNamespace
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
        // Move speed of the player
        private int _moveSpeed;

        private Weapon _w;
        
        // Returns a basic value of the move speed.
        public abstract int Move();

        // Adds an additional speed boost when returned.
        public abstract int Sprint();
    }
}