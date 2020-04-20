using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using Game.Exceptions;

namespace Game.Additional
{
    public class MultiLang : MonoBehaviour
    {
        /// <summary>
        /// Property which contains name of global object with MultiLangCore
        /// </summary>
        public static MultiLangCore core = null;

        /// <summary>
        /// String with key
        /// </summary>
        public string idText;

        async void Start()
        {
            if (core == null)
            {
                GameObject tmp = GameObject.Find("GlobalObject");
                if (tmp == null)
                    throw new CantFindGlobalObj(
                        "Can't find GO for getting MultiLangCore");
                core = tmp.GetComponent<MultiLangCore>();

            }
            while (!core.IsLoaded)
            {
                await Task.Delay(10);
            }

            core.textFieldsContainer.Add(this);
            SetText();
        }

        /// <summary>
        /// Sets text to curreng game object
        /// </summary>
        public void SetText()
        {
            gameObject.GetComponent<TextMeshProUGUI>().text =
                core.GetText(idText);
        }
    }
}