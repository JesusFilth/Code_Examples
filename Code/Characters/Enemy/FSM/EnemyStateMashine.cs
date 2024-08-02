using UnityEngine;

public class EnemyStateMashine : MonoBehaviour
{
    [SerializeField] private EnemyState _firstState;
    [SerializeField] private bool _isStartAwake;

    public EnemyState CurrentState { get; private set; }

    private void Awake()
    {
        if(_isStartAwake)
            ResetToFirstState();
    }

    private void Update()
    {
        if (CurrentState == null)
            return;

        EnemyState nextState = CurrentState.GetNextState();

        if (nextState != null)
            Transit(nextState);
    }

    public void ResetToFirstState()
    {
        CurrentState?.Exit();

        CurrentState = _firstState;
        CurrentState.Enter();
    }

    private void Transit(EnemyState nextState)
    {
        if(nextState==null)
            return;

        CurrentState?.Exit();
        CurrentState = nextState;
        CurrentState.Enter();
    }
}
