using UnityEngine;

namespace DungeonSlasher
{
    /// <summary>
    /// Handle player health events.
    /// </summary>
    public class PlayerHealthHandler : HealthHandler
    {
        /// <summary>
        /// Take damage event.
        /// </summary>
        /// <param name="damage">Damage taken.</param>
        protected override void TakeDamage(float damage)
        {
            Debug.Log(damage);
        }

        /// <summary>
        /// Heal damage event.
        /// </summary>
        /// <param name="damage">Damaged healed.</param>
        protected override void HealDamage(float damage)
        {

        }

        /// <summary>
        /// Out of health event.
        /// </summary>
        protected override void OutOfHealth()
        {

        }
    }
}
