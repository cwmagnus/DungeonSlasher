using UnityEngine;

namespace DungeonSlasher
{
    [RequireComponent(typeof(Animator)), RequireComponent(typeof(AutoMovement))]
    public class MovementController : MonoBehaviour
    {
        private Animator animator;
        private AutoMovement movement;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            movement = GetComponent<AutoMovement>();
        }

        private void LateUpdate()
        {
            if (movement.HasTarget)
            {
                movement.Stopped = !animator.GetBool("Walk");
            }
        }

        public void Stop()
        {
            movement.Stopped = true;
            animator.SetBool("Walk", false);
        }

        public void Walk()
        {
            movement.Stopped = false;
            animator.SetBool("Walk", true);
        }

        public bool IsAnimationPlaying(string animationName)
        {
            return animator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
        }

        public void TriggerAnimation(string animationName)
        {
            animator.SetTrigger(animationName);
        }
    }
}
