using System.Collections;
using UnityEngine;

namespace DungeonSlasher
{
    /// <summary>
    /// Basic player sword attack action.
    /// </summary>
    public class PlayerSwordSlash : PlayerAttack
    {
        /// <summary>
        /// Update when attacking.
        /// </summary>
        protected override void OnAttacking()
        {
            // Get the players movement component
            PlayerMovement movement = player.GetComponent<PlayerMovement>();

            // Check if the player is not idle to set walking animation
            if (movement.IsIdle())
            {
                movement.StartWalk();
                attacking = false;
            }
        }

        /// <summary>
        /// Use the players sword attack.
        /// </summary>
        protected override void Attack()
        {
            // Get the players movement component
            PlayerMovement movement = player.GetComponent<PlayerMovement>();

            // Handle movement and animations
            movement.StopWalk();
            movement.Attack(animationName);

            // Damage the enemy
            StartCoroutine("DamageEnemy");

            // Reset attack status
            cooldown = maxCooldown;
            attacking = true;
        }

        /// <summary>
        /// Damage the enemy if there is one.
        /// </summary>
        private IEnumerator DamageEnemy()
        {
            // Wait for the attack duration to end
            float attackTime = duration;
            while (attackTime > 0)
            {
                attackTime -= Time.deltaTime;

                // Attack the enemy
                if (attackTime <= 0)
                {
                    // Check if attack hit an enemy
                    RaycastHit hit;
                    if (Physics.Raycast(player.transform.position,
                        player.transform.TransformDirection(Vector3.forward), out hit, range))
                    {
                        // Check if attack hit enemy
                        if (hit.transform.tag == "EnemyRaycast")
                        {
                            // Damage the enemy
                            Health enemyHealth = hit.transform.parent.GetComponent<Health>();
                            if (enemyHealth != null)
                            {
                                enemyHealth.TakeDamage(damage);
                            }
                        }
                    }
                }

                // Return nothing to the coroutine
                yield return null;
            }
        }
    }
}
