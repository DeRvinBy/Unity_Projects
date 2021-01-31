using System.IO;
using UnityEngine;

namespace Scripts.Localization
{
    public class Localization : MonoBehaviour
    {
        public static Translate Translate = new Translate();

        [SerializeField]
        [Tooltip("Count variatns of languages")]
        private int CountOfLaguages = 2;

        private Languages _currentLanguage;
        private LanguageInterprater _interprater;

        private void Awake()
        {
            if (!PlayerPrefs.HasKey("Language"))
            {
                if (Application.systemLanguage == SystemLanguage.Russian)
                {
                    PlayerPrefs.SetString("Language", "ru_RU");
                    _currentLanguage = Languages.Russian;
                }
                else
                {
                    PlayerPrefs.SetString("Language", "en_EN");
                    _currentLanguage = Languages.English;
                }
            }
            else
            {
                if (PlayerPrefs.GetString("Language") == "ru_RU")
                {
                    _currentLanguage = Languages.Russian;
                }
                else
                {
                    _currentLanguage = Languages.English;
                }
            }

            _interprater = GetComponent<LanguageInterprater>();

            LoadLanguage();
        }

        public void LoadLanguage()
        {

#if UNITY_ANDROID
        string stream = Path.Combine(Application.streamingAssetsPath, "Languages/" + PlayerPrefs.GetString("Language") + ".json");
        WWW reader = new WWW(stream);
        while(!reader.isDone) { }
        translate = JsonUtility.FromJson<Translate>(reader.text);
#endif

#if UNITY_STANDALONE
            string stream = File.ReadAllText(Application.streamingAssetsPath + "/Languages/" + PlayerPrefs.GetString("Language") + ".json");
            Translate = JsonUtility.FromJson<Translate>(stream);
#endif

            _interprater.SetTranslation();
        }

        public void SetLanguage(int index)
        {
            switch ((Languages)index)
            {
                case Languages.English:
                    PlayerPrefs.SetString("Language", "en_EN");
                    break;
                case Languages.Russian:
                    PlayerPrefs.SetString("Language", "ru_RU");
                    break;
                default:
                    PlayerPrefs.SetString("Language", "en_EN");
                    break;
            }

            _currentLanguage = (Languages)index;

            LoadLanguage();
        }

        public void ChangeLanguage()
        { 
            int index = (int)_currentLanguage;
            index++;

            if (index >= CountOfLaguages)
                index = 0;

            SetLanguage(index);
        }
    }

    public enum Languages
    {
        English,
        Russian
    }
}