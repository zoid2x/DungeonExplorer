using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class Player
    {
        public string Name { get; set; }
        public int Health { get; private set; }
        private List<string> inventory = new List<string>();

        public Player(string name, int health) 
        {
            Name = name;
            Health = health;
        }
        public void PickUpItem(string item)
        {
            inventory.Add(item);
            Console.WriteLine($"{Name} picked up {item}!");
        }
        public string InventoryContents()
        {
            return string.Join(", ", inventory);
        }
    }
}