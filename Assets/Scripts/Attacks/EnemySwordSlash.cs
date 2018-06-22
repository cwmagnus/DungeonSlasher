using System.Collections;
using UnityEngine;

namespace DungeonSlasher
{
    /// <summary>
    /// Basic enemy sword attack action.
    /// </summary>
    public class EnemySwordSlash : EnemyAttack
    {
        /// <summary>
        /// Update when attacking.
        /// </summary>
        protected override void OnAttacking()
        {
            // Get the enemy's movement component
            EnemyMovement movement = transform.parent.GetComponent<EnemyMovement>();

            // Check if the player is not idle to set walking animation
            if (movement.IsIdle())
            {
                movement.StartWalk();
                attacking = false;
            }
        }

        /// <summary>
        /// Use the enemy's sword attack.
        /// </summary>
        protected override void Attack()
        {
            // Get the enemy's movement component
            EnemyMovement movement = transform.parent.GetComponent<EnemyMovement>();

            // Handle movement and animations
            movement.StopWalk();
            movement.Action(animationName);

            // Damage the player
            StartCoroutine("DamageEntity");

            // Reset attack status
            cooldown = maxCooldown;
            attacking = true;
        }

        /// <summary>
        /// Damage the player if there is one.
        /// </summary>
        protected override IEnumerator DamageEntity()
        {
            // Get the player's movement component
            EnemyMovement movement = transform.parent.GetComponent<EnemyMovement>();

            // Wait for the attack duration to end
            float attackTime = duration;
            while (attackTime > 0)
            {
                attackTime -= Time.deltaTime;

                // Attack the player
                if (attackTime <= 0)
                {
                    // Make sure the enemy is still playing the attack animation
                    if (movement.IsAction(animationName))
                    {
                        // Check if attack hit an player
                        RaycastHit hit;
                        if (RaycastTarget("PlayerRaycast", transform.parent.position,
                                          transform.parent.TransformDirection(Vector3.forward),
                                          out hit))
                        {
                            Debug.Log("Test");
                            // Damage the player
                            Health playerHealth = hit.transform.parent.GetComponent<Health>();
                            if (playerHealth != null) playerHealth.TakeDamage(damage);
                        }
                    }
                }

                // Return nothing to the coroutine
                yield return null;
            }
        }
    }
}
