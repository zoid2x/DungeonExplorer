using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    /// <summary>
    /// Handles all encounters and combat logic in the game.
    /// </summary>
    public class Encounters
    {
        static Random rand = new Random(); // Random number generator for the encounters


        // Encounters

        /// <summary>
        /// Triggers the first encounter with a 3-headed monster.
        /// </summary>
        /// <param name="player">The player object participating in the encounter.</param>
        public static void FirstEncounter(Player player)
        {
            Console.WriteLine("\nThis huge 3 Headed Monster is blocking your exit");
            Console.WriteLine("He turns... (Press ENTER)");
            Console.ReadKey();
            Combat(player, false, "3 Headed Monster", 2, 8);
        }

        /// <summary>
        /// Triggers a basic fight encounter with a random enemy. (Used in the final boss encounter)
        /// </summary>
        /// <param name="player">The player object participating in the encounter.</param>
        public static void BasicFightEncounter (Player player)
        {
            Console.Clear();
            Console.WriteLine("Ahead of you is a huge shadow...");
            Console.WriteLine("You enter into the final battle... Will you prevail? (Press ENTER)");
            Console.ReadKey();
            Combat(player, true, "", 0, 0, true); // Set `isFinalBoss` to true
        }

        /// <summary>
        /// Triggers the second encounter with a giant spider.
        /// </summary>
        /// <param name="player">The player object participating in the encounter.</param>
        public static void SecondEncounter(Player player)
        {
            Console.WriteLine("\nOut of nowhere this 7 foot Spider Drops down from the roof");
            Console.WriteLine("His fangs drip with venom as it stares at you with murderous eyes... (Press ENTER)");
            Console.ReadKey();
            Combat(player, false, "Big ahh Spider", 3, 8);
        }

        /// <summary>
        /// Triggers the third encounter with a venomous creature.
        /// </summary>
        /// <param name="player">The player object participating in the encounter.</param>
        public static void ThirdEncounter(Player player)
        {
            Console.Clear();
            Console.WriteLine("As you enter the room, a powerful Venom Fang emerges from the floor!");
            Console.WriteLine("Its eyes glow with dark green venom as it prepares to strike... (Press ENTER)");
            Console.ReadKey();
            Combat(player, false, "Venom Fang", 3, 10);
        }



        // Encounter Tools

        /// <summary>
        /// Handles the combat logic between the player and an enemy.
        /// </summary>
        /// <param name="player">The player object.</param>
        /// <param name="random">Whether the enemy is randomly generated.</param>
        /// <param name="name">The name of the enemy.</param>
        /// <param name="power">The power level of the enemy.</param>
        /// <param name="health">The health of the enemy.</param>
        /// <param name="isFinalBoss">Whether the enemy is the final boss.</param>
        public static void RandomEncounter(Player player)
        {
            switch (rand.Next(0, 1))
            {
                case 0:
                    BasicFightEncounter(player);
                    break;
            }
        }

        // Combat Logic
        public static void Combat(Player player, bool random, string name, int power, int health, bool isFinalBoss = false)
        {
            string n = "";
            int p = 0;
            int h = 0;

            if (random)
            {
                n = GetName();
                p = rand.Next(1, 8);
                h = rand.Next(10, 15);
            }
            else
            {
                n = name;
                p = power;
                h = health;
            }

            while (h > 0 && player.Health > 0)
            {
                // Display boss stats
                Console.Clear();
                Console.WriteLine("==========================");
                Console.WriteLine("|       BOSS STATS      |");
                Console.WriteLine("==========================");
                Console.WriteLine("| Name:   " + n.PadRight(15) + " |");
                Console.WriteLine("| Health: " + h.ToString().PadRight(15) + " |");
                Console.WriteLine("| Power:  " + p.ToString().PadRight(15) + " |");
                Console.WriteLine("==========================");

                // Combat Menu
                Console.WriteLine("\n==========================");
                Console.WriteLine("| (A)ttack (D)efend |");
                Console.WriteLine("|  (R)un    (H)eal  |");
                Console.WriteLine("==========================");
                Console.WriteLine("My Potions: " + player.Potion + " My Health: " + player.Health);
                Console.WriteLine("\nChoose an option out of these four!");
                Console.Write("> ");

                string input = Console.ReadLine().ToLower();

                // Handles invalid input
                bool validInput = false;
                while (!validInput)
                {
                    if (input == "a" || input == "d" || input == "r" || input == "h")
                    {
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please choose (A)ttack, (D)efend, (R)un, or (H)eal.");
                        Console.Write("> ");
                        input = Console.ReadLine().ToLower();
                    }
                }

                if (input == "a")
                {
                    // Attack
                    Console.WriteLine("You launch hastily towards the " + n + " striking it, however as you pass by, the " + n + " strikes back.");
                    int damage = p - player.ArmourValue;
                    if (damage < 0)
                        damage = 0;
                    int attack = rand.Next(1, player.WeaponValue + 1) + rand.Next(1, 4);
                    Console.WriteLine("You lose " + damage + " health and deal " + attack + " damage (Press Enter...) ");
                    player.Health -= damage;
                    h -= attack;
                }
                else if (input == "d")
                {
                    // Defend
                    Console.WriteLine("As the " + n + " prepares to strike, you prepare to block");
                    int damage = (p / 4) - player.ArmourValue;
                    if (damage < 0)
                        damage = 0;
                    int attack = (rand.Next(1, player.WeaponValue + 1) / 2) + rand.Next(1, 3);
                    Console.WriteLine("You lose " + damage + " health and deal " + attack + " damage (Press Enter...) ");
                    player.Health -= damage;
                    h -= attack;
                }
                else if (input == "r")
                {
                    // Run
                    if (rand.Next(0, 2) == 0)
                    {
                        Console.WriteLine("You manage to escape the " + n + " without taking any damage (Press Enter).");
                        Console.ReadKey();
                        return; // Exit the encounter (combat)
                    }
                    else
                    {
                        Console.WriteLine("You fail to escape, and the " + n + " strikes you!");
                        int damage = p - player.ArmourValue;
                        if (damage < 0) damage = 0;
                        Console.WriteLine("You lose " + damage + " health (Press Enter...)");
                        player.Health -= damage;
                        Console.ReadKey();
                    }
                }
                else if (input == "h")
                {
                    // Heal
                    if (player.Potion == 0)
                    {
                        Console.WriteLine("As you reach into your bag for a health potion, you can feel nothing in there...");
                        int damage = p - player.ArmourValue;
                        if (damage < 0)
                            damage = 0;
                        Console.WriteLine("You are unable to heal and the " + n + " strikes and you lose " + damage + " health!");
                        player.Health -= damage;
                    }
                    else
                    {
                        Console.WriteLine("You reach into your bag and pull out a health potion, you drink it and feel revitalized.");
                        int potionV = 5;
                        player.Heal(potionV);
                        Console.WriteLine("You gain " + potionV + " health");
                        player.Potion--; // Decrease the number of potions by 1
                        Console.WriteLine("As you were occupied drinking the potion, the " + n + " strikes and you");
                        int damage = (p / 2) - player.ArmourValue;
                        if (damage < 0)
                            damage = 0;
                        Console.WriteLine("You lose " + damage + " health (Press Enter...)");
                        player.Health -= damage;
                    }
                    Console.ReadKey();
                }

                // Check if player has died
                if (player.Health <= 0)
                {
                    Console.WriteLine("\nYou were defeated by the " + n + "... Game Over!");
                    Console.ReadKey();
                    Environment.Exit(0); // End the game
                }
                Console.ReadKey();
            }

            // Enemy Defeat Check
            if (h <= 0)
            {
                int c = rand.Next(10, 50);
                Console.WriteLine("\nYou defeated the " + n + ", its body dissolves into " + c + " gold coins! (Press Enter.)");
                Console.ReadKey();
                player.PickUpItem("" + c + " Gold"); // Reward for defeating the enemy

                // Special reward for defeating the Venom Fang Boss
                if (n == "Venom Fang") // Check if it's the Venom Fang Boss
                {
                    Console.WriteLine("\nAs the Venom Fang Boss falls, you notice a magical staff among its remains!");
                    Console.WriteLine("You pick up the Magical Staff. It hums with arcane energy!");
                    player.PickUpItem("Magical Staff"); // Add Magical Staff to inventory
                }

                // Special reward for defeating the final boss
                if (isFinalBoss)
                {
                    Console.Clear();
                    Console.WriteLine("\nAs the final boss falls, you notice a treasure chest among its remains!");
                    Console.WriteLine("You open the chest and find the legendary treasure!");
                    player.PickUpItem("Legendary Treasure"); // Add the artifact to the player's inventory

                    Console.WriteLine("\nCongratulations! You have completed the game!");
                    Console.WriteLine($"\n{player.Name}'s Inventory: {player.InventoryContents()}"); // Display final inventory
                    Console.WriteLine("Press Enter to exit...");
                    Console.ReadKey();
                    Environment.Exit(0); // End the game
                }

                Console.Clear();
            }
        }

        /// <summary>
        /// Generates a random name for the enemy.
        /// </summary>
        /// <returns>A randomly selected enemy name.</returns>
        public static string GetName()
        {
            string[] names = { "Skeleton Boss", "Zombie Boss", "Human Cultist Boss", "Grave Robber Boss" };
            return names[rand.Next(0, names.Length)];
        }

    }
}
