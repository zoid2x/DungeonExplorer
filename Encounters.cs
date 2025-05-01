using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using DungeonExplorer;

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
            var monster = new Goblin("Goblin Warrior", power: 5, maxHealth: 8, goldReward: 10);
            Console.WriteLine($"\nA nasty {monster.Name} blocks your path!");
            Console.WriteLine("He turns... (Press ENTER)");
            Console.ReadKey();
            Combat(player, monster);
        }

        public static void DragonEncounter(Player player)
        {
            var monster = new Dragon("Ancient Red Dragon", power: 15, maxHealth: 50, goldReward: 100);
            Console.WriteLine($"\nThe ground shakes as {monster.Name} appears!");
            Combat(player, monster);
        }


        /// <summary>
        /// Triggers a basic fight encounter with a random enemy. (Used in the final boss encounter)
        /// </summary>
        /// <param name="player">The player object participating in the encounter.</param>
        public static void BasicFightEncounter(Player player)
        {
            var finalBoss = new Monster("Ancient Dragon", power: 10, maxHealth: 20, goldReward: 100);
            Console.Clear();
            Console.WriteLine("Ahead of you is a huge shadow...");
            Console.WriteLine("You enter into the final battle... Will you prevail? (Press ENTER)");
            Console.ReadKey();
            Combat(player, finalBoss, true);
        }

        /// <summary>
        /// Triggers the second encounter with a giant spider.
        /// </summary>
        /// <param name="player">The player object participating in the encounter.</param>
        public static void SecondEncounter(Player player)
        {
            var monster = new Monster("Giant Spider", power: 3, maxHealth: 8, goldReward: 15);
            Console.WriteLine("\nOut of nowhere this 7 foot Spider Drops down from the roof");
            Console.WriteLine("His fangs drip with venom as it stares at you with murderous eyes... (Press ENTER)");
            Console.ReadKey();
            Combat(player, monster);
        }

        /// <summary>
        /// Triggers the third encounter with a venomous creature.
        /// </summary>
        /// <param name="player">The player object participating in the encounter.</param>
        public static void ThirdEncounter(Player player)
        {
            var monster = new Monster("Venom Fang", power: 3, maxHealth: 10, goldReward: 30);
            Console.Clear();
            Console.WriteLine("As you enter the room, a powerful Venom Fang emerges from the floor!");
            Console.WriteLine("Its eyes glow with dark green venom as it prepares to strike... (Press ENTER)");
            Console.ReadKey();
            Combat(player, monster);
        }

        // Encounter Tools

        /// <summary>
        /// Handles the combat logic between the player and an enemy.
        /// </summary>
        /// <param name="player">The player object.</param>
        /// <param name="monster">The monster object.</param>
        /// <param name="isFinalBoss">Whether the enemy is the final boss.</param>
        public static void Combat(Player player, Monster monster, bool isFinalBoss = false)
        {
            while (monster.IsAlive && player.IsAlive)
            {
                // Display boss stats
                Console.Clear();
                Console.WriteLine("==========================");
                Console.WriteLine("|       BOSS STATS      |");
                Console.WriteLine("==========================");
                Console.WriteLine("| Name:   " + monster.Name.PadRight(15) + " |");
                Console.WriteLine("| Health: " + monster.Health.ToString().PadRight(15) + " |");
                Console.WriteLine("| Power:  " + monster.Power.ToString().PadRight(15) + " |");
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
                    Console.WriteLine("You launch hastily towards the " + monster.Name + " striking it, however as you pass by, the " + monster.Name + " strikes back.");
                    int damage = monster.Power - player.ArmourValue;
                    if (damage < 0)
                        damage = 0;
                    int playerAttack = rand.Next(1, player.WeaponValue + 1) + rand.Next(1, 4);
                    int monsterDamage = monster.Attack(player);
                    Console.WriteLine($"You deal {playerAttack} damage and take {monsterDamage} damage!");
                    player.TakeDamage(damage);
                    monster.TakeDamage(playerAttack);
                    Console.ReadKey();
                }
                else if (input == "d")
                {
                    // Defend
                    Console.WriteLine("As the " + monster.Name + " prepares to strike, you prepare to block");
                    int damage = (monster.Power / 4) - player.ArmourValue;
                    if (damage < 0)
                        damage = 0;
                    int attack = (rand.Next(1, player.WeaponValue + 1) / 2) + rand.Next(1, 3);
                    Console.WriteLine("You lose " + damage + " health and deal " + attack + " damage (Press Enter...) ");
                    player.TakeDamage(damage);
                    monster.TakeDamage(attack);
                    Console.ReadKey();
                }
                else if (input == "r")
                {
                    // Run
                    if (rand.Next(0, 2) == 0)
                    {
                        Console.WriteLine("You manage to escape the " + monster.Name + " without taking any damage (Press Enter).");
                        Console.ReadKey();
                        return; // Exit the encounter (combat)
                    }
                    else
                    {
                        Console.WriteLine("You fail to escape, and the " + monster.Name + " strikes you!");
                        int damage = monster.Power - player.ArmourValue;
                        if (damage < 0) damage = 0;
                        Console.WriteLine("You lose " + damage + " health (Press Enter...)");
                        player.TakeDamage(damage);
                        Console.ReadKey();
                    }
                }
                else if (input == "h")
                {
                    // Heal
                    if (player.Potion == 0)
                    {
                        Console.WriteLine("As you reach into your bag for a health potion, you can feel nothing in there...");
                        int damage = monster.Power - player.ArmourValue;
                        if (damage < 0)
                            damage = 0;
                        Console.WriteLine("You are unable to heal and the " + monster.Name + " strikes and you lose " + damage + " health!");
                        player.TakeDamage(damage);
                    }
                    else
                    {
                        Console.WriteLine("You reach into your bag and pull out a health potion, you drink it and feel revitalized.");
                        int potionV = 5;
                        player.Heal(potionV);
                        Console.WriteLine("You gain " + potionV + " health");
                        player.Potion--; // Decrease the number of potions by 1
                        Console.WriteLine("As you were occupied drinking the potion, the " + monster.Name + " strikes and you");
                        int damage = (monster.Power / 2) - player.ArmourValue;
                        if (damage < 0)
                            damage = 0;
                        Console.WriteLine("You lose " + damage + " health (Press Enter...)");
                        player.TakeDamage(damage);
                    }
                    Console.ReadKey();
                }

                // Check if player has died
                if (player.Health <= 0)
                {
                    Console.WriteLine("\nYou were defeated by the " + monster.Name + "... Game Over!");
                    Console.ReadKey();
                    Environment.Exit(0); // End the game
                }
            }

            // Enemy Defeat Check
            if (monster.Health <= 0)
            {
                Console.WriteLine("\nYou defeated the " + monster.Name + ", its body dissolves into " + monster.GoldReward + " gold coins! (Press Enter.)");
                Console.ReadKey();
                player.PickUpItem(new Item.Key($"{monster.GoldReward} Gold", "", "Shiny coins"));

                // Special reward for defeating the Venom Fang Boss
                if (monster.Name == "Venom Fang")
                {
                    Console.WriteLine("\nAs the Venom Fang Boss falls, you notice a magical staff among its remains!");
                    Console.WriteLine("You pick up the Magical Staff. It hums with arcane energy!");
                    player.PickUpItem(new Item.Weapon("Magical Staff", 3, "Hums with arcane energy"));
                }

                // Special reward for defeating the final boss
                if (isFinalBoss)
                {
                    Console.Clear();
                    Console.WriteLine("\nAs the final boss falls, you notice a treasure chest among its remains!");
                    Console.WriteLine("You open the chest and find the legendary treasure!");
                    player.PickUpItem(new Item.Key("Legendary Treasure", "", "The ultimate prize"));

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

        /// <summary>
        /// Triggers a random encounter.
        /// </summary>
        /// <param name="player">The player object.</param>
        public static void RandomEncounter(Player player)
        {
            switch (rand.Next(0, 1))
            {
                case 0:
                    BasicFightEncounter(player);
                    break;
            }
        }
    }
}