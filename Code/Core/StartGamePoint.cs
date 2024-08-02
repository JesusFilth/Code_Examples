using Reflex.Attributes;
using UnityEngine;

public class StartGamePoint : MonoBehaviour
{
    [Inject] private StateMashine _stateMashine;
    [Inject] private IUserStorage _userStorage;
    [Inject] private IDefaultUser _defaultUser;

    private void Start()
    {
        _stateMashine.Init(_userStorage, _defaultUser);
        _stateMashine.EnterIn<BootstrapState>();
    }
}
