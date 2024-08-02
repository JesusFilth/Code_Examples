using Agava.YandexGames;
using System;
using System.Collections;

public class BootstrapState : IGameState
{
    private readonly StateMashine _stateMashine;

    public BootstrapState(StateMashine stateMashine)
    {
        if (stateMashine == null)
            throw new ArgumentNullException(nameof(stateMashine));

        _stateMashine = stateMashine;
    }

    public void Execute()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        YandexGamesSdk.CallbackLogging = true;
        CoroutineRunner.Instance.Run(Initialize());
#else
        _stateMashine.EnterIn<LoadDataState>();
#endif
    }

    private IEnumerator Initialize()
    {        
        yield return YandexGamesSdk.Initialize(OnInitialized);
    }

    private void OnInitialized()
    {
        _stateMashine.EnterIn<LoadDataState>();
    }
}
