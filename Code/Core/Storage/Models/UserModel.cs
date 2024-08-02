using System;
using UnityEngine.Scripting;

[Serializable]
public class UserModel
{
    [field: Preserve] public string Name;

    [field: Preserve] public int Gold;

    [field: Preserve] public UserStatsModel PlayerStats;

    [field: Preserve] public LevelModel[] Levels;
}
