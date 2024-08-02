using com.cyborgAssets.inspectorButtonPro;
using IJunior.TypedScenes;
using Reflex.Attributes;
using UnityEngine;

public class OnGameLevelLoaded : MonoBehaviour, ISceneLoadHandler<LevelMode>,
    ILevelSpawnItemSetting, 
    ILevelMineralOreSetting,
    ILevelEnemySettings,
    ILevelTempleSetting,
    ICurrentLevelInfo,
    ILevelSkillSetting,
    ILevelBombSettings
{
    [Inject] private GameUIStateMashine _gameUI;// temp this
    [Inject] private IPlayerPosition _player;

    [Inject] private SpawnEnemyPool _spawnEnemyPool;
    [Inject] private SpawnItemPool _spawnItemPool;

    [Inject] private GameLevelConteinerDI _levelConteinerDI;

    private LevelMode _currentLevelMode;

    public void OnSceneLoaded(LevelMode levelMode)
    {
        _levelConteinerDI.InitHot();
        _currentLevelMode = levelMode;

        Initialize();
    }

    private void Initialize()
    {
        LevelModeInit level = Instantiate(_currentLevelMode.Map);

        _spawnEnemyPool.Init(_currentLevelMode.Enemys, _currentLevelMode.EnemyCapasityPool);
        _spawnItemPool.Init(_currentLevelMode.Items, _currentLevelMode.ItemCapasityPool);

        level.Init(_currentLevelMode.Type);
        _player.SetPosition(level.StartPoint);
    }

    [ProButton]
    public void ToWin()//temp
    {
        _gameUI.EnterIn<WinWindowUIState>();
    }

    [ProButton]
    public void ToLose()//temp
    {
        _gameUI.EnterIn<LoseWindowUIState>();
    }

    public int GetCount() => _currentLevelMode.ItemSpawnCount;

    public int GetMaxOreCount() => _currentLevelMode.MaxOreCount;

    public float GetMaxProgress() => _currentLevelMode.OreMaxProgress;

    public float GetForceResistance() => _currentLevelMode.OreForceResistance;

    public float GetSpawnChance() => _currentLevelMode.EnemyChanceSpawn;

    public float GetSpawnDelay() => _currentLevelMode.EnemyDelaySpawn;

    public float GetImproveStats() => _currentLevelMode.ImproveEnemyStats;

    public float GetBuildForceResistance() => _currentLevelMode.BuildForceResistance;

    public float GetBuildMaxProgress() => _currentLevelMode.BuildMaxProgress;

    public int GetLevelNumber() => _currentLevelMode.CurrentLevelIndex + 1;

    public LevelTypeMode GetLevelType() => _currentLevelMode.Type;

    public int GetPrice() => _currentLevelMode.GoldPrise;

    public int GetMaxValue() => _currentLevelMode.MaxValueSkill;

    public float GetDelay() => _currentLevelMode.BombDelay;

    public int GetChance() => _currentLevelMode.BombChance;

    public float GetMaerkerDelay() => _currentLevelMode.BombMarkerDelay;
}
