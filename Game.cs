using System;
using System.Linq;
using System.Media;
using System.Security.Cryptography.X509Certificates;
using DungeonExplorer;

namespace DungeonExplorer
{
    public class Game
    {
        private void GiveItemsToPlayer()
        {
            // Replace string items with Item objects:
            player.PickUpItem(new Item.Weapon("Spectre Wand", 1, "Ghostly weapon."));
            player.PickUpItem(new Item.Weapon("Gleaming Sword", 2, "A shining blade."));
            player.PickUpItem(new Item.Potion("Health Potion", 5, "Restores HP."));
            player.PickUpItem(new Item.Key("Dungeon Key", "final_room", "Opens the final door."));
        }

        private Player player;
        private Room currentRoom;
        private Room finalRoom; // Final room
        private GameMap map = new GameMap(); // GameMap instance

        public Game()
        {
            map.AddRoom("living", new Room("\nYou see a door..."));
            map.AddRoom("dining", new Room("\nA grand dining hall..."));
            map.AddRoom("final", new Room("\nThis is the final room..."));
            map.AddConnection("living", "dining");

            player = new Player("Name", 10); // Initialize the player with their name and health
            currentRoom = map.GetRoom("living"); // Use mapped room   
            finalRoom = map.GetRoom("final");
        }

        // Add the HandleFinalRoom method here
        private void HandleFinalRoom()
        {
            Console.Clear();
            Console.WriteLine(finalRoom.GetDescription());
            Console.WriteLine("You enter the final room...");
            Encounters.BasicFightEncounter(player); // Trigger the final encounter
        }

        public void Start()
        {
            Console.WriteLine("Welcome to the Haunted Dungeon!");
            Console.WriteLine("Your goal is to find the key to the final room and defeat the powerful presence within.");


            void CheckName()
            {
                while (true) // Loop until a valid name is entered
                {
                    Console.WriteLine("Name:");
                    Console.Write("> ");
                    string inputName = Console.ReadLine();

                    if (string.IsNullOrEmpty(inputName))
                    {
                        Console.WriteLine("\nName can't be empty.");
                    }
                    else if (inputName.Any(char.IsWhiteSpace) || !inputName.All(char.IsLetter))
                    {
                        Console.WriteLine("\nName can't contain any whitespace or numbers.");
                    }
                    else
                    {
                        player.Name = inputName; // Set the player's name
                        break; // Exit the loop if the name is valid
                    }
                }
            }

            CheckName();
            Console.Clear();

            Console.WriteLine("\nYou awaken in a dark, stone-cold room. You feel uneasy and have trouble remembering anything...");
            Console.WriteLine("The only thing you manage to remember is that your name is " + player.Name + ".");

            bool hasKey = false;
            bool hasWeapon = false;

            while (true) // Loop indefinitely until the player chooses to enter the final room
            {
                Console.WriteLine(currentRoom.GetDescription()); // Display the current room's description

                string roomChoice = "";
                while (roomChoice != "living room" && roomChoice != "dining room" && roomChoice != "armory" && roomChoice != "library")
                {
                    Console.WriteLine("\nDo you want to enter the living room, dining room, armory, or library?");
                    Console.Write("> ");
                    roomChoice = Console.ReadLine().ToLower();

                    if (roomChoice != "living room" && roomChoice != "dining room" && roomChoice != "armory" && roomChoice != "library")
                    {
                        Console.WriteLine("You have entered an invalid choice. Please answer living room, dining room, armory, or library.");
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
                        player.PickUpItem(new Item.Weapon("Spectre Wand", 1, "Ghostly weapon")); // Adds the item to the player's inventory
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
                        player.PickUpItem(new Item.Key("Dungeon Key", "final_room", "Opens final door")); // Adds the item to the player's inventory
                        hasKey = true; // Update key status
                    }
                    else
                    {
                        Console.WriteLine("\nYou decide not to open the vase and leave the room.");
                        Console.WriteLine("As you leave, you hear a creepy whistle sound in the background.");
                        Encounters.SecondEncounter(player); // This triggers the encounter
                    }
                }
                else if (roomChoice == "armory")
                {
                    Console.WriteLine("You have entered the armory.");
                    Console.WriteLine("\nAs you enter the room, you see a gleaming sword mounted on the wall.");

                    string swordChoice = "";
                    while (swordChoice != "yes" && swordChoice != "no")
                    {
                        Console.WriteLine("Do you want to take the sword or leave the room? (yes/no)");
                        Console.Write("> ");
                        swordChoice = Console.ReadLine().ToLower();

                        if (swordChoice != "yes" && swordChoice != "no")
                        {
                            Console.WriteLine("You have entered an invalid choice. Please answer yes or no.");
                        }
                    }

                    if (swordChoice == "yes")
                    {
                        Console.Clear();
                        Console.WriteLine("You take the gleaming sword. It feels powerful in your hands!");
                        player.PickUpItem(new Item.Weapon("Gleaming Sword", 2, "A shining blade")); // Adds the item to the player's inventory
                        hasWeapon = true; // Update weapon status
                    }
                    else
                    {
                        Console.WriteLine("You decide not to take the sword and leave the room.");
                    }
                }
                else if (roomChoice == "library")
                {
                    Console.WriteLine("You have entered the library (Press Enter...)");
                    Console.ReadKey();
                    Encounters.ThirdEncounter(player); // Trigger the third encounter
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

                        // Input validation for finalChoice
                        while (finalChoice != "enter" && finalChoice != "explore")
                        {
                            Console.WriteLine("Invalid input. Please type 'enter' to proceed to the final room or 'explore' to keep exploring.");
                            Console.Write("> ");
                            finalChoice = Console.ReadLine().ToLower();
                        }

                        if (finalChoice == "enter")
                        {
                            HandleFinalRoom(); // Call the HandleFinalRoom method
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

                        // Input validation for finalChoice
                        while (finalChoice != "yes" && finalChoice != "no")
                        {
                            Console.WriteLine("Invalid input. Please type 'yes' to proceed to the final room or 'no' to keep exploring.");
                            Console.Write("> ");
                            finalChoice = Console.ReadLine().ToLower();
                        }

                        if (finalChoice == "yes")
                        {
                            HandleFinalRoom(); // Call the HandleFinalRoom method
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









