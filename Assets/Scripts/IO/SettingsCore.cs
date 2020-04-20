using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using System.Runtime.Serialization.Json;
using Game.Interfaces;
using Game.Additional;
using Game.Exceptions;
using static Game.Additional.MultiLangCore;

namespace Game.IO
{
    /// <summary>
    /// Core of settings
    /// </summary>
    public class SettingsCore : MonoBehaviour, IGlobalObject
    {
        /// <summary>
        /// Class which containts settings data
        /// </summary>
        [Serializable]
        public class SettingsContainer
        {
            /// <summary>
            /// String with versions
            /// </summary>
            public string version;

            /// <summary>
            /// Size of game field. As default - 4x4
            /// </summary>
            public ESize size = ESize._4x4;
            /// <summary>
            /// Difficult of game. As default - Easy
            /// </summary>
            public EDifficult difficult = EDifficult.Easy;
            /// <summary>
            /// Type of game. As default - classic
            /// </summary>
            public ETypeGame typeGame = ETypeGame.Classic;
            /// <summary>
            /// Frequency of saves. As default - every 5 swaps
            /// </summary>
            public EFrequencySave frequencySave = EFrequencySave.Every5Swaps;
            /// <summary>
            /// Defines is need save game on exit
            /// </summary>
            public bool saveOnExit = true;
            /// <summary>
            /// Defines is user allow push him messages from game
            /// </summary>
            public bool allowPushMessages = true;
            /// <summary>
            /// Defines allow autoupdate. As default - it's function disabled
            /// </summary>
            public bool autoUpdate = false;
            /// <summary>
            /// Defines is user allow push errors game to server
            /// </summary>
            public bool pushErrors = true;

            /// <summary>
            /// Defines is user allow win in diffrent destenations for classic
            /// </summary>
            public bool allowDiffrentDestenationWin = false;
            /// <summary>
            /// Defines is user allow using numbers at mode "Puzzle"
            /// </summary>
            public bool allowUsingNumbersPuzzle = false;
            /// <summary>
            /// Defines is user allow using numbers at mode "custom image"
            /// </summary>
            public bool allowUsingNumbersCustomImage = false;

            /// <summary>
            /// Current language. This field must be defines on game
            /// </summary>
            public LangData currentLang;

        }

        /// <summary>
        /// Defines is Settings container load
        /// </summary>
        public bool IsLoaded { get; set; }

        /// <summary>
        /// Defines is file with settings exists
        /// </summary>
        public bool IsSettingsFileExists
        {
            get
            {
                return File.Exists(PathToFileSettings);
            }
        }

        /// <summary>
        /// Defines is need show message about customization
        /// </summary>
        public bool ShowMessageAboutCust { get; private set; }

        /// <summary>
        /// Property of current lang
        /// </summary>
        public LangData CurrentLanguage
        {
            get 
            {
                return Container.currentLang;
            }
            set
            {
                Container.currentLang = value;
            }
        }

        /// <summary>
        /// Property of container
        /// </summary>
        public SettingsContainer Container { get; private set; }

        /// <summary>
        /// Path to file settings.json
        /// </summary>
        string PathToFileSettings
        {
            get
            {
                return Path.Combine(
                        Application.persistentDataPath,
                        "settings.json");
            }
        }

        /// <summary>
        /// Serializer
        /// </summary>
        DataContractJsonSerializer serializer;

        void Start()
        {
            IsLoaded = false;

            serializer = new DataContractJsonSerializer(typeof(SettingsContainer));

            if (!IsSettingsFileExists) //Check is file with settings exists
            {
                ShowMessageAboutCust = true;
                Container = new SettingsContainer();
                GetSystemLang();
                Container.version = Application.version;
                WriteToFile();

            }
            else
            {
                ShowMessageAboutCust = false;
                ReadFromFile();
                Debug.LogWarning("Realize control version!");
            }
            IsLoaded = true;
        }

        /// <summary>
        /// Async method of getting system language, and set it to container
        /// </summary>
        async void GetSystemLang()
        {
            SystemLanguage sl = Application.systemLanguage;
            MultiLangCore mlcRef = gameObject.GetComponent<MultiLangCore>();
            if (mlcRef == null)
                throw new CantFindGlobalObj(
                    "Can't find MultiLangCore on object with SettingsCore");

            while(mlcRef.LanguageDataList == null) //Await loading list of languages
                await Task.Delay(10);


            if (sl == SystemLanguage.Russian || //list of countries which undestand russian
                sl == SystemLanguage.Ukrainian ||
                sl == SystemLanguage.Polish ||
                sl == SystemLanguage.Belarusian ||
                sl == SystemLanguage.Estonian ||
                sl == SystemLanguage.Romanian ||
                sl == SystemLanguage.Latvian ||
                sl == SystemLanguage.Lithuanian)
            {
                foreach (LangData ld in mlcRef.LanguageDataList)
                {
                    //If founded russian language data
                    if (ld.langID == SystemLanguage.Russian)
                    {
                        Container.currentLang = ld;
                        return;
                    }
                }
                //if not founded russian... It's strange... throw fatal exception
                throw new CantFindLangInAssets("[FATAL!]Can't find rus.json!");
            }
            else //else - use english lang
            {
                foreach (LangData ld in mlcRef.LanguageDataList)
                {
                    //If founded russian language data
                    if (ld.langID == SystemLanguage.English)
                    {
                        Container.currentLang = ld;
                        return;
                    }
                }
                //if not found english lang... this fatal error too
                throw new CantFindLangInAssets("[FATAL!]Can't find eng.json!");
            }
        }

        /// <summary>
        /// Write settings to file
        /// </summary>
        public async void WriteToFile()
        {
            using (FileStream fs =
                new FileStream(PathToFileSettings, FileMode.Create, FileAccess.Write))
            {
                await Task.Run(() =>
                {
                    serializer.WriteObject(fs, Container);
                });
            }
        }

        /// <summary>
        /// Read settings from file
        /// </summary>
        public void ReadFromFile()
        {
            if (!IsSettingsFileExists)
            {
                Debug.LogWarning("Settings file is not exists! This file will be " +
                    "creted with default settings if it's null");
                
                if (Container != null)
                    return;
                Container = new SettingsContainer();
                GetSystemLang();
                //Finaly - write created settings
                WriteToFile();
                return;
            }

            using (FileStream fs =
                new FileStream(PathToFileSettings, FileMode.Open, FileAccess.Read))
            {
                Container = serializer.ReadObject(fs) as SettingsContainer;
            }
        }
    }
}
