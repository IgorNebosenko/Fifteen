using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    /// <summary>
    /// Modificated toggle group for selected group of toggles
    /// </summary>
    public class ToggleSelect : MonoBehaviour
    {
        /// <summary>
        /// Toggle group
        /// </summary>
        public Toggle[] group;

        /// <summary>
        /// Last index
        /// </summary>
        int lastIndex = -1;

        void Start()
        {
            if (group == null || group.Length == 0)
            {
                Debug.LogError("Array of ToggleSelect is empty!");
                return;
            }
            if (group.Length == 1)
            {
                Debug.LogWarning("Array of ToggleSelect has 1 element!");
            }

            int groupLength = group.Length;
            for (int i = 0; i < groupLength; ++i)
            { 
                if(group[i].isOn)
                {
                    if (lastIndex == -1) //if it first element
                        lastIndex = i;
                    else //if this not first element - isOn = false
                        group[i].isOn = false;
                }
            }
            //if indexSelected == -1 - here's not selected toggles. Select first element as default
            if (lastIndex == -1)
            {
                group[0].isOn = true;
                lastIndex = 0;
            }

        }

        void Update()
        {
            if (group.Length == 0)
                return;

            if (!group[lastIndex].isOn) //if pressed on selected item
            {
                group[lastIndex].isOn = true;
                return;
            }

            int groupLength = group.Length;
            for (int i = 0; i < groupLength; ++i)
            {
                if (i == lastIndex)
                    continue;

                if (group[i].isOn)
                {
                        group[lastIndex].isOn = false;
                        lastIndex = i;
                        group[lastIndex].isOn = true;
                        return;
                }
            }
        }
    }
}