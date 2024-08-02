using Reflex.Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class WinWindowView : GameOverView
{
    [Inject] private ILevelStorage _levelStorage;
    [Inject] private IWallet _wallet;

    protected override void Initialize()
    {
        Sounds.Win();

        _wallet.AddCoin(LevellInfo.GetPrice());
        Gold.text = _wallet.GetCoin().ToString();
        LevelNumber.text = LevellInfo.GetLevelNumber().ToString();

        _levelStorage.AddStar(LevellInfo.GetLevelNumber() - 1, LevellInfo.GetLevelType());

        GoldStorage.AddGold(_wallet.GetCoin());
    }
}
