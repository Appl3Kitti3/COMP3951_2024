﻿namespace EndlessCatacombs
{
    /// <summary>
    /// Description:
    ///     An abstract class that represents a playable class that a player can select.
    ///     [Melee, Mage, Archer]
    /// Author: 
    /// Source: Applied C# Skills
    /// </summary>
    public abstract class Playable
    {
        public abstract string Name { get; }

        public abstract int BaseHealth { get; }

        // Move speed of the player
        protected int MoveSpeed { get; }

        public Weapon Weapon { get; set; }

        protected Playable(int speed)
        {
            MoveSpeed = speed;
        }
        // Returns a basic value of the move speed.
        public abstract int GetSpeed();
        
    }
}