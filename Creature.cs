using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonExplorer
{
    /// <summary>
    /// Abstract base class for all living entities in the game.
    /// Contains common properties and methods for Player and Monster.
    /// </summary>
    public abstract class Creature : IDamageable
    {
        /// <summary>
        /// Gets or sets the creature's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the creature's current health.
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// Gets or sets the creature's maximum health.
        /// </summary>
        public int MaxHealth { get; set; }

        /// <summary>
        /// Gets or sets the creature's attack power.
        /// </summary>
        public int Power { get; protected set; }

        /// <summary>
        /// Heals the creature by specified amount without exceeding MaxHealth.
        /// </summary>
        /// <param name="amount">Health points to restore</param>
        public virtual void Heal(int amount)
        {
            Health = Math.Min(Health + amount, MaxHealth);
            Console.WriteLine($"{Name} healed for {amount} HP!");
        }

        /// <summary>
        /// Reduces creature's health by specified damage amount.
        /// Implementation of IDamageable.TakeDamage.
        /// </summary>
        /// <param name="amount">Damage points to take</param>
        public virtual void TakeDamage(int amount)
        {
            Health = Math.Max(Health - amount, 0);
            if (!IsAlive) OnDeath();
        }

        protected virtual void OnDeath()
        {
            Console.WriteLine($"{Name} has been defeated!");
        }

        /// <summary>
        /// Checks if the creature is alive.
        /// </summary>
        public bool IsAlive => Health > 0;
    }
}
