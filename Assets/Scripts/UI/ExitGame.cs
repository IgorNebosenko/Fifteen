using UnityEngine;

namespace Game.UI
{
    /// <summary>
    /// UI class for exit from game
    /// </summary>
    public class ExitGame : MonoBehaviour
    {
        /// <summary>
        /// Method of termination
        /// </summary>
        public void TerminateGame()
        {
            Application.Quit();
        }
    }
}