using UnityEngine;
using UnityEngine.UI;

namespace DungeonSlasher
{
    /// <summary>
    /// Handles setting the health bar's value to the specified health component.
    /// </summary>
    [RequireComponent(typeof(Slider))]
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private string targetTag;
        [SerializeField] private Health health;
        [SerializeField] private float slideTime;
        private Slider healthBar;

        /// <summary>
        /// Set the health component.
        /// </summary>
        public Health Health { set { health = value; } }

        /// <summary>
        /// Get components.
        /// </summary>
        private void Awake()
        {
            healthBar = GetComponent<Slider>();

            // If there is target then find the health component
            if (targetTag != string.Empty)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                health = player.GetComponent<Health>();
            }
        }

        /// <summary>
        /// Set the health bar value.
        /// </summary>
        private void Update()
        {
            if (health != null)
            {
                // Slide down over time
                healthBar.value = Mathf.Lerp(healthBar.value, 
                    health.PercentHealth(), Time.deltaTime * slideTime);
            }
        }
    }
}
