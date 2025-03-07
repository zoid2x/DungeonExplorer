using System;
using System.Collections.Generic;
using System.Linq;
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
            Console.WriteLine("This huge 3 Headed Monster is blocking your exit");
            Console.WriteLine("He turns...");
            Console.ReadKey();
            Combat(player, false, "3 Headed Monster", 2, 5);
        }


        // Encounter Tools
        public static void Combat(Player player, bool random, string name, int power, int health)
        {
            string n = "";
            int p = 0;
            int h = 0;

            if (random)
            {

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
                Console.WriteLine("Potions: " + player.InventoryContents() + " Health: " + player.Health);
                string input = Console.ReadLine().ToLower();


                if (input == "a")
                {
                    // Attack
                    Console.WriteLine("You launch hastily towards the " + n + " striking it, however as you pass by, the " + n + " strikes back.");
                    int damage = p - player.ArmourValue;
                    if (damage < 0)
                        damage = 0;
                    int attack = rand.Next(0, player.WeaponValue) + rand.Next(1, 4);
                    Console.WriteLine("You lose " + damage + " health and deal " + attack + "damage");
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
                    int attack = rand.Next(0, player.WeaponValue) / 2;
                    Console.WriteLine("You lose " + damage + " health and deal " + attack + "damage");
                    player.Health -= damage;
                    h -= attack;
                }


                else if (input == "r")
                {
                    // Run
                    if (rand.Next(0, 2) == 0)
                    {
                        Console.WriteLine("You manage to escape the " + n + ", however its strike catches you in the back...");
                        int damage = p - player.ArmourValue;
                        if (damage < 0)
                            damage = 0;
                        Console.WriteLine("You lose " + damage + " health and are unable to escape");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("You manage to escape the " + n + " without taking any damage.");
                        Console.ReadKey();
                        return; // Exit the encounter (combat)
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
                        Console.WriteLine("You lose " + damage + " health");
                    }
                    Console.ReadKey();
                }
                Console.ReadKey();
            }

            if (h <= 0)
            {
                Console.WriteLine("You defeated the " + n + "!");
                player.PickUpItem("Gold"); // Reward for defeating the enemy
            }
            else if (player.Health <= 0)
            {
                Console.WriteLine("You were defeated by the " + n + "... Game Over!");
                Environment.Exit(0); // End the game
            }
        }
    }
}
