using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Game.Additional;

namespace Game.UI
{
    /// <summary>
    /// Class which adds languages to dropdown items
    /// </summary>
    public class MakeDDListLang : MonoBehaviour
    {
        /// <summary>
        /// Index which selects as default
        /// </summary>
        public int selectedIndex = 0;

        private void Start()
        {
            TMP_Dropdown dd = gameObject.GetComponent<TMP_Dropdown>();

            List<string> lstLang = MultiLang.core.GetLangList();

            foreach (string str in lstLang)
                dd.options.Add(new TMP_Dropdown.OptionData(str));

            dd.value = 1;
            dd.value = selectedIndex;
        }
    }
}