using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using UnityEngine;
using Game.Exceptions;
using Game.Interfaces;
using Game.IO;
using System.Threading.Tasks;

namespace Game.Additional
{
    /// <summary>
    /// Core of multilanguage realisation. 
    /// Must be call before main menu creates, but after properties 
    /// </summary>
    public class MultiLangCore : MonoBehaviour, IGlobalObject
    {
        /// <summary>
        /// Class of language data
        /// </summary>
        [Serializable]
        public class LangData
        {
            /// <summary>
            /// Id of system language
            /// </summary>
            public SystemLanguage langID;
            /// <summary>
            /// Name of file at Assets\Resorces\Text
            /// </summary>
            public string fileName;
            /// <summary>
            /// Name of language which displays at selection language
            /// </summary>
            public string displayName;

#if UNITY_EDITOR
            /// <summary>
            /// This CTOR only for debug
            /// </summary>
            /// <param name="langID">Language ID</param>
            /// <param name="fileName">Name of file</param>
            /// <param name="displayName">Displayed name of language</param>
            public LangData(SystemLanguage langID, string fileName, string displayName)       
            {
                this.langID = langID;
                this.fileName = fileName;
                this.displayName = displayName;
            }
#endif
        }

        /// <summary>
        /// Container with language data
        /// </summary>
        List<LangData> lstData;
        /// <summary>
        /// Dictionary of text pairs
        /// </summary>
        Dictionary<string, string> textPairs;
        /// <summary>
        /// Data serializer
        /// </summary>
        DataContractJsonSerializer dataSerializer;
        /// <summary>
        /// Text serializer
        /// </summary>
        DataContractJsonSerializer textSerializer;

        /// <summary>
        /// Containts list of MultiLang. This container needs for update values when
        /// laungage edit.
        /// </summary>
        public List<MultiLang> textFieldsContainer;

        public bool IsLoaded { get; private set; }

        /// <summary>
        /// Returns path to data of text
        /// </summary>
        static string PathToData
        {
            get 
            {
                return Path.Combine("Text", "data"); //<-do not use json!
            }
        }

        /// <summary>
        /// Method which calls at first
        /// </summary>
        async void Start()
        {
            IsLoaded = false;

            lstData = new List<LangData>();
            textPairs = new Dictionary<string, string>();
            dataSerializer = new DataContractJsonSerializer(typeof(List<LangData>));
            textSerializer = new DataContractJsonSerializer(
                typeof(Dictionary<string, string>));

            textFieldsContainer = new List<MultiLang>();

            LoadLangData();

            //MakeDataFile(); <-Only for debug
            SettingsCore sc =  gameObject.GetComponent<SettingsCore>();
            if (sc == null)
                throw new CantFindGlobalObj("Can't find SettingsCore!");
            while (!sc.IsLoaded)
                await Task.Delay(10);

            CheckPropertyLang();
            //MakeLangFiles(); <- Only for debug
            LoadLangDictionary();
            IsLoaded = true;
        }

        /// <summary>
        /// Method of loading language data
        /// </summary>
        void LoadLangData()
        {
            TextAsset data = Resources.Load<TextAsset>(PathToData);
            using (MemoryStream ms = new MemoryStream(data.bytes, 0, data.bytes.Length))
            {
                lstData = dataSerializer.ReadObject(ms) as List<LangData>;
            }
        }
        /// <summary>
        /// Method of checking: is current language in properties containts at lstDat.
        /// If language not containts - loads english lang
        /// </summary>
        void CheckPropertyLang()
        {
            SettingsCore sc = gameObject.GetComponent<SettingsCore>();

            foreach (LangData ld in lstData)
            {
                if (ld.langID == sc.CurrentLanguage.langID)
                    return;
            }
            sc.CurrentLanguage.langID = SystemLanguage.English;
        }

        /// <summary>
        /// Method which loads dictionary of language pairs. Language which be used
        /// seted at Properties.
        /// This method can be load from another methods
        /// </summary>
        public void LoadLangDictionary()
        {
            textPairs.Clear();

            string path = null;
            SettingsCore sc = gameObject.GetComponent<SettingsCore>();

            foreach (LangData ld in lstData)
            {
                if (ld.langID == sc.CurrentLanguage.langID)
                {
                    path = ld.fileName;
                    break;
                }
            }

            if (path == null)
                throw new NoTextFileException("Can't find pair with selected lang id!");

            TextAsset ta = Resources.Load<TextAsset>(Path.Combine("Text", path));
            if (ta.bytes.Length == 0)
                throw new NoTextFileException("Can't load text from " + path + ".json!");

            using (MemoryStream ms = new MemoryStream(ta.bytes, 0, ta.bytes.Length))
            { 
                textPairs = textSerializer.ReadObject(ms) as Dictionary<string, string>;
            }

            foreach (MultiLang ml in textFieldsContainer)
            {
                ml.SetText();
            }
        }

        /// <summary>
        /// Returns text by id
        /// </summary>
        /// <param name="id">id of phrase</param>
        /// <returns>Text which in pair with id, or error messge if text not found</returns>
        public string GetText(string id)
        {
            if (!textPairs.ContainsKey(id))
            {
                string str = "Can't find text with id: " + id;
                Debug.LogError(str);
                return str;
            }
            return textPairs[id];
        }

        /// <summary>
        /// Returns list with languages name
        /// </summary>
        /// <returns>List</returns>
        public List<string> GetLangList()
        {
            List<string> lst = new List<string>();
            foreach (LangData ld in lstData)
                lst.Add(ld.displayName);
            return lst;
        }

        /// <summary>
        /// Gets list of language data
        /// </summary>
        public List<LangData> LanguageDataList
        { 
            get
            {
                return lstData;
            }
        }

        //Methods only for debug
#if UNITY_EDITOR
        /// <summary>
        /// Path to debug files
        /// </summary>
        static string PathToDebugFiles
        {
            get
            {
                return "D:/DebugFiles";
            }
        }
        /// <summary>
        /// Debug method, which makes default data file with russian and
        /// english languages
        /// </summary>
        void MakeDataFile()
        {

            List<LangData> lst = new List<LangData>();
            lst.Add(new LangData(SystemLanguage.Russian, "rus", "Русский"));
            lst.Add(new LangData(SystemLanguage.English, "eng", "English"));

            if (!Directory.Exists(PathToDebugFiles))
                Directory.CreateDirectory(PathToDebugFiles);

            using (FileStream fs = new FileStream(Path.Combine(PathToDebugFiles, "data.json"),
                FileMode.Create, FileAccess.Write))
            {
                dataSerializer.WriteObject(fs, lst);
            }
        }

        /// <summary>
        /// Debug method, which makes default files of eng and rus localization
        /// </summary>
        void MakeLangFiles()
        {
            Dictionary<string, string> rusDictionary = new Dictionary<string, string>();
            Dictionary<string, string> engDictionary = new Dictionary<string, string>();

            rusDictionary.Add("newGame", "Новая игра");
            rusDictionary.Add("continueGame", "Продолжить игру");

            engDictionary.Add("newGame", "New game");
            engDictionary.Add("continueGame", "Continue game");

            using (FileStream fs = new FileStream(Path.Combine(PathToDebugFiles, "rus.json"),
                FileMode.Create, FileAccess.Write))
            {
                textSerializer.WriteObject(fs, rusDictionary);
            }

            using (FileStream fs = new FileStream(Path.Combine(PathToDebugFiles, "eng.json"),
                FileMode.Create, FileAccess.Write))
            {
                textSerializer.WriteObject(fs, engDictionary);
            }

        }
#endif
    }
}