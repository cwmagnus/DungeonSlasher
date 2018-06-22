using UnityEngine;

namespace DungeonSlasher
{
    /// <summary>
    /// Handle health events.
    /// </summary>
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(EntityMovement))]
    public abstract class HealthHandler : MonoBehaviour
    {
        protected Health health;
        protected EntityMovement movement;

        /// <summary>
        /// Get components.
        /// </summary>
        protected void Awake()
        {
            // Get and bind functions to the health component
            health = GetComponent<Health>();
            health.OnDamage += TakeDamage;
            health.OnHealDamage += HealDamage;
            health.OnOutOfHealth += OutOfHealth;

            movement = GetComponent<EntityMovement>();
        }

        /// <summary>
        /// Take damage event.
        /// </summary>
        /// <param name="damage">Damage taken.</param>
        protected abstract void TakeDamage(float damage);

        /// <summary>
        /// Heal damage event.
        /// </summary>
        /// <param name="damage">Damage healed.</param>
        protected abstract void HealDamage(float damage);

        /// <summary>
        /// Out of health event.
        /// </summary>
        protected abstract void OutOfHealth();
    }
}
