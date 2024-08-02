using System;
using UnityEngine;

[Serializable]
public class LevelMode
{
    [SerializeField] private LevelTypeMode _type;
    [SerializeField] private int _goldPrise;
    [Header("Work Process")]
    [Space]
    [SerializeField] private int _maxOreCount;
    [SerializeField] private float _oreForceResistance = 5;
    [SerializeField] private float _oreMaxProgress = 100;
    [Space]
    [SerializeField] private float _buildForceResistance = 5;
    [SerializeField] private float _buildMaxProgress = 100;
    [Header("Enemys")]
    [Space]
    [SerializeField] private SpawnObjectModel[] _enemys;
    [SerializeField] private float _enemyChanceSpawn;
    [SerializeField] private float _enemyDelaySpawn;
    [SerializeField] private int _enemyCapasityPool = 10;
    [SerializeField] private float _improveEnemyStats;
    [Header("Items")]
    [Space]
    [SerializeField] private SpawnObjectModel[] _items;
    [SerializeField] private int _itemSpawnCount;
    [SerializeField] private int _itemCapasityPool = 5;
    [Header("Skills")]
    [Space]
    [SerializeField][Range(1,100)] private int _maxValueSkill;
    [Header("Bombs")]
    [Space]
    [SerializeField] private float _bombDelay = 10;
    [SerializeField] private int _bombChance = 50;
    [SerializeField] private float _bombMarkerDalay = 3;

    public LevelTypeMode Type => _type;
    public LevelModeInit Map => _map;
    public int GoldPrise => _goldPrise;

    public int MaxOreCount => _maxOreCount;
    public float OreForceResistance => _oreForceResistance;
    public float OreMaxProgress => _oreMaxProgress;
    public float BuildForceResistance => _buildForceResistance;
    public float BuildMaxProgress => _buildMaxProgress;

    public SpawnObjectModel[] Enemys => _enemys;
    public float EnemyChanceSpawn => _enemyChanceSpawn;
    public float EnemyDelaySpawn => _enemyDelaySpawn;
    public int EnemyCapasityPool => _enemyCapasityPool;
    public float ImproveEnemyStats => _improveEnemyStats;

    public SpawnObjectModel[] Items => _items;
    public int ItemSpawnCount => _itemSpawnCount;
    public int ItemCapasityPool => _itemCapasityPool;

    public int MaxValueSkill => _maxValueSkill;

    public float BombDelay => _bombDelay;
    public int BombChance => _bombChance;
    public float BombMarkerDelay => _bombMarkerDalay;

    public int CurrentLevelIndex { get; private set; }

    private LevelModeInit _map;

    public void SetMap(LevelModeInit map)
    {
        _map = map;
    }

    public void SetType(LevelTypeMode levelTypeMode)
    {
        _type = levelTypeMode;
    }

    public void SetLevelIndex(int index) => CurrentLevelIndex = index;
}