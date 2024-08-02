using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSetting", menuName = "World of Cubes/LevelSetting", order = 2)]
public class LevelSetting : ScriptableObject
{
    const int MaxMode = 3;

    [SerializeField] private string _name;
    [SerializeField] private int _needStars;
    [SerializeField] private Sprite _icon;
    [SerializeField] private LevelModeInit _levelMap;
    [SerializeField] private LevelMode[] _mods = new LevelMode[MaxMode];

    public string Name => _name;
    public int NeedStars => _needStars;
    public Sprite Icon => _icon;

    public LevelModeInit LevelMap => _levelMap;

    private void OnValidate()
    {
        for (int i = 0; i < _mods.Length; i++)
        {
            _mods[i].SetType((LevelTypeMode)i);
        }

        if (_mods.Length != MaxMode)
            Array.Resize(ref _mods, MaxMode);

        if (_levelMap == null)
            throw new ArgumentNullException(nameof(_levelMap));
    }

    public LevelMode GetLevelMode(LevelTypeMode mode)
    {
        return _mods.FirstOrDefault(levelMode => levelMode.Type == mode);
    }
}
