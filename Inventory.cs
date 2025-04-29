using System;

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

    // LINQ example
    public IEnumerable<Item> GetWeapons() =>
        items.Where(i => i.Type == "weapon");
}
}