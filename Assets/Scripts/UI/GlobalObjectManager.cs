using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Game.IO;
using Game.Interfaces;
using Game.Exceptions;

namespace Game.UI
{
    /// <summary>
    /// Manager of global objects. Wait while all objects loads, and then - show UI
    /// </summary>
    public class GlobalObjectManager : MonoBehaviour
    {
        /// <summary>
        /// List of global objects which loads by script
        /// </summary>
        private List<IGlobalObject> globalObjects;
        /// <summary>
        /// Defines is this manager initialized
        /// </summary>
        private bool isInitialized = false;

        /// <summary>
        /// GameObject of UI main window
        /// </summary>
        public GameObject mainWindow;
        /// <summary>
        /// GameObject of UI settings
        /// </summary>
        public GameObject settings;
        /// <summary>
        /// GameObject of UI first start screen
        /// </summary>
        public GameObject firstStartScreen;


        private void Start()
        {
            globalObjects = new List<IGlobalObject>();
            globalObjects.AddRange(gameObject.GetComponents<IGlobalObject>());
        }

        private async void Update()
        {
            if (isInitialized)
            {
                await Task.Delay(100);
                return;
            }
            foreach (IGlobalObject i in globalObjects)
            {
                if (!i.IsLoaded)
                {
                    isInitialized = false;
                    break;
                }
                else
                    isInitialized = true;
            }
            if (isInitialized)
            {
                Initialize();
            }
            await Task.Delay(10);

        }

        private void Initialize()
        {
            SettingsCore sc = gameObject.GetComponent<SettingsCore>();
            if (sc.ShowMessageAboutCust)
            {
                if (settings == null)
                    throw new CantFindUIElement("Can't find Settings UI!");
                settings.SetActive(true);
                if (firstStartScreen == null)
                    throw new CantFindUIElement("Can't find first start screen on UI");
                firstStartScreen.SetActive(true);
            }
            else
            {
                if (mainWindow == null)
                    throw new CantFindUIElement("Can't find Main Window UI");
                mainWindow.SetActive(true);
            }
        }
    }
}