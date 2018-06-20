using UnityEngine;

namespace DungeonSlasher
{
    /// <summary>
    /// Base attack class for all attacks in the game.
    /// </summary>
    public abstract class Attack : MonoBehaviour
    {
        [SerializeField] protected float damage;
        [SerializeField] protected float range;
        [SerializeField] protected float duration;
        [SerializeField] protected float cooldown;
        protected float maxCooldown;
        [SerializeField] protected string animationName;
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
