using System;
using System.Linq;
using System.Media;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using DungeonExplorer;

namespace DungeonExplorer
{
    public class Game
    {
        private Player player;
        private Room currentRoom;
        private Room finalRoom; // Final room
        private GameMap map = new GameMap(); // GameMap instance

        public Game()
        {
            // Initialize the game map with 7 rooms
            map.AddRoom("entrance", new Room("\nYou stand in the cold, stone entrance hall. Torches flicker on the walls."));
            map.AddRoom("living", new Room("\nA cozy living room with a fireplace. The furniture is covered in dust."));
            map.AddRoom("dining", new Room("\nA grand dining hall with a long table set for a feast that never happened."));
            map.AddRoom("kitchen", new Room("\nA large kitchen with rusty pots and pans hanging from the ceiling."));
            map.AddRoom("armory", new Room("\nThe armory contains racks of weapons and armor, some still in good condition."));
            map.AddRoom("library", new Room("\nTall bookshelves line the walls of this ancient library. The air smells of old parchment."));
            map.AddRoom("final", new Room("\nThis is the final chamber. The air hums with dark energy."));

            // Connect rooms
            map.AddConnection("entrance", "living");
            map.AddConnection("living", "dining");
            map.AddConnection("dining", "kitchen");
            map.AddConnection("living", "armory");
            map.AddConnection("living", "library");
            map.AddConnection("kitchen", "final");

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

        /// <summary>
        /// Saves basic game data to a simple text file.
        /// </summary>
        private void QuickSave()
        {
            try
            {
                File.WriteAllText("quicksave.txt", player.GetSimpleSaveData());
                Console.WriteLine("\nGame saved successfully!");
            }
            catch
            {
                Console.WriteLine("\nFailed to save game.");
            }
        }

        /// <summary>
        /// Loads basic game data from the save file.
        /// </summary>
        private bool QuickLoad()
        {
            if (!File.Exists("quicksave.txt"))
            {
                Console.WriteLine("\nNo save file found.");
                return false;
            }

            try
            {
                player.LoadSimpleData(File.ReadAllText("quicksave.txt"));
                Console.WriteLine("\nGame loaded successfully!");
                return true;
            }
            catch
            {
                Console.WriteLine("\nFailed to load game.");
                return false;
            }
        }

        public void Start()
        {
            Console.WriteLine("Welcome to the Haunted Dungeon!");
            Console.WriteLine("Press L to load or any other key to start new...");
            Console.Write("> ");
            var key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.L)
            {
                if (!QuickLoad())
                {
                    Console.WriteLine("Starting new game instead...");
                }
            }

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
                if (Console.KeyAvailable)
                {
                    var saveKey = Console.ReadKey(true).Key;
                    if (saveKey == ConsoleKey.S)
                    {
                        QuickSave();
                        continue;
                    }
                }

                Console.WriteLine(currentRoom.GetDescription());

                string roomChoice = "";
                string roomKey = "";
                while (true)
                {
                    Console.WriteLine("\nWhere do you want to go? (entrance, living room, dining room, kitchen, armory, library, final room)");
                    Console.Write("> ");
                    roomChoice = Console.ReadLine().ToLower().Trim();

                    // Convert input to room key
                    roomKey = roomChoice.Replace(" ", "") switch
                    {
                        "livingroom" => "living",
                        "diningroom" => "dining",
                        "finalroom" => "final",
                        "entrance" => "entrance",
                        "kitchen" => "kitchen",
                        "armory" => "armory",
                        "library" => "library",
                        _ => "invalid"
                    };

                    if (roomKey != "invalid" && map.CanMoveToRoom(currentRoom == null ? "entrance" : "living", roomKey))
                        break;

                    Console.WriteLine("Invalid choice. Please choose from the available rooms.");
                }

                currentRoom = map.GetRoom(roomKey);

                // Handle room events based on roomKey
                if (roomKey == "living")
                {
                    Console.WriteLine("You have entered the living room.");
                    Console.WriteLine("\nAs you enter the room, you see a ghostly figure in the corner.");

                    string ghostChoice = "";
                    while (ghostChoice != "yes" && ghostChoice != "no")
                    {
                        Console.WriteLine("Do you want to approach the figure? (yes/no)");
                        Console.Write("> ");
                        ghostChoice = Console.ReadLine().ToLower();

                        if (ghostChoice != "yes" && ghostChoice != "no")
                            Console.WriteLine("Invalid choice. Please answer yes or no.");
                    }

                    if (ghostChoice == "yes")
                    {
                        Console.Clear();
                        Console.WriteLine("You approach the figure - it's a friendly ghost!");
                        Console.WriteLine("The ghost gives you a magical weapon!");
                        player.PickUpItem(new Item.Weapon("Spectre Wand", 1, "Ghostly weapon"));
                        hasWeapon = true;
                    }
                    else
                    {
                        Console.WriteLine("You avoid the figure, but a monster blocks your exit!");
                        Encounters.FirstEncounter(player);
                    }
                }
                else if (roomKey == "dining")
                {
                    Console.WriteLine("You enter the dining room.");
                    Console.WriteLine("\nA shiny vase sits on the table.");

                    string vaseChoice = "";
                    while (vaseChoice != "yes" && vaseChoice != "no")
                    {
                        Console.WriteLine("Open the vase? (yes/no)");
                        Console.Write("> ");
                        vaseChoice = Console.ReadLine().ToLower();

                        if (vaseChoice != "yes" && vaseChoice != "no")
                            Console.WriteLine("Invalid choice. Please answer yes or no.");
                    }

                    if (vaseChoice == "yes")
                    {
                        Console.WriteLine("\nYou find a key inside the vase!");
                        player.PickUpItem(new Item.Key("Dungeon Key", "final_room", "Opens final door"));
                        hasKey = true;
                    }
                    else
                    {
                        Console.WriteLine("\nYou leave the vase alone... but hear creepy noises!");
                        Encounters.SecondEncounter(player);
                    }
                }
                else if (roomKey == "armory")
                {
                    Console.WriteLine("You enter the armory.");
                    Console.WriteLine("\nA gleaming sword hangs on the wall.");

                    string swordChoice = "";
                    while (swordChoice != "yes" && swordChoice != "no")
                    {
                        Console.WriteLine("Take the sword? (yes/no)");
                        Console.Write("> ");
                        swordChoice = Console.ReadLine().ToLower();

                        if (swordChoice != "yes" && swordChoice != "no")
                            Console.WriteLine("Invalid choice. Please answer yes or no.");
                    }

                    if (swordChoice == "yes")
                    {
                        Console.Clear();
                        Console.WriteLine("You take the gleaming sword!");
                        player.PickUpItem(new Item.Weapon("Gleaming Sword", 2, "A shining blade"));
                        hasWeapon = true;
                    }
                    else
                    {
                        Console.WriteLine("You leave the sword untouched.");
                    }
                }
                else if (roomKey == "library")
                {
                    Console.WriteLine("You enter the library... (Press Enter)");
                    Console.ReadKey();
                    Encounters.ThirdEncounter(player);
                }
                else if (roomKey == "entrance")
                {
                    Console.WriteLine("You return to the entrance.");
                    Console.WriteLine("\nTorches flicker as you consider your next move.");
                }
                else if (roomKey == "kitchen")
                {
                    Console.WriteLine("You enter the kitchen.");
                    Console.WriteLine("\nRats scurry as you look around.");

                    string pantryChoice = "";
                    while (pantryChoice != "yes" && pantryChoice != "no")
                    {
                        Console.WriteLine("Search the pantry? (yes/no)");
                        Console.Write("> ");
                        pantryChoice = Console.ReadLine().ToLower();

                        if (pantryChoice != "yes" && pantryChoice != "no")
                            Console.WriteLine("Invalid choice. Please answer yes or no.");
                    }

                    if (pantryChoice == "yes")
                    {
                        Console.WriteLine("\nYou find a health potion!");
                        player.PickUpItem(new Item.Potion("Health Potion", 5, "Restores 5 HP"));
                        player.Heal(5);
                    }
                    else
                    {
                        Console.WriteLine("\nYou leave the pantry alone.");
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









