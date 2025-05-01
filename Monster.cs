using System;
using DungeonExplorer;

namespace DungeonExplorer
{
    /// <summary>
    /// Represents a monster in the dungeon.
    /// Inherits from Creature base class.
    /// </summary>
    public class Monster : Creature
    {
        /// <summary>
        /// Gets the gold reward for defeating this monster.
        /// </summary>
        public int GoldReward { get; }

        /// <summary>
        /// Virtual method for attack behavior that can be overridden by derived classes.
        /// </summary>
        /// <param name="player">The player being attacked.</param>
        /// <returns>Damage dealt by the attack.</returns>
        public virtual int Attack(Player player)
        {
            int damage = Power - player.ArmourValue;
            Console.WriteLine($"{Name} attacks with a basic strike!");
            return Math.Max(damage, 0);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Monster"/> class.
        /// </summary>
        /// <param name="name">The monster's name.</param>
        /// <param name="power">The monster's attack power.</param>
        /// <param name="maxHealth">The monster's maximum health.</param>
        /// <param name="goldReward">Gold awarded when defeated.</param>

        public Monster(string name) : this(name, 5, 10, 5) { } // Default stats
        public Monster(string name, int power, int maxHealth, int goldReward) : base()
        {
            Name = name;
            Power = power;
            MaxHealth = maxHealth;
            Health = maxHealth;
            GoldReward = goldReward;
        }
    }

    /// <summary>
    /// Goblin monster with quick but weak attacks.
    /// </summary>
    public class Goblin : Monster
    {
        public Goblin(string name, int power, int maxHealth, int goldReward)
            : base(name, power, maxHealth, goldReward) { }


        private static Random rand = new Random();

        /// <summary>
        /// Overrides base attack with a quick double strike (50% chance).
        /// </summary>
        public override int Attack(Player player)
        {
            if (rand.Next(0, 2) == 0) // 50% chance
            {
                int damage = (Power / 2 - player.ArmourValue) * 2;
                Console.WriteLine($"{Name} performs a quick double strike!");
                return Math.Max(damage, 0);
            }
            return base.Attack(player);
        }
    }

    /// <summary>
    /// Dragon monster with powerful fire breath attack.
    /// </summary>
    public class Dragon : Monster
    {
        private static Random rand = new Random();
        public Dragon(string name, int power, int maxHealth, int goldReward)
            : base(name, power, maxHealth, goldReward) { }

        /// <summary>
        /// Overrides base attack with a powerful fire breath (ignores armor).
        /// </summary>
        public override int Attack(Player player)
        {
            Console.WriteLine($"{Name} unleashes a terrifying fire breath!");
            return Power + rand.Next(0, 3); // Adds random 0-2 bonus damage
        }
    }
}
