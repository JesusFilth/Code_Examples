using Agava.YandexGames;
using Lean.Localization;
using UnityEngine;

public class LocalizationInitialize : MonoBehaviour
{
    private const string EnglishCode = "English";
    private const string RussinCode = "Russian";
    private const string TurkishCode = "Turkish";
    private const string Turkish = "tr";
    private const string Russian = "ru";
    private const string English = "en";

    [SerializeField] private LeanLocalization _leanLocalization;

    private void Awake()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        ChangeLenguage();
#endif
    }

    private void ChangeLenguage()
    {
        string languageCode = YandexGamesSdk.Environment.i18n.lang;

        switch (languageCode)
        {
            case English:
                _leanLocalization.SetCurrentLanguage(EnglishCode);
                break;
            case Russian:
                _leanLocalization.SetCurrentLanguage(RussinCode);
                break;
            case Turkish:
                _leanLocalization.SetCurrentLanguage(TurkishCode);
                break;
            default:
                _leanLocalization.SetCurrentLanguage(EnglishCode);
                break;
        }
    }
}
