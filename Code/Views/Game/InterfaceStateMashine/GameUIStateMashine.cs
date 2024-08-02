using System;
using System.Collections.Generic;
using UnityEngine;

public class GameUIStateMashine : MonoBehaviour
{
    [SerializeField, SerializeInterface(typeof(IGameLevelView))] private GameObject _gameUIView;
    [SerializeField, SerializeInterface(typeof(IGameLevelView))] private GameObject _menuView;
    [SerializeField, SerializeInterface(typeof(IGameLevelView))] private GameObject _loseWindow;
    [SerializeField, SerializeInterface(typeof(IGameLevelView))] private GameObject _winWindow;
    [SerializeField, SerializeInterface(typeof(IGameLevelView))] private GameObject _lavaWindow;
    [SerializeField, SerializeInterface(typeof(IGameLevelView))] private GameObject _skillsWindow;

    private IGameUIState _currentState;
    private Dictionary<Type, IGameUIState> _states;

    private void Awake()
    {
        Initialize();
    }

    private void OnValidate()
    {
        if (_gameUIView == null)
            throw new ArgumentNullException(nameof(_gameUIView));

        if (_menuView == null)
            throw new ArgumentNullException(nameof(_menuView));

        if (_winWindow == null)
            throw new ArgumentNullException(nameof(_winWindow));

        if (_loseWindow == null)
            throw new ArgumentNullException(nameof(_loseWindow));

        if (_lavaWindow == null)
            throw new ArgumentNullException(nameof(_lavaWindow));

        if (_skillsWindow == null)
            throw new ArgumentNullException(nameof(_skillsWindow));
    }

    public void EnterIn<TState>() where TState : IGameUIState
    {
        if(_states.TryGetValue(typeof(TState), out IGameUIState state))
        {
            _currentState?.Close();
            _currentState = state;
            _currentState.Open();
        }
    }

    public void AddState(IGameUIState state)
    {
        Type type = state.GetType();

        if(_states.ContainsKey(type) == false)
            _states.Add(type, state);
    }

    private void Initialize()
    {
        _states = new Dictionary<Type, IGameUIState>()
        {
            [typeof(GameUIState)] = new GameUIState(_gameUIView.GetComponent<IGameLevelView>()),
            [typeof(GameMenuUIState)] = new GameMenuUIState(_menuView.GetComponent<IGameLevelView>()),
            [typeof(LoseWindowUIState)] = new LoseWindowUIState(_loseWindow.GetComponent<IGameLevelView>()),
            [typeof(WinWindowUIState)] = new WinWindowUIState(_winWindow.GetComponent<IGameLevelView>()),
            [typeof(LavaUIState)] = new GameUIState(_lavaWindow.GetComponent<IGameLevelView>()),
            [typeof(SkillsUIState)] = new SkillsUIState(_skillsWindow.GetComponent<IGameLevelView>()),
        };

        EnterIn<GameUIState>();
    }
}
