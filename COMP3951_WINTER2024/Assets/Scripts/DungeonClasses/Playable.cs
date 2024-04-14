namespace EndlessCatacombs
{
    /// <summary>
    /// Description:
    ///     An abstract class that represents a playable class that a player can select.
    ///     [Melee, Mage, Archer]
    /// Author: Tedrik "Teddy" Dumam-Ag (A01329707)
    /// Date: April 12 2024
    /// Source: Applied C# Skills
    /// </summary>
    public abstract class Playable
    {
        // Name of the class.
        public abstract string Name { get; }

        // Base Health points.
        public abstract int BaseHealth { get; }
        
        // Property of its Weapon.
        public Weapon Weapon { get; protected set; }
        
        // Returns a calculation of its regular speed.
        public abstract int GetSpeed();

        // Returns a calculation of its ultimate speed.
        public abstract int GetUltimateSpeed();
    }
}