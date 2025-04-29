using System;

namespace DungeonExplorer
{
    public abstract class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; protected set; } // LINQ filtering

        public Item(string name, string description = "")
        {
            Name = name;
            Description = description;
        }

        // Weapon subclass
        public class Weapon : Item
        {
            public int DamageBonus { get; set; }

            public Weapon(string name, int damageBonus, string description = "")
                : base(name, description)
            {
                Type = "weapon"; // Set type for LINQ filtering
                DamageBonus = damageBonus;
            }

            public override void Use(Player player)
            {
                player.WeaponValue += DamageBonus;
                Console.WriteLine($"{Name} equipped! Damage +{DamageBonus}.");
            }
        }


        // Potion subclass
        public class Potion : Item
        {
            public int HealAmount { get; set; }

            public Potion(string name, int healAmount, string description = "")
                : base(name, description)
            {
                Type = "potion"; // Set type for LINQ filtering
                HealAmount = healAmount;
            }

            public override void Use(Player player)
            {
                player.Health += HealAmount;
                Console.WriteLine($"{Name} consumed! Healed +{HealAmount} HP.");
            }
        }

        // Key subclass
        public class Key : Item
        {
            public string DoorID { get; set; }  // e.g., "final_room"

            public Key(string name, string doorID, string description = "")
                : base(name, description)
            {
                Type = "key"; // Set type for LINQ filtering
                DoorID = doorID;
            }

            public override void Use(Player player)
            {
                Console.WriteLine($"{Name} unlocks {DoorID}.");
            }
        }
    }
}
