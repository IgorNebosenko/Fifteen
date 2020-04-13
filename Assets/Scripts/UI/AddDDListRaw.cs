using UnityEngine;
using Game.Additional;
using TMPro;

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
        public string[] ListText;

        void Start()
        {
            TMP_Dropdown dd = gameObject.GetComponent<TMP_Dropdown>();

            for (int i = 0; i < ListText.Length; ++i)
            {
                dd.options.Add(new TMP_Dropdown.OptionData(
                    MultiLang.core.GetText(ListText[i]))) ;
            }
            if (ListText.Length > 1) //<-Sometimes value with id 0 - don't selected!
                dd.value = 1;
            else //Dropdown must have more than 1 value
                Debug.LogWarning("DropDown has less than 2 variants!");
            dd.value = selectedIndex;
        }
    }
}