using Agava.YandexGames;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AutorizationView : MonoBehaviour
{
    [SerializeField] private MainMenuNavigation _menu;
    [SerializeField] private Button _yes;
    [SerializeField] private Button _no;

    private void OnEnable()
    {
        _yes.onClick.AddListener(ToAutorization);
        _no.onClick.AddListener(ToMainMenu);
    }

    private void OnDisable()
    {
        _yes.onClick.RemoveListener(ToAutorization);
        _no.onClick.RemoveListener(ToMainMenu);
    }

    private void OnValidate()
    {
        if (_menu == null)
            throw new ArgumentNullException(nameof(_menu));

        if (_yes == null)
            throw new ArgumentNullException(nameof(_yes));

        if (_no == null)
            throw new ArgumentNullException(nameof(_no));
    }

    private void ToAutorization()
    {
        PlayerAccount.Authorize();
        ToMainMenu();
    }

    private void ToMainMenu() => _menu.ToMain();
}
