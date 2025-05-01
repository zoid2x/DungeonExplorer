using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;
using DungeonExplorer;
using System.Linq;

namespace DungeonExplorer
{
    /// <summary>
    /// Represents the player in the game.
    /// Inherits from Creature base class.
    /// </summary>
    public class Player : Creature
    {
        /// <summary>
        /// Gets ir sets the player's damage value.
        /// </summary>
        public int Damage => Power + WeaponValue;

        /// <summary>
        /// Gets or sets the player's armor value.
        /// </summary>
        public int ArmourValue { get; set; }

        /// <summary>
        /// Gets or sets the number of potions the player has.
        /// </summary>
        public int Potion { get; set; }

        /// <summary>
        /// Gets or sets the player's weapon value.
        /// </summary>
        public int WeaponValue { get; set; }

        private Inventory inventory = new Inventory();

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="name">The player's name.</param>
        /// <param name="health">The player's initial health.</param>
        public Player(string name, int health) : base()
        {
            Name = name;
            Health = health;
            MaxHealth = health;
            Power = 1; // Base attack power
            ArmourValue = 0;
            Potion = 5;
            WeaponValue = 1;
        }

        /// <summary>
        /// Adds an item to the player's inventory and applies stat bonuses.
        /// </summary>
        /// <param name="item">The item to add, must implement ICollectible.</param>
        public void PickUpItem(ICollectible item)
        {
            item.OnCollect(this); // This will call the item's specific collection logic
            if (item is Item concreteItem)
            {
                inventory.AddItem(concreteItem);
                Console.WriteLine($"{Name} picked up {concreteItem.Name}!");
            }
        }

        /// <summary>
        /// Returns the contents of the player's inventory.
        /// </summary>
        /// <returns>A string representation of the inventory.</returns>
        public string InventoryContents()
        {
            return inventory.Contents();
        }

        // Add a method to heal the player without exceeding MaxHealth

        /// <summary>
        /// Heals the player by a specified amount without exceeding MaxHealth.
        /// </summary>
        /// <param name="amount">The amount to heal.</param>
        public override void Heal(int amount)
        {
            Health = Math.Min(Health + amount, MaxHealth);
        }

        /// <summary>
        /// Creates a simple string containing essential player data for saving.
        /// </summary>
        public string GetSimpleSaveData()
        {
            // Format: Name,Health,MaxHealth,PotionCount,HasKey,HasWeapon
            bool hasKey = inventory.GetItems().Any(i => i is Item.Key);
            bool hasWeapon = inventory.GetItems().Any(i => i is Item.Weapon);
            return $"{Name},{Health},{MaxHealth},{Potion},{hasKey},{hasWeapon}";
        }

        /// <summary>
        /// Loads basic player data from a saved string.
        /// </summary>
        public void LoadSimpleData(string data)
        {
            string[] values = data.Split(',');
            if (values.Length != 6) return;

            // Clear inventory
            inventory = new Inventory();

            Name = values[0];
            Health = int.Parse(values[1]);
            MaxHealth = int.Parse(values[2]);
            Potion = int.Parse(values[3]);

            // Restore key if had one
            if (bool.Parse(values[4]))
            {
                PickUpItem(new Item.Key("Dungeon Key", "final_room", "Restored key"));
            }

            // Restore weapon if had one
            if (bool.Parse(values[5]))
            {
                PickUpItem(new Item.Weapon("Basic Sword", 1, "Restored weapon"));
            }
        }
    }
}