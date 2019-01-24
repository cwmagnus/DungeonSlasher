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
        [SerializeField] private float destinationRotationSpeed;
        protected NavMeshAgent agent;
        protected Animator animator;
        protected Transform target;
        protected bool stopped;

        /// <summary>
        /// Get components.
        /// </summary>
        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            StartWalk();
        }

        /// <summary>
        /// Update every frame.
        /// </summary>
        private void Update()
        {
            if (GameStateManager.Playing())
            {
                PreUpdateMovement();

                if (target != null)
                {
                    //agent.isStopped = !animator.GetBool("Walk");
                    UpdateMovement();
                }
            }
        }

        /// <summary>
        /// Manually select a target.
        /// </summary>
        protected void ManuallySelectTarget(string targetTag)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == targetTag + "Raycast")
                {
                    target = hit.transform.parent;
                }
            }
        }

        /// <summary>
        /// Stop the agent.
        /// </summary>
        public void Stop()
        {
            agent.isStopped = true;
            //agent.velocity = Vector3.zero;
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
        /// Get the distance away from the destination.
        /// </summary>
        /// <returns>Distance from destination.</returns>
        public float GetDestinationDistance()
        {
            return Vector3.Distance(agent.destination, transform.position);
        }

        /// <summary>
        /// Rotates the entity towards the target over time.
        /// </summary>
        public void RotateTowardsTarget()
        {
            Vector3 direction = target.position - transform.position;
            direction.y = 0;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation,
                Time.deltaTime * destinationRotationSpeed);
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
        /// Check if entity is idle.
        /// </summary>
        /// <returns><c>true</c>, if idle, <c>false</c> otherwise.</returns>
        public bool IsIdle()
        {
            return animator.GetCurrentAnimatorStateInfo(0).IsName("Idle");
        }

        /// <summary>
        /// Check if the entity is dead.
        /// </summary>
        /// <returns><c>true</c>, if dead, <c>false</c> if alive.</returns>
        public bool IsDead()
        {
            return animator.GetCurrentAnimatorStateInfo(0).IsName("Dead");
        }

        /// <summary>
        /// Check if the action is going on.
        /// </summary>
        /// <returns><c>true</c>, if action, <c>false</c> otherwise.</returns>
        /// <param name="actionName">Name of the action.</param>
        public bool IsAction(string actionName)
        {
            return animator.GetCurrentAnimatorStateInfo(0).IsName(actionName);
        }

        /// <summary>
        /// Play the action animation.
        /// </summary>
        /// <param name="actionName">Action to use.</param>
        public void Action(string actionName)
        {
            animator.SetTrigger(actionName);
        }

        /// <summary>
        /// Set the agent's destination.
        /// </summary>
        /// <param name="targetTag">Tag of target to go to.</param>
        public void SetDestination(string targetTag)
        {
            if (target != null && !stopped)
            {
                agent.destination = target.position;
                animator.SetBool("Walk", true);

                // Remove target if they are dead
                if (target.GetComponent<Health>().OutOfHealth())
                {
                    target.tag = "Dead";
                    target = null;
                }
            }
            else if (!stopped)
            {
                target = ScanForTarget(targetTag);
                animator.SetBool("Walk", false);
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
