using System;
using System.Collections.Generic;
using System.Linq;
using DungeonExplorer;

namespace DungeonExplorer
{
    public class Inventory
    {
        private List<Item> items = new List<Item>();

        public void AddItem(Item item) => items.Add(item);

        public string Contents()
        {
            return items.Count == 0 ? "Empty" :
                string.Join(", ", items.Select(i => i.Name));
        }

        public List<Item> GetItems()
        {
            return new List<Item>(items); // Return copy to prevent external modification
        }

        public IEnumerable<Item> GetItemsByType(string type) =>
            items.Where(i => i.Type.Equals(type, StringComparison.OrdinalIgnoreCase))
                 .OrderBy(i => i.Name);

        public IEnumerable<Item> GetWeapons() =>
            items.Where(i => i.Type == "weapon");

        // Gets the strongest weapon
        public Item GetStrongestWeapon() =>
        items.OfType<Item.Weapon>()
             .OrderByDescending(weapon => weapon.DamageBonus)
             .FirstOrDefault();
    }
}