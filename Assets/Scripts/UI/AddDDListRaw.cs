using UnityEngine;
using TMPro;
using Game.Additional;

namespace Game.UI
{
    /// <summary>
    /// Class which adds to dropdown items. All text values are raw
    /// </summary>
    public class AddDDListRaw : MonoBehaviour
    {
        /// <summary>
        /// Index which selects as default
        /// </summary>
        public int selectedIndex = 0;


        /// <summary>
        /// List of text strings
        /// </summary>
        public string[] listText;

        private void Start()
        {
            TMP_Dropdown dd = gameObject.GetComponent<TMP_Dropdown>();

            foreach (var t in listText)
            {
                dd.options.Add(new TMP_Dropdown.OptionData(
                    MultiLang.core.GetText(t))) ;
            }
            if (listText.Length > 1) //<-Sometimes value with id 0 - don't selected!
                dd.value = 1;
            else //Dropdown must have more than 1 value
                Debug.LogWarning("DropDown has less than 2 variants!");
            dd.value = selectedIndex;
        }
    }
}