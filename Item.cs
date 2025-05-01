using System;
using System.Collections.ObjectModel;
using System.Security.Policy;
using DungeonExplorer;

namespace DungeonExplorer
{
    /// <summary>
    /// Abstract base class for all items in the game.
    /// </summary>
    public abstract class Item : ICollectible
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; protected set; } // LINQ filtering

        public Item(string name, string description = "")
        {
            Name = name;
            Description = description;
        }


        public void OnCollect(Creature collector)
        {
            Console.WriteLine($"{collector.Name} collected {Name}!");
            Use(collector); // Call the abstract Use method
        }


        /// <summary>
        /// Called when the item is collected by a creature.
        /// Implementation of ICollectible.OnCollect.
        /// </summary>
        /// <param name="collector">The creature that collected this item.</param>
        public abstract void Use(Creature creature);

        public virtual string GetInfo()
        {
            return $"{Name}: {Description}";
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

            /// <summary>
            /// Overrides base Use method with weapon-specific behavior.
            /// </summary>
            public override void Use(Creature creature)
            {
                if (creature is Player player)
                {
                    player.WeaponValue += DamageBonus;
                    Console.WriteLine($"{Name} equipped! Damage +{DamageBonus}.");
                }
            }

            /// <summary>
            /// Overrides GetInfo with weapon-specific details.
            /// </summary>
            public override string GetInfo()
            {
                return $"{base.GetInfo()} (Damage Bonus: {DamageBonus})";
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

            /// <summary>
            /// Overrides base Use method with potion-specific behavior.
            /// </summary>
            public override void Use(Creature creature)
            {
                creature.Heal(HealAmount);
                Console.WriteLine($"{Name} consumed! Healed +{HealAmount} HP.");
            }

            /// <summary>
            /// Overrides GetInfo with potion-specific details.
            /// </summary>
            public override string GetInfo()
            {
                return $"{base.GetInfo()} (Heals: {HealAmount} HP)";
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

            public override void Use(Creature creature)
            {
                Console.WriteLine($"{Name} unlocks {DoorID}.");
            }
        }
    }
}
    
        
    

