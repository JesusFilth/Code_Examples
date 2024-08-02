using Reflex.Attributes;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class LoseWindowView : GameOverView
{
    protected override void Initialize()
    {
        Sounds.Lose();

        Gold.text = Wallet.GetCoin().ToString();
        LevelNumber.text = LevellInfo.GetLevelNumber().ToString();

        GoldStorage.AddGold(Wallet.GetCoin());
    }
}
