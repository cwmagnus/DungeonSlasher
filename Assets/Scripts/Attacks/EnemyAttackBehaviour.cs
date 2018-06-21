using UnityEngine;
using SRandom = System.Random;
using URandom = UnityEngine.Random;

namespace DungeonSlasher
{
    /// <summary>
    /// Behaviour for enemy attacks.
    /// </summary>
    [RequireComponent(typeof(EnemyMovement))]
    public class EnemyAttackBehaviour : MonoBehaviour
    {
        [SerializeField] private string attackGroup;
        [SerializeField] private MinMaxValues attackVariance;
        [SerializeField] private float targetVariance;
        private EnemyAttack[] attackResources;
        private EnemyAttack[] attacks;
        private EnemyMovement enemyMovement;

        /// <summary>
        /// Load attack resources.
        /// </summary>
        private void Awake()
        {
            // Get the enemy movement component
            enemyMovement = GetComponent<EnemyMovement>();

            // Load all attacks for this enemy's attack group
            attackResources = Resources.LoadAll<EnemyAttack>("Attacks/" + attackGroup);
            attacks = new EnemyAttack[attackResources.Length];

            // Spawn all attacks
            for (int i = 0; i < attackResources.Length; i++)
            {
                GameObject attackGameObject = Instantiate(attackResources[i].gameObject, transform);
                attacks[i] = attackGameObject.GetComponent<EnemyAttack>();
            }
        }

        /// <summary>
        /// Update enemy attacks.
        /// </summary>
        private void Update()
        {
            // Make sure enemy has attacks
            if (attacks.Length > 0)
            {
                // Attack at random intervals
                float randomInterval = URandom.Range(attackVariance.min, attackVariance.max);
                if (randomInterval > targetVariance)
                {
                    // Choose a random attack to use and attack with it
                    SRandom random = new SRandom();
                    EnemyAttack currentAttack = attacks[random.Next(attacks.Length)];

                    // Attack when in range
                    if (currentAttack.Range >= enemyMovement.GetDestinationDistance())
                    {
                        currentAttack.UseAttack();
                    }
                }
            }
        }
    }
}
