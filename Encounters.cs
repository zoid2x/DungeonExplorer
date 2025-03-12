using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    public class Encounters
    {
        static Random rand = new Random(); // Random number generator
        
        
        // Encounters 
        public static void FirstEncounter(Player player)
        {
            Console.WriteLine("\nThis huge 3 Headed Monster is blocking your exit");
            Console.WriteLine("He turns... (Press ENTER)");
            Console.ReadKey();
            Combat(player, false, "3 Headed Monster", 2, 5);
        }
        
        
        public static void BasicFightEncounter (Player player)
        {
            Console.Clear();
            Console.WriteLine("Ahead of you is a huge shadow...");
            Console.WriteLine("You enter into the final battle... Will you prevail? (Press ENTER)");
            Console.ReadKey();
            Combat(player, true, "", 0, 0);
        }


        public static void SecondEncounter(Player player)
        {
            Console.WriteLine("\nOut of nowhere this 7 foot Spider Drops down from the roof");
            Console.WriteLine("His fangs drip with venom as it stares at you with murderous eyes... (Press ENTER)");
            Console.ReadKey();
            Combat(player, false, "Big ahh Spider", 4, 7);
        }



        // Encounter Tools
        public static void RandomEncounter(Player player)
        {
            switch (rand.Next(0, 1))
            {
                case 0:
                    BasicFightEncounter(player);
                    break;
            }
        }

        public static void Combat(Player player, bool random, string name, int power, int health)
        {
            string n = "";
            int p = 0;
            int h = 0;

            if (random)
            {
                n = GetName();
                p = rand.Next(1, 8);
                h = rand.Next(5, 20);
            }
            else
            {
                n = name;
                p = power;
                h = health;
            }
            while (h > 0)
            {
                Console.Clear();
                Console.WriteLine(n);
                Console.WriteLine(p + "/" + h);
                Console.WriteLine("==========================");
                Console.WriteLine("| (A)ttack (D)efend |");
                Console.WriteLine("|  (R)un    (H)eal  |");
                Console.WriteLine("==========================");
                Console.WriteLine("My Potions: " + player.InventoryContents() + " My Health: " + player.Health);
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
                        Console.WriteLine("You are unable to heal and the " + n + " strikes and you lose " + damage + " health!" );

                    }
                    else
                    {
                        Console.WriteLine("You reach into your bag and pull out a health potion, you drink it and feel revitalized.");
                        int potionV = 5;
                        Console.WriteLine("You gain " + potionV + " health");
                        player.Health += potionV;
                        Console.WriteLine("As you were occupied drinking the potion, the " + n + " strikes and you");
                        int damage = (p / 2) - player.ArmourValue;
                        if (damage < 0)
                            damage = 0;
                        Console.WriteLine("You lose " + damage + " health (Press Enter...)");
                    }
                    Console.ReadKey();
                }
                Console.ReadKey();
            }

            if (h <= 0)
            {
                int c = rand.Next(10, 50);
                Console.WriteLine("\nYou defeated the "+ n +", its body dissolves into "+ c +" gold coins! (Press Enter.)");
                Console.ReadKey();
                player.PickUpItem(""+ c +" Gold"); // Reward for defeating the enemy
                Console.Clear();
            }
            else if (player.Health <= 0)
            {
                // Death Code
                Console.WriteLine("\nYou were defeated by the " + n + "... Game Over!");
                Console.ReadKey();
                Environment.Exit(0); // End the game
            }
        }
        public static string GetName()
        {
            string[] names = { "Skeleton Boss", "Zombie Boss", "Human Cultist Boss", "Grave Robber Boss" };
            return names[rand.Next(0, names.Length)];
        }

    }
}
