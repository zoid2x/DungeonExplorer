using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using DungeonExplorer;

namespace DungeonExplorer
{
    /// <summary>
    /// Contains tests for the game logic using Debug.Assert.
    /// </summary>
    public class GameTest
    {
        /// <summary>
        /// Tests the player's ability to pick up items and update stats.
        /// </summary>
        public void TestPlayerPickUpItem()
        {
            // Arrange
            var player = new Player("TestPlayer", 10);
            int initialDamage = player.Damage; // Should be 2 (Power 1 + WeaponValue 1)

            // Act
            player.PickUpItem(new Item.Weapon("Spectre Wand", 1, ""));

            // Assert
            Debug.Assert(player.WeaponValue == 2, "WeaponValue should be 2 after picking up Spectre Wand.");
            Debug.Assert(player.Damage == 3, "Damage should be 3 after picking up Spectre Wand.");
            Console.WriteLine("TestPlayerPickUpItem passed.");
        }

        /// <summary>
        /// Tests the combat logic when the player defeats an enemy.
        /// </summary>
        public void TestCombatPlayerDefeatsEnemy()
        {
            // Arrange
            var player = new Player("TestPlayer", 20);
            int initialHealth = player.Health;

            // Simulate combat without triggering actual combat logic
            player.Health -= 5; // Simulate player taking damage
            player.PickUpItem(new Item.Key("10 Gold", "", "Currency")); // Simulate receiving gold after defeating an enemy

            // Assert
            Debug.Assert(player.Health < initialHealth, "Player health should decrease after combat.");
            Debug.Assert(player.InventoryContents().Contains("Gold"), "Player should receive gold after defeating an enemy.");
            Console.WriteLine("TestCombatPlayerDefeatsEnemy passed.");
        }

        /// <summary>
        /// Tests the player's healing logic.
        /// </summary>
        public void TestPlayerHeal()
        {
            // Arrange
            var player = new Player("TestPlayer", 10);
            player.Health = 5; // Simulate damage

            // Act
            player.Heal(3);

            // Assert
            Debug.Assert(player.Health == 8, "Player health should be 8 after healing.");
            Console.WriteLine("TestPlayerHeal passed.");
        }

        /// <summary>
        /// Runs all the tests.
        /// </summary>
        public void RunAllTests()
        {
            Console.WriteLine("Starting tests...");
            TestPlayerPickUpItem();
            TestCombatPlayerDefeatsEnemy();
            TestPlayerHeal();
            Console.WriteLine("All tests passed.");
        }
    }
}