using System;

namespace DungeonExplorer
{
    public class GameMap
    {
    private Dictionary<string, Room> rooms = new();

    public void AddConnection(string from, string to)
    {
        // Add your room connection logic
    }

    public Room GetRoom(string name) => rooms[name];
}
}
