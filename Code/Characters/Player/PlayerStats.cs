using Reflex.Attributes;
using System;
using UnityEngine;

public class PlayerStats : Stats
{
    [SerializeField] private float _miningPower;
    [SerializeField] private float _buildPower;
    [SerializeField] private int _maxMineralConteiner;

    public event Action<int> MaxMineralChanged;

    public float MiningPower => _miningPower;
    public float BuildPower => _buildPower;
    public int MaxMaxeralConteiner => _maxMineralConteiner;

    [Inject] private IStateStorage _stateStorage;

    private void Start()
    {
        Initialize(_stateStorage.GetStats());

        UpadateViewChanges();
        MaxMineralChanged?.Invoke(_maxMineralConteiner);
    }

    public void AddBuildPower(float power)
    {
        _buildPower = Mathf.Clamp(_buildPower += power, 0, int.MaxValue);
    }

    public void AddMiningPower(float power)
    {
        _miningPower = Mathf.Clamp(_miningPower += power, 0, int.MaxValue);
    }

    public void AddMaxMineralConteinerSize(int size)
    {
        _maxMineralConteiner = Mathf.Clamp(_maxMineralConteiner += size, 0, int.MaxValue);
        MaxMineralChanged?.Invoke(_maxMineralConteiner);
    }

    private void Initialize(UserStatsModel userStats)
    {
        _miningPower = userStats.CraftSpeed;
        _buildPower = userStats.BuildSpeed;
        _maxMineralConteiner = (int)userStats.MaxMineralConteiner;
        SetDamage(userStats.Damage);
        SetMaxHealth(userStats.Health);
        CurrentHealth = userStats.Health;
    }
}
