using UnityEngine;

namespace Game.UI
{
    /// <summary>
    /// UI class for exit from game
    /// </summary>
    public class ExitGame : MonoBehaviour
    {
        /// <summary>
        /// Mettod of termination
        /// </summary>
        public void TerminatreGame()
        {
            Application.Quit();
        }
    }
}