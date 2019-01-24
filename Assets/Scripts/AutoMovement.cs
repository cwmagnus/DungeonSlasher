using UnityEngine;
using UnityEngine.AI;

namespace DungeonSlasher
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AutoMovement : MonoBehaviour
    {
        private NavMeshAgent agent;
        private Transform target;

        public bool Stopped { get; set; }
        public bool HasTarget { get { return target != null; } }

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {

        }
    }
}
