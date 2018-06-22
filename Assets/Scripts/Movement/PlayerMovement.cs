using UnityEngine;

namespace DungeonSlasher
{
    /// <summary>
    /// Moves the player using a nav mesh agent.
    /// </summary>
    class PlayerMovement : EntityMovement
    {
        /// <summary>
        /// Update before player movement.
        /// </summary>
        protected override void PreUpdateMovement()
        {
            SetDestination("Enemy");
        }

        /// <summary>
        /// Update player movement.
        /// </summary>
        protected override void UpdateMovement()
        {
            if (InDestinationDistance())
            {
                Stop();

                RotateTowardsTarget();
            }
        }

        /// <summary>
        /// Scan for enemy targets.
        /// </summary>
        /// <returns>Found target.</returns>
        protected override Transform ScanForTarget(string targetName)
        {
            Transform enemyTarget = null;
            float closestDistance = Mathf.Infinity;

            // Find the closest enemy if possible
            foreach (var enemy in GameObject.FindGameObjectsWithTag(targetName))
            {
                Vector3 directionToTarget = enemy.transform.position - transform.position;
                float distanceToTarget = directionToTarget.sqrMagnitude;
                if (distanceToTarget < closestDistance)
                {
                    closestDistance = distanceToTarget;
                    enemyTarget = enemy.transform;

                    // Remove target if they are dead
                    if (enemyTarget.GetComponent<Health>().OutOfHealth())
                    {
                        enemy.tag = "EnemyDead";
                        enemyTarget = null;
                    }
                }
            }

            // Lose target if enemy is out of health
            if (enemyTarget != null)
            {
                if (enemyTarget.GetComponent<Health>().OutOfHealth())
                {
                    enemyTarget = null;
                }
            }

            return enemyTarget;
        }
    }
}
