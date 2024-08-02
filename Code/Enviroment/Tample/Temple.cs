using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using Reflex.Attributes;

public class Temple : WorkProcess<PlayerBuilder>
{
    [SerializeField] private TempleBlock[] _templeBlocks;
    [SerializeField] private MineralSpawner _mineralSpawner;

    private int _currentIndexBlock = 0;
    private List<TempleBlockInterval> _blocks = new List<TempleBlockInterval>();

    [Inject] private ITempleProgressView _buidProgressView;
    [Inject] private ITempleBuildSounds _sounds;
    [Inject] private ILevelTempleSetting _templeSetting;
    [Inject] private GameUIStateMashine _gameUI;
    [Inject] private MessageBox _messageBox;
    [Inject] private LocalizationTranslate _localizationTranslate;  

    private void Start()
    {
        Initialize();
        UpdateProgressView();
    }

    private void OnValidate()
    {
        if (_templeBlocks == null || _templeBlocks.Length == 0)
            throw new ArgumentException(nameof(_templeBlocks));
    }

    public void SetProgressView(ITempleProgressView buidProgressView)
    {
        _buidProgressView = buidProgressView;
    }

    public override void ToWork()
    {
        if (Player == null)
            throw new ArgumentNullException(nameof(Player));

        if (CanBuild() == false)
        {
            Debug.Log("Больше строить нечего");
            return;
        }

        if (Player.CanBuild(_blocks.First().Type) == false)
        {
            _messageBox.Show(_localizationTranslate.GetMessage(LocalizationMessageType.DontOre));
            return;
        }

        _sounds.ToBuild();
        AddProgress(Player.GetWorkPower());

        Player.Transform.LookAt(gameObject.transform);
        
        CheckComplete();
    }

    protected override void Complete()
    {
        BuildBlock();
        NextCurrentBlock();
        CheckWin();
    }

    private void Initialize()
    {
        ReadBlockConteiner();
        SetCurrentCount(_blocks.First().Count);

        SetForceResistance(_templeSetting.GetBuildForceResistance());
        SetMaxProgress(_templeSetting.GetBuildMaxProgress());

        _mineralSpawner.Create(GetBlocksPrototype());
    }

    private IReadOnlyList<TempleBlockInterval> GetBlocksPrototype()
    {
        List<TempleBlockInterval> prototype = new List<TempleBlockInterval>();

        foreach (TempleBlockInterval block in _blocks)
            prototype.Add(new TempleBlockInterval(block.Type, block.Count));

        return prototype;
    }

    private void ReadBlockConteiner()
    {
        for (int i = 0; i < _templeBlocks.Length; i++)
        {
            TempleBlockInterval tempBlock = _blocks.LastOrDefault();

            if (tempBlock == null || tempBlock.Type != _templeBlocks[i].Type)
            {
                _blocks.Add(new TempleBlockInterval(_templeBlocks[i].Type, 1));
                continue;
            }

            tempBlock.AddCount();
        }
    }

    private bool CanBuild() => _blocks.Count > 0;

    private void BuildBlock()
    {
        if(Player == null)
            throw new ArgumentNullException(nameof(Player));

        Player.BuildBlock(_templeBlocks[_currentIndexBlock]);
    }

    private void NextCurrentBlock()
    {
        _blocks.First().RemoveCount();

        if (_blocks.First().HasCount() == false)
            _blocks.RemoveAt(0);

        _currentIndexBlock++;
        UpdateProgressView();
    }

    private float GetProgressPercent()
    {
        const float MaxPercent = 100;

        if (_currentIndexBlock == 0)
            return 0;

        float _current = _currentIndexBlock;
        float _maxValue = _templeBlocks.Length;

        return (_current / _maxValue) * MaxPercent;
    }

    private void CheckWin()
    {
        if (CanBuild())
        {
            SetCurrentCount(_blocks.First().Count);
            return;
        }

        SetCurrentCount(0);
        _gameUI.EnterIn<WinWindowUIState>();
    }

    private void UpdateProgressView()
    {
        const int NextIndex = 1;

        if (CanBuild())
        {
            bool hasNext = _blocks.Count > NextIndex;

            if (hasNext)
                _buidProgressView.ChangeNextMineral(hasNext, _blocks[NextIndex].Type);
            else
                _buidProgressView.ChangeNextMineral(hasNext, _blocks.First().Type);
        
            _buidProgressView.ChangeCurrentMineral(_blocks.First().Type);
            _buidProgressView.ChangeBuildProgressBar(GetProgressPercent(), _blocks.First().Count);

            return;
        }

        _buidProgressView.ChangeBuildProgressBar(GetProgressPercent(), 0);
    }
}
