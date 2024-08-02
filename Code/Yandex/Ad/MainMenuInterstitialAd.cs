using Agava.YandexGames;
using UnityEngine;

public class MainMenuInterstitialAd : MonoBehaviour
{
    private void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        InterstitialAd.Show(OnOpenAdCallback, OnCloseAdCallback);
#endif
    }

    private void OnOpenAdCallback()
    {
        AudioListener.volume = 0;
        Time.timeScale = 0;
    }

    private void OnCloseAdCallback(bool wasShown)
    {
        Time.timeScale = 1;
        AudioListener.volume = 1;
    }
}
