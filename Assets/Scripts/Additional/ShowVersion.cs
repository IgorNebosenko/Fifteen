using UnityEngine;
using TMPro;
using Game.Exceptions;

namespace Game.Additional
{
    /// <summary>
    /// This class provides getting current version of game, 
    /// and display this with using TextMeshPro
    /// </summary>
    public class ShowVersion : MonoBehaviour
    {
        private void Start()
        {
            TextMeshProUGUI textObj = this.gameObject.GetComponent<TextMeshProUGUI>();
            if (textObj == null)
                throw new CantFindObject("ShowVersion can't find TextMeshProUGUI!");

            textObj.text = string.Concat("v.", Application.version);
        }
    }
}