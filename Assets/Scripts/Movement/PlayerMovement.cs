using UnityEngine;

/// <summary>
/// Moves the player using a nav mesh agent.
/// </summary>
class PlayerMovement : EntityMovement
{
    [SerializeField] private float destinationRotationSpeed;

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

            // Look at target over time
            Vector3 direction = target.position - transform.position;
            direction.y = 0;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 
                Time.deltaTime * destinationRotationSpeed);
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
