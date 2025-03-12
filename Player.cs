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

        private List<string> inventory = new List<string>();

        public Player(string name, int health) 
        {
            Name = name;
            Health = health;
            Damage = 1;
            ArmourValue = 0;
            Potion = 5;
            WeaponValue = 1;
        }
        public void PickUpItem(string item)
        {
            inventory.Add(item);
            Console.WriteLine($"{Name} picked up {item}!");
        }
        public string InventoryContents()
        {
            return inventory.Count == 0 ? "Empty" : string.Join(", ", inventory);
        }
    }
}