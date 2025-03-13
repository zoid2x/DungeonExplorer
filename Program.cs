using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Run tests before starting the game
            Console.WriteLine("=== Running Tests ===");
            GameTest gameTest = new GameTest();
            gameTest.RunAllTests();
            Console.WriteLine("Tests completed. Starting the game...\n");

            // Start the game
            Game game = new Game();
            game.Start();
        }
    }
}