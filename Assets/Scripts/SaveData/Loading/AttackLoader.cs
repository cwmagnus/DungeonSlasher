using UnityEngine;

namespace DungeonSlasher
{
    /// <summary>
    /// Loads attacks from resources.
    /// </summary>
    public class AttackLoader : MonoBehaviour, IObjectLoader
    {
        [SerializeField] private string attackName;
        [SerializeField] private string attackGroup;

        /// <summary>
        /// Load attack from start.
        /// </summary>
        private void Start()
        {
            if (!attackName.Equals("None"))
            {
                Load("Attacks/" + attackGroup + '/' + attackName);
            }
        }

        /// <summary>
        /// Load an attack from resources.
        /// </summary>
        /// <param name="attackPath">Path to the attack resource.</param>
        public void Load(string attackPath)
        {
            GameObject attackPrefab = Resources.Load<GameObject>(attackPath);
            Instantiate(attackPrefab, transform);
        }
    }
}
