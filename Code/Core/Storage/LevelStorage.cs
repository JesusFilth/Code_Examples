using System;
using UnityEngine;

public class LevelStorage : MonoBehaviour, IFindLevel, ILevelInfo, IDefaultUser
{
    [SerializeField] private DefaultUserSettings _userSettings;
    [SerializeField] private LevelSetting[] _levels;

    public LevelSetting[] Levels => _levels;

    public bool TryGetLevel(int index, LevelTypeMode mode, out LevelMode level)
    {
        level = _levels[index].GetLevelMode(mode);

        if (level != null)
        {
            level.SetLevelIndex(index);
            level.SetMap(_levels[index].LevelMap);
            return true;
        }

        return false;
    }

    public UserModel GetUser()
    {
        UserModel user;
        _userSettings.Init(out user);

        user.Levels = GetLevels();

        return user;
    }
    
    public int GetNeedStars(int index)
    {
        if(index > _levels.Length || index<0)
            throw new ArgumentOutOfRangeException(nameof(index));

        return _levels[index].NeedStars;
    }

    public Sprite GetIcon(int index)
    {
        if (index > _levels.Length || index < 0)
            throw new ArgumentOutOfRangeException(nameof(index));

        return _levels[index].Icon;
    }

    private LevelModel[] GetLevels()
    {
        if (_levels == null || _levels.Length == 0)
            throw new ArgumentNullException(nameof(_levels));

        LevelModel[] levels = new LevelModel[_levels.Length];

        for (int i = 0; i < levels.Length; i++)
        {
            levels[i] = new LevelModel() {
                Name = _levels[i].Name, 
                Stars = 0, 
                NeedStarForOpen = _levels[i].NeedStars,
                OpenMode = LevelTypeMode.I,
                Id = i};
        }

        //temp
        levels[0].IsOpen = true;
        levels[0].OpenMode = LevelTypeMode.I;
        levels[0].Stars = 0;

        //levels[1].IsOpen = true;
        //levels[1].OpenMode = LevelTypeMode.I;
        //levels[1].Stars = 0;

        return levels;
    }
}
