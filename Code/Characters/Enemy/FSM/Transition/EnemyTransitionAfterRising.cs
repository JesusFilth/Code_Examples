using UnityEngine;

[RequireComponent(typeof(EnemyRisingState))]
public class EnemyTransitionAfterRising : EnemyTransition
{
    private EnemyRisingState _risingState;

    private void Awake()
    {
        _risingState = GetComponent<EnemyRisingState>();
    }

    private void Update()
    {
        if (_risingState.IsFinished)
            NeedTransit = true;
    }
}
