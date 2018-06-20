using UnityEngine;
using UnityEngine.UI;

namespace DungeonSlasher
{
    /// <summary>
    /// Handles attack button clicks for the player.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public abstract class PlayerAttack : Attack
    {
        [SerializeField] protected string playerTag;
        protected Button attackButton;
        protected GameObject player;
        protected bool foundPlayer;

        /// <summary>
        /// Bind the attack function to the attack button.
        /// </summary>
        protected override void OnAwake()
        {
            attackButton = GetComponent<Button>();
            attackButton.onClick.AddListener(UseAttack);
        }

        /// <summary>
        /// Update the attack every frame.
        /// </summary>
        private void Update()
        {
            if (foundPlayer)
            {
                OnUpdate();

                if (attacking) OnAttacking();

                cooldown -= Time.deltaTime;
            }
            else FindPlayer(playerTag);
        }

        /// <summary>
        /// Finds the player.
        /// </summary>
        /// <param name="playerTag">Player tag to find.</param>
        protected void FindPlayer(string playerTag)
        {
            player = GameObject.FindGameObjectWithTag(playerTag);
            foundPlayer = player != null;
        }

        /// <summary>
        /// Use the attack.
        /// </summary>
        protected override void UseAttack()
        {
            if (foundPlayer)
            {
                if (cooldown <= 0) Attack();
            }
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
