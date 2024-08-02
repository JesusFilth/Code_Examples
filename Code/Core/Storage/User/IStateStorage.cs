using System;

public interface IStateStorage
{
    event Action<UserStatsModel> StatsChanged;

    bool AddStat(ref float value);

    UserStatsModel GetStats();

    int GetPurchaseGold(float value);
}
