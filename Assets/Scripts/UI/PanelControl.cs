using UnityEngine;

namespace DungeonSlasher
{
    /// <summary>
    /// Controls panel UI elements.
    /// </summary>
    public class PanelControl : MonoBehaviour
    {
        private GameObject[] panels;

        /// <summary>
        /// Get the panels that belong to this canvas.
        /// </summary>
        private void Start()
        {
            // Get children panels
            panels = new GameObject[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                panels[i] = transform.GetChild(i).gameObject;
            }
        }

        /// <summary>
        /// Get the specified panel by name.
        /// </summary>
        /// <returns>The panel with that name.</returns>
        /// <param name="name">Name of the panel to find.</param>
        public GameObject GetPanel(string name)
        {
            foreach (GameObject panel in panels)
            {
                if (panel.name == name) return panel;
            }
            return null;
        }

        /// <summary>
        /// Play the game.
        /// </summary>
        public void Play()
        {
            GameStateManager.SetState(GameState.PLAYING);
            GetPanel("PlayPanel").SetActive(false);
            GetPanel("AttackPanel").SetActive(true);
            GetPanel("TopPanel").SetActive(true);
        }
    }
}
