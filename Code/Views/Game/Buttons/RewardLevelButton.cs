using Reflex.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RewardLevelButton : MonoBehaviour
{
    [Inject] private IWallet _wallet;
    [Inject] private IGoldStorage _goldStorage;

    private Button _button;

    private void Awake() => _button = GetComponent<Button>();

    private void OnEnable() => _button.onClick.AddListener(OnClick);

    private void OnDisable() => _button.onClick.RemoveListener(OnClick);

    private void OnClick()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Agava.YandexGames.VideoAd.Show(OnOpenCallback, OnRevardCallback, OnCloseCallback);
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
        _goldStorage.AddGold(_wallet.GetCoin());
        _wallet.AddCoin(_wallet.GetCoin());
        gameObject.SetActive(false);
    }
}
