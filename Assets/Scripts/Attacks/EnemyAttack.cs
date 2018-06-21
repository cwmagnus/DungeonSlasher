using UnityEngine;

namespace DungeonSlasher
{
    /// <summary>
    /// Handles enemy attacks.
    /// </summary>
    public abstract class EnemyAttack : Attack
    {
        /// <summary>
        /// Update the attack every frame.
        /// </summary>
        private void Update()
        {
            OnUpdate();

            if (attacking) OnAttacking();

            cooldown -= Time.deltaTime;
        }

        /// <summary>
        /// Use the attack.
        /// </summary>
        public override void UseAttack()
        {
            if (cooldown <= 0) Attack();
        }

        /// <summary>
        /// Called when the player attacks.
        /// </summary>
        protected abstract void Attack();

        /// <summary>
        /// Called after the player is found every frame.
        /// </summary>
        protected virtual void OnUpdate() { }

        /// <summary>
        /// Called every frame when attacking.
        /// </summary>
        protected virtual void OnAttacking() { }
    }
}
