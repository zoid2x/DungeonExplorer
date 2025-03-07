using System;
using System.Media;
using System.Security.Cryptography.X509Certificates;

using System;

namespace DungeonExplorer
{
    public class Game
    {
        private Player player;
        private Room currentRoom;

        public Game()
        {
            player = new Player("Name", 100); // Initialize the player with their name and health
            currentRoom = new Room("\nYou see a door in front of you and a door to your right..."); // Initialize the room
        }

        public void Start()
        {
            Console.WriteLine("Welcome to the Haunted Dungeon!");
            Console.WriteLine("Name:");
            player.Name = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("\nYou awaken in dark,stone and cold room. You feel uneasy and have trouble remembering anything...");
            Console.WriteLine("You feel a sense of dread and fear as you look around the room.");
            Console.WriteLine("The only thing you manage to remember is that your name is " + player.Name + ".");

            Console.WriteLine(currentRoom.GetDescription()); // Display the current room's description

            string roomChoice = "";
            while (roomChoice != "living room" && roomChoice != "dining room")
            {
                Console.WriteLine("\nDo you want to enter the living room or the dining room?");
                Console.Write("> ");
                roomChoice = Console.ReadLine().ToLower();

                if (roomChoice != "living room" && roomChoice != "dining room")
                {
                    Console.WriteLine("You have entered an invalid choice. Please answer living room or dining room.");
                }
            }

            if (roomChoice == "living room")
            {
                Console.WriteLine("You have entered the living room.");
                Console.WriteLine("As you enter the room, you see a ghostly figure in the corner of the room.");

                string ghostChoice = "";
                while (ghostChoice != "yes" && ghostChoice != "no")
                {
                    Console.WriteLine("Do you want to approach the figure or leave the room? (yes/no)");
                    Console.Write("> ");
                    ghostChoice = Console.ReadLine().ToLower();

                    if (ghostChoice != "yes" && ghostChoice != "no")
                    {
                        Console.WriteLine("You have entered an invalid choice. Please answer yes or no.");
                    }
                }

                if (ghostChoice == "yes")
                {
                    Console.WriteLine("You approach the figure and it turns out to be a friendly ghost.");
                    Console.WriteLine("The ghost provides you with a magical weapon which will assist you on your journey!.");
                    player.PickUpItem("Spectre Wand"); // Adds the item to the player's inventory
                }
                else
                {
                    Console.WriteLine("You decide not to approach the figure and leave the room.");
                    Console.WriteLine("You turn back and see a 3 Headed Monster blocking the exit.");
                }
            }
            else if (roomChoice == "dining room")
            {
                Console.WriteLine("You have entered the dining room.");
                Console.WriteLine("As you enter the room, you see a shiny vase on the table.");

                string vaseChoice = "";
                while (vaseChoice != "yes" && vaseChoice != "no")
                {
                    Console.WriteLine("Do you want to open the vase or leave the room? (yes/no)");
                    Console.Write("> ");
                    vaseChoice = Console.ReadLine().ToLower();

                    if (vaseChoice != "yes" && vaseChoice != "no")
                    {
                        Console.WriteLine("You have entered an invalid choice. Please answer yes or no.");
                    }
                }

                if (vaseChoice == "yes")
                {
                    Console.WriteLine("You open the vase and find a key inside.");
                    Console.WriteLine("The key will help you unlock the door to the next room.");
                    player.PickUpItem("Key"); // Adds the item to the player's inventory
                }
                else
                {
                    Console.WriteLine("You decide not to open the vase and leave the room.");
                    Console.WriteLine("As you leave you hear a creepy whistle sound in the background.");
                }
            }

            // Display the player's inventory
            Console.WriteLine($"\n{player.Name}'s Inventory: {player.InventoryContents()}");
        }
    }
}


