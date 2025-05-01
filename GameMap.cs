using System;
using System.Collections.Generic;

namespace DungeonExplorer
{
    public class GameMap
    {
        private Dictionary<string, Room> rooms = new Dictionary<string, Room>();
        public void AddRoom(string name, Room room)
        {
            rooms[name] = room;
        }

        public void AddConnection(string from, string to)
        {
            // Add room connection logic
        }

        public Room GetRoom(string name) => rooms[name];

        /// <summary>
        /// Validates room connections before transitioning.
        /// </summary>
        public bool CanMoveToRoom(string from, string to)
        {
            if (!rooms.ContainsKey(from) || !rooms.ContainsKey(to))
            {
                Console.WriteLine("\nError: Room connection doesn't exist!");
                return false;
            }

            return true;
        }
    }
}

