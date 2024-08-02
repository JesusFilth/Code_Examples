using Agava.YandexGames;
using System;
using System.Linq;
using UnityEngine;

public class UserStorage : IStateStorage, IGoldStorage, ILevelStorage, IUserStorage
{
    private const string UserKey = "User";
    private const string LeaderboardName = "Leaderboard";
    private const int MaxStars = 3;
    private const int GoldRise = 50;

    public event Action<UserStatsModel> StatsChanged;
    public event Action<int> GoldChanged;

    private UserModel _user;

    public int GetGold()
    {
        return _user.Gold;
    }

    public void AddGold(int gold)
    {
        _user.Gold = Mathf.Clamp(_user.Gold += gold, 0, int.MaxValue);
        GoldChanged?.Invoke(_user.Gold);
        Save();
    }

    public int GetPurchaseGold(float value)
    {
        return (int)value * GoldRise;
    }

    public bool AddStat(ref float value)
    {
        if (HasGold(value))
        {
            AddGold(-GetPurchaseGold((int)value));
            value += 1;
            StatsChanged?.Invoke(GetStats());
            Save();

            return true;
        }

        return false;
    }

    public UserStatsModel GetStats()
    {
        return _user.PlayerStats;
    }
    
    public int GetAllStars()
    {
        return _user.Levels.Sum(level => level.Stars);
    }

    public LevelModel[] GetLevels()
    {
        return _user.Levels;
    }

    public void AddStar(int indexLevel, LevelTypeMode mode)
    {
        LevelModel level = _user.Levels[indexLevel];

        if ((int)mode < (int)level.OpenMode)
            return;

        if(mode == level.OpenMode)
            if (level.Stars == MaxStars)
                return;

        level.OpenMode = (LevelTypeMode)((int)level.OpenMode + 1);
        level.Stars++;

        OpenNewLevels();
        UpdatePlayerScore();
        Save();
    }

    public void SetUser(UserModel user) => _user = user;

    private void OpenNewLevels()
    {
        for(int i = 0; i < _user.Levels.Length; i++)
        {
            _user.Levels[i].IsOpen = _user.Levels[i].NeedStarForOpen <= GetAllStars();
        }
    }

    private void Save()
    {
        string json = JsonUtility.ToJson(_user);
        PlayerPrefs.SetString(UserKey, json);
        PlayerPrefs.Save();

#if UNITY_WEBGL && !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized)
            PlayerAccount.SetCloudSaveData(json);
#endif
    }

    private bool HasGold(float value)
    {
        if (GetPurchaseGold((int)value) <= _user.Gold)
            return true;

        return false;
    }

    private void UpdatePlayerScore()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
         if (PlayerAccount.IsAuthorized == false)
            return;

        int score = GetAllStars();

        Leaderboard.GetPlayerEntry(LeaderboardName, (result) =>
        {
            if (result == null || result.score < score)
                Leaderboard.SetScore(LeaderboardName, score);
        });
#endif
    }
}
