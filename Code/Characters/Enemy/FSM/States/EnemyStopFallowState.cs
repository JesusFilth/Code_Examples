using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class EnemyStopFallowState : EnemyState
{
    private EnemyMovement _movement;

    private void Awake()
    {
        _movement = GetComponent<EnemyMovement>();
    }

    private void OnEnable()
    {
        _movement.StopFallowTarget();
    }
}
