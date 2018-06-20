using UnityEngine;

namespace DungeonSlasher
{
    /// <summary>
    /// Loads attacks from resources.
    /// </summary>
    public class PlayerLoader : MonoBehaviour, IObjectLoader
    {
        [SerializeField] private string playerName;

        /// <summary>
        /// Load attack from start.
        /// </summary>
        private void Start()
        {
            Load("Players/" + playerName);
        }

        /// <summary>
        /// Load a player from resources.
        /// </summary>
        /// <param name="playerPath">Path to the player resource.</param>
        public void Load(string playerPath)
        {
            GameObject playerPrefab = Resources.Load<GameObject>(playerPath);
            Instantiate(playerPrefab, transform);
        }
    }
}
