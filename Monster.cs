using System;

namespace DungeonExplorer
{
    /// <summary>
    /// Represents a monster in the dungeon.
    /// </summary>
    public class Monster
    {
        public string Name { get; }
        public int Power { get; }
        public int MaxHealth { get; }
        public int Health { get; set; }
        public int GoldReward { get; }

        public Monster(string name, int power, int maxHealth, int goldReward)
        {
            Name = name;
            Power = power;
            MaxHealth = maxHealth;
            Health = maxHealth;
            GoldReward = goldReward;
        }
    }
}