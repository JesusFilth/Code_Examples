using Reflex.Attributes;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MineralSpawner : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> _points = new List<SpawnPoint>();
    [SerializeField] private MineralOreSettings _settings;

    [Inject] private ILevelMineralOreSetting _oreSetting;
    [Inject] private ICurrentLevelInfo _currentLevelInfo;

    private void OnValidate()
    {
        if (_settings == null)
            throw new ArgumentNullException(nameof(_settings));
    }

    private void Awake()
    {
        Initialize();
    }

    public void Create(IReadOnlyList<TempleBlockInterval> blocks)
    {
        foreach (TempleBlockInterval block in blocks)
        {
            while (block.Count != 0)
            {
                MineralSizeType sizeType = _settings.GetBetweenSize(block.Count);
                int sizeOre = _settings.GetRandomCount(sizeType);

                MineralOreInitsializator ore = Instantiate(_settings.MineralOre, GetFreeRandomPoint().position, Quaternion.identity);
                ore.Init(
                    block.Type,
                    sizeOre,
                    _oreSetting.GetMaxProgress(),
                    _oreSetting.GetForceResistance(),
                    _settings.GetModelView(block.Type, sizeType));

                block.AddCount(-sizeOre);
            }
        }
    }

    private void Initialize()
    {
        GameLevelConteinerDI.Instance.InjectRecursive(gameObject);
        RemoveOverPoints();
    }

    private void RemoveOverPoints()
    {
        _points.RemoveAll(point => (int)point.Mode > (int)_currentLevelInfo.GetLevelType());
    }

    private Transform GetFreeRandomPoint()
    {
        if(_points == null || _points.Count == 0)
            throw new ArgumentNullException(nameof(_points));

        int randomPoint = Random.Range(0, _points.Count);
        SpawnPoint tempPoint = _points[randomPoint];

        Transform point = tempPoint.transform;
        _points.Remove(tempPoint);

        return point;
    }
}
