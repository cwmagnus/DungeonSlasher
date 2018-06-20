using UnityEngine;

namespace DungeonSlasher
{
    /// <summary>
    /// Base attack class for all attacks in the game.
    /// </summary>
    public abstract class Attack : MonoBehaviour
    {
        [Header("Attack Properties")]
        [SerializeField, Tooltip("Amount of damage the attack will do.")] 
        protected float damage;
        [SerializeField, Tooltip("Attack range.")] 
        protected float range;
        [SerializeField, Tooltip("How long the attack lasts.")]
        protected float duration;
        [SerializeField, Tooltip("How often the attack can be used.")] 
        protected float cooldown;
        protected float maxCooldown;
        [SerializeField, Tooltip("Name of the attack animation to play.")]
        protected string animationName;
        protected bool attacking;

        /// <summary>
        /// Set default variables.
        /// </summary>
        private void Awake()
        {
            maxCooldown = cooldown;
            OnAwake();
        }

        /// <summary>
        /// Gets called on awake, use this to set up the attack.
        /// </summary>
        protected virtual void OnAwake() { }

        /// <summary>
        /// Use the attack.
        /// </summary>
        protected abstract void UseAttack();
    }
}
