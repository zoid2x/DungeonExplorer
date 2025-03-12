using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace DungeonExplorer
{
    public class Player
    {
        public string Name { get; set; } // Make these a property
        public int Health { get; set; }
        public int Damage { get; set; }
        public int ArmourValue { get; set; }
        public int Potion { get; set; }
        public int WeaponValue { get; set; }
        public int MaxHealth { get; private set; } // Add a property for MaxHealth

        private List<string> inventory = new List<string>();

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

        public string InventoryContents()
        {
            return inventory.Count == 0 ? "Empty" : string.Join(", ", inventory);
        }

        // Add a method to heal the player without exceeding MaxHealth
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