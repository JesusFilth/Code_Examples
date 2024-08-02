using System;
using UnityEngine;

public class LevelModeInit : MonoBehaviour
{
    [SerializeField] private Transform _startPlayerPoint;
    [SerializeField] private ItemSpawner _itemSpawner;
    [SerializeField] private LevelMap _levelMap;
    [SerializeField] private LevelTemple _levelTemple;

    public Transform StartPoint => _startPlayerPoint;

    private void OnValidate()
    {
        if (_startPlayerPoint == null)
            throw new ArgumentNullException(nameof(_startPlayerPoint));

        if (_itemSpawner == null)
            throw new ArgumentNullException(nameof(_itemSpawner));

        if (_levelMap == null)
            throw new ArgumentNullException(nameof(_levelMap));

        if (_levelTemple == null)
            throw new ArgumentNullException(nameof(_levelTemple));
    }

    public void Init(LevelTypeMode mode)
    {
        CreateMap(mode);
        CreateTemple(mode);
        _itemSpawner.Create(mode);
    }

    private void CreateMap(LevelTypeMode mode)
    {
        _levelMap.Init(mode);
    }

    private void CreateTemple(LevelTypeMode mode)
    {
        _levelTemple.Init(mode);
    }
}
