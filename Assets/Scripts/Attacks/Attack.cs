using System.Collections;
using UnityEngine;

namespace DungeonSlasher
{
    /// <summary>
    /// Base attack class for all attacks in the game.
    /// </summary>
    public abstract class Attack : MonoBehaviour
    {
        [SerializeField] protected float damage;
        public float Damage { get { return damage; } }
        [SerializeField] protected float range;
        public float Range { get { return range; } }
        [SerializeField] protected float duration;
        public float Duration { get { return duration; } }
        [SerializeField] protected float cooldown;
        public float Cooldown { get { return cooldown; } }
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
        /// Raycasts to a range and detects if it hit a target.
        /// </summary>
        /// <returns><c>true</c>, if the target was found, <c>false</c> otherwise.</returns>
        /// <param name="targetTag">Target tag.</param>
        /// <param name="startPosition">Start position of the raycast.</param>
        /// <param name="direction">Direction to raycast.</param>
        protected bool RaycastTarget(string targetTag, Vector3 startPosition, 
                                     Vector3 direction, out RaycastHit raycastHit)
        {
            // Raycast and check if hit target
            if (Physics.Raycast(startPosition, direction, out raycastHit, range))
            {
                return raycastHit.transform.tag == targetTag;
            }

            return false;
        }

        /// <summary>
        /// Gets called on awake, use this to set up the attack.
        /// </summary>
        protected virtual void OnAwake() { }

        /// <summary>
        /// Use the attack.
        /// </summary>
        public abstract void UseAttack();

        /// <summary>
        /// Damage entity coroutine.
        /// </summary>
        /// <returns>Coroutine yield.</returns>
        protected abstract IEnumerator DamageEntity();
    }
}
