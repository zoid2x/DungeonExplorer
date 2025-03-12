using System;
using System.Media;
using System.Security.Cryptography.X509Certificates;

namespace DungeonExplorer
{
    public class Game
    {
        private Player player;
        private Room currentRoom;
        private Room finalRoom; // Final room

        public Game()
        {
            player = new Player("Name", 20); // Initialize the player with their name and health
            currentRoom = new Room("\nYou see a door in front of you and a door to your right...");
            finalRoom = new Room("\nThis is the final room. A powerful presence awaits you...");
        }

        public void Start()
        {
            Console.WriteLine("Welcome to the Haunted Dungeon!");
            Console.WriteLine("Name:");
            Console.Write("> ");
            player.Name = Console.ReadLine();
            Console.Clear();

            Console.WriteLine("\nYou awaken in a dark, stone-cold room. You feel uneasy and have trouble remembering anything...");
            Console.WriteLine("The only thing you manage to remember is that your name is " + player.Name + ".");

            bool hasKey = false;
            bool hasWeapon = false;

            while (true) // Loop indefinitely until the player chooses to enter the final room
            {
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
                    Console.WriteLine("\nAs you enter the room, you see a ghostly figure in the corner of the room.");

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
                        Console.Clear();
                        Console.WriteLine("You approach the figure and it turns out to be a friendly ghost.");
                        Console.WriteLine("The ghost provides you with a magical weapon which will assist you on your journey!");
                        player.PickUpItem("Spectre Wand"); // Adds the item to the player's inventory
                        hasWeapon = true; // Update weapon status
                    }
                    else
                    {
                        Console.WriteLine("You decide not to approach the figure and leave the room.");
                        Console.WriteLine("You turn back and see a 3-Headed Monster blocking the exit.");
                        Encounters.FirstEncounter(player); // This triggers the first encounter
                    }
                }
                else if (roomChoice == "dining room")
                {
                    Console.WriteLine("You have entered the dining room.");
                    Console.WriteLine("\nAs you enter the room, you see a shiny vase on the table.");

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
                        Console.WriteLine("\nYou open the vase and find a key inside.");
                        Console.WriteLine("The key will help you unlock the door to the next room.");
                        player.PickUpItem("Key"); // Adds the item to the player's inventory
                        hasKey = true; // Update key status
                    }
                    else
                    {
                        Console.WriteLine("\nYou decide not to open the vase and leave the room.");
                        Console.WriteLine("As you leave, you hear a creepy whistle sound in the background.");
                        Encounters.SecondEncounter(player); // This triggers the encounter
                    }
                }

                // Display inventory
                Console.WriteLine($"\n{player.Name}'s Inventory: {player.InventoryContents()}");

                // Check if the player has the key
                if (hasKey)
                {
                    Console.Clear();
                    Console.WriteLine("You have the key to the final room.");
                    if (!hasWeapon)
                    {
                        Console.WriteLine("\nYou can enter the final room now, but you don't have a weapon. It might be dangerous!");
                        Console.WriteLine("Would you like to enter the final room or keep exploring to find a weapon? (enter/explore)");
                        Console.Write("> ");
                        string finalChoice = Console.ReadLine().ToLower();

                        if (finalChoice == "enter")
                        {
                            Console.Clear();
                            Console.WriteLine(finalRoom.GetDescription());
                            Console.WriteLine("You enter the final room...");
                            Encounters.BasicFightEncounter(player); // Trigger the final encounter
                            return; // End the game after the final encounter
                        }
                        else
                        {
                            Console.WriteLine("You decide to keep exploring...");
                        }
                    }
                    else
                    {
                        Console.WriteLine("You have both the key and the weapon. Would you like to enter the final room? (yes/no)");
                        Console.Write("> ");
                        string finalChoice = Console.ReadLine().ToLower();

                        if (finalChoice == "yes")
                        {
                            Console.Clear();
                            Console.WriteLine(finalRoom.GetDescription());
                            Console.WriteLine("You enter the final room...");
                            Encounters.BasicFightEncounter(player); // Trigger the final encounter
                            return; // End the game after the final encounter
                        }
                        else
                        {
                            Console.WriteLine("You decide to keep exploring...");
                        }
                    }
                }
            }
        }
    }
}

