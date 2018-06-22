using System.Collections;
using UnityEngine;

namespace DungeonSlasher
{
    /// <summary>
    /// Handle standard entity health events.
    /// </summary>
    public class EntityHealthHandler : HealthHandler
    {
        [SerializeField] private Flash damageFlash;
        [SerializeField] private Flash healFlash;
        private Color originalColor;
        private SkinnedMeshRenderer meshRenderer;

        /// <summary>
        /// Set local variables.
        /// </summary>
        private void Start()
        {
            meshRenderer = transform.GetComponentInChildren<SkinnedMeshRenderer>();
            originalColor = meshRenderer.material.color;
        }

        /// <summary>
        /// Check if the entity is dead.
        /// </summary>
        private void Update()
        {
            // Destroy if dead
            if (movement.IsDead())
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Take damage event.
        /// </summary>
        /// <param name="damage">Damage taken.</param>
        protected override void TakeDamage(float damage)
        {
            StartCoroutine("Flash", damageFlash);
        }

        /// <summary>
        /// Heal damage event.
        /// </summary>
        /// <param name="damage">Damaged healed.</param>
        protected override void HealDamage(float damage)
        {
            StartCoroutine("Flash", healFlash);
        }

        /// <summary>
        /// Out of health event.
        /// </summary>
        protected override void OutOfHealth()
        {
            movement.Action("Die");
        }

        /// <summary>
        /// Flash animation on hit.
        /// </summary>
        /// <returns>Null every cycle.</returns>
        private IEnumerator Flash(Flash flash)
        {
            // How long to flash
            float flashTime = flash.duration;

            // Delay by flash time
            while (flashTime > 0)
            {
                // Flash on hit
                meshRenderer.material.color = flash.color;
                flashTime -= Time.deltaTime;

                yield return null;
            }

            // Revert to original color
            meshRenderer.material.color = originalColor;
            movement.StartWalk();
        }
    }
}
