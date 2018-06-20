using UnityEngine;

namespace DungeonSlasher
{
    /// <summary>
    /// Move the enemy using nav mesh agent.
    /// </summary>
    [RequireComponent(typeof(Health))]
    public class EnemyMovement : EntityMovement
    {
        private Health health;

        /// <summary>
        /// Get components.
        /// </summary>
        private void Start()
        {
            health = GetComponent<Health>();
        }

        /// <summary>
        /// Update before enemy movement.
        /// </summary>
        protected override void PreUpdateMovement()
        {
            SetDestination("Player");
        }

        /// <summary>
        /// Find the player target.
        /// </summary>
        /// <param name="targetName">Name of the target.</param>
        /// <returns>Found target.</returns>
        protected override Transform ScanForTarget(string targetName)
        {
            Transform playerTarget = GameObject.FindGameObjectWithTag(targetName).transform;
            return playerTarget;
        }

        /// <summary>
        /// Update the enemy movement.
        /// </summary>
        protected override void UpdateMovement()
        {
            if (InDestinationDistance() || health.OutOfHealth())
            {
                Stop();
            }
        }
    }
}
