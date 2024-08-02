using System;
using System.Collections.Generic;

public class StateMashine
{
    private Dictionary<Type, IGameState> _states;
    private IGameState _currentState;

    public void Init(IUserStorage userStorage, IDefaultUser defaultUser)
    {
        _states = new Dictionary<Type, IGameState>()
        {
            [typeof(BootstrapState)] = new BootstrapState(this),
            [typeof(LoadDataState)] = new LoadDataState(this, userStorage, defaultUser),
            [typeof(LoadMainMenuSceneState)] = new LoadMainMenuSceneState(),
            [typeof(LoadGameSceneState)] = new LoadGameSceneState()
        };
    }

    public void EnterIn<TState>() where TState : IGameState
    {
        if(_states.TryGetValue(typeof(TState), out IGameState state))
        {
            _currentState = state;
            _currentState.Execute();
        }
    }

    public void EnterIn<TState, TParam>(TParam param) where TState : IGameState<TParam>
    {
        if (_states.TryGetValue(typeof(TState), out IGameState state))
        {
            _currentState = state;
            ((IGameState<TParam>)_currentState).SetParam(param);
            _currentState.Execute();
        }
    }

    public void AddState(IGameState state)
    {
        Type type = state.GetType();

        if(_states.ContainsKey(type) == false)
        {
            _states.Add(type, state);
        }
    }
}
