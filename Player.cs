using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace DungeonExplorer
{
    /// <summary>
    /// Represents the player in the game.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Gets or sets the player's name.
        /// </summary>
        public string Name { get; set; } // Make these a property
        
        /// <summary>
        /// Gets or sets the player's health.
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// Gets or sets the player's damage.
        /// </summary>
        public int Damage { get; set; }

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

        /// <summary>
        /// Gets the player's maximum health.
        /// </summary>
        public int MaxHealth { get; private set; } // Add a property for MaxHealth

        private List<string> inventory = new List<string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="name">The player's name.</param>
        /// <param name="health">The player's initial health.</param>
        public Player(string name, int health)
        {
            Name = name;
            Health = health;
            MaxHealth = health;
            Damage = 1;
            ArmourValue = 0;
            Potion = 5;
            WeaponValue = 1;
        }

        /// <summary>
        /// Adds an item to the player's inventory and applies stat bonuses.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void PickUpItem(string item)
        {
            inventory.Add(item);
            Console.WriteLine($"{Name} picked up {item}!");

            // Apply stat bonuses for weapons
            if (item == "Spectre Wand")
            {
                WeaponValue += 1; // Increase weapon value by 2
                Damage += 1; // Increase base damage by 2
                Console.WriteLine($"Your damage has increased to {Damage}!");
            }
            else if (item == "Gleaming Sword")
            {
                WeaponValue += 2; 
                Damage += 2; 
                Console.WriteLine($"Your damage has increased to {Damage}!");
            }
            else if (item == "Magical Staff")
            {
                WeaponValue += 3; 
                Damage += 3; 
                Console.WriteLine($"Your damage has increased to {Damage}!");
            }
        }

        /// <summary>
        /// Returns the contents of the player's inventory.
        /// </summary>
        /// <returns>A string representation of the inventory.</returns>
        public string InventoryContents()
        {
            return inventory.Count == 0 ? "Empty" : string.Join(", ", inventory);
        }

        // Add a method to heal the player without exceeding MaxHealth

        /// <summary>
        /// Heals the player by a specified amount without exceeding MaxHealth.
        /// </summary>
        /// <param name="amount">The amount to heal.</param>
        public void Heal(int amount)
        {
            Health += amount;
            if (Health > MaxHealth)
            {
                Health = MaxHealth; // Cap health at MaxHealth
            }
        }
    }
}