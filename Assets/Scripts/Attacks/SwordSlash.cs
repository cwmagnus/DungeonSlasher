using System.Collections;
using UnityEngine;

/// <summary>
/// Basic sword attack action.
/// </summary>
public class SwordSlash : AttackHandler
{
    [SerializeField] private string attackAnimationName;
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private float attackDuration;
    [SerializeField] private float cooldownTime;
    private float cooldown;
    private GameObject player;
    private PlayerMovement playerMovement;
    private bool attacking = false;
    private bool foundPlayer = false;

    /// <summary>
    /// Decrease cooldown over time.
    /// </summary>
    private void Update()
    {
        if (foundPlayer)
        {
            cooldown -= Time.deltaTime;

            // Do stuff every frame when attacking
            if (attacking)
            {
                // Check if the player is not idle to set walking animation
                if (playerMovement.IsIdle())
                {
                    playerMovement.StartWalk();
                    attacking = false;
                }
            }
        }
        else
        {
            FindPlayer("Player");
        }
    }

    /// <summary>
    /// Use the players sword attack.
    /// </summary>
    public override void Attack()
    {
        if (foundPlayer)
        {
            if (cooldown <= 0)
            {
                playerMovement.StopWalk();
                playerMovement.Attack(attackAnimationName);
                cooldown = cooldownTime;
                attacking = true;
                StartCoroutine("DamageEnemy");
            }
        }
    }

    /// <summary>
    /// Damage the enemy if there is one.
    /// </summary>
    private IEnumerator DamageEnemy()
    {
        float attackTime = attackDuration;
        while (attackTime > 0)
        {
            attackTime -= Time.deltaTime;

            // Raycast to enemy
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

            yield return null;
        }
    }

    /// <summary>
    /// Find the player and it's components.
    /// </summary>
    /// <param name="playerTag">Player tag to find.</param>
    private void FindPlayer(string playerTag)
    {
        player = GameObject.FindGameObjectWithTag(playerTag);
        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
            foundPlayer = true;
        }
        else
        {
            foundPlayer = false;
        }
    }
}
