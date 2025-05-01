using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonExplorer;

namespace DungeonExplorer
{
    /// <summary>
    /// Interface for objects that can take damage.
    /// </summary>
    public interface IDamageable
    {
        /// <summary>
        /// Applies damage to the object.
        /// </summary>
        /// <param name="amount">Amount of damage to apply.</param>
        void TakeDamage(int amount);
    }

    /// <summary>
    /// Interface for objects that can be collected.
    /// </summary>
    public interface ICollectible
    {
        /// <summary>
        /// Called when the object is collected.
        /// </summary>
        /// <param name="collector">The creature that collected this object.</param>
        void OnCollect(Creature collector);
    }
}
