using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class EnemyTransitionToFallow : EnemyTransition
{
    private EnemyMovement _movement;

    private void Awake()
    {
        _movement = GetComponent<EnemyMovement>();
    }

    private void Update()
    {
        if (_movement.HasTarget)
            NeedTransit = true;
    }
}
