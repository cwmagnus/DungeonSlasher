using UnityEngine;
using UnityEngine.AI;

namespace DungeonSlasher
{
    /// <summary>
    /// Base class for movement.
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]
    public abstract class EntityMovement : MonoBehaviour
    {
        [SerializeField] protected float destinationDistanceBuffer;
        protected NavMeshAgent agent;
        protected Animator animator;
        protected Transform target;
        protected bool stopped = false;

        /// <summary>
        /// Get components.
        /// </summary>
        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        /// <summary>
        /// Update every frame.
        /// </summary>
        private void Update()
        {
            PreUpdateMovement();

            if (target != null)
            {
                agent.isStopped = !animator.GetBool("Walk");
                UpdateMovement();
            }
        }

        /// <summary>
        /// Stop the agent.
        /// </summary>
        public void Stop()
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            animator.SetBool("Walk", false);
        }

        /// <summary>
        /// Check if the entity is in the destination distance.
        /// </summary>
        /// <returns>Entity in destination distance.</returns>
        public bool InDestinationDistance()
        {
            float destinationDistance = Vector3.Distance(transform.position,
                agent.destination);
            return destinationDistance < destinationDistanceBuffer;
        }

        /// <summary>
        /// External stop function.
        /// </summary>
        public void StopWalk()
        {
            animator.SetBool("Walk", false);
            stopped = true;
        }

        /// <summary>
        /// External go function.
        /// </summary>
        public void StartWalk()
        {
            animator.SetBool("Walk", true);
            stopped = false;
        }

        /// <summary>
        /// Check if the player is id
        /// </summary>
        /// <returns>State of idle.</returns>
        public bool IsIdle()
        {
            return animator.GetCurrentAnimatorStateInfo(0).IsName("Idle");
        }

        /// <summary>
        /// Player the attack animation.
        /// </summary>
        /// <param name="attackName">Attack to use.</param>
        public void Attack(string attackName)
        {
            animator.SetTrigger(attackName);
        }

        /// <summary>
        /// Set the agent's destination.
        /// </summary>
        /// <param name="targetTag">Tag of target to go to.</param>
        public void SetDestination(string targetTag)
        {
            target = ScanForTarget(targetTag);
            if (target != null && !stopped)
            {
                agent.destination = target.position;
                animator.SetBool("Walk", true);
            }
            else
            {
                animator.SetBool("Walk", false);
            }
        }

        /// <summary>
        /// Update the movement every frame.
        /// </summary>
        protected abstract void UpdateMovement();

        /// <summary>
        /// Update before movement.
        /// </summary>
        protected abstract void PreUpdateMovement();

        /// <summary>
        /// Scane for the destination target.
        /// </summary>
        /// <param name="targetName">Name of the target to find.</param>
        /// <returns>Target transform.</returns>
        protected abstract Transform ScanForTarget(string targetName);
    }
}
