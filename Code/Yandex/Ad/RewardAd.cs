using Reflex.Attributes;
using UnityEngine;

public class RewardAd : MonoBehaviour
{
    [Inject] private IGoldStorage _goldStorage;

    private int _giftCoins = 50;

    public void Show(int coins)
    {
        _giftCoins = coins;
#if UNITY_WEBGL && !UNITY_EDITOR
         Agava.YandexGames.VideoAd.Show(OnOpenCallback,OnRevardCallback, OnCloseCallback);
#endif
    }

    private void OnOpenCallback()
    {
        Time.timeScale = 0;
        AudioListener.volume = 0;
    }

    private void OnCloseCallback()
    {
        Time.timeScale = 1;
        AudioListener.volume = 1;
    }

    private void OnRevardCallback()
    {
        _goldStorage.AddGold(_giftCoins);
    }
}
