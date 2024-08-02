using System;

public interface IWallet
{
    event Action<int> CoinChanged;

    int GetCoin();

    void AddCoin(int coin);
}
