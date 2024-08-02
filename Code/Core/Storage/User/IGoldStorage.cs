using System;

public interface IGoldStorage
{
    event Action<int> GoldChanged;

    int GetGold();

    void AddGold(int value);
}
