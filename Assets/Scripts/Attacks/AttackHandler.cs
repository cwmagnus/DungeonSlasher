using UnityEngine;
using UnityEngine.UI;

namespace DungeonSlasher
{
    /// <summary>
    /// Handles attack button clicks.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public abstract class AttackHandler : MonoBehaviour
    {
        protected Button attackButton;

        /// <summary>
        /// Bind the attack function to the attack button.
        /// </summary>
        private void Awake()
        {
            attackButton = GetComponent<Button>();
            attackButton.onClick.AddListener(Attack);
        }

        /// <summary>
        /// Attack function called on click.
        /// </summary>
        public abstract void Attack();
    }
}
