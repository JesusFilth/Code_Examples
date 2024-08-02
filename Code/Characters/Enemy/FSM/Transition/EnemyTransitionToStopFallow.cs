using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class EnemyTransitionToStopFallow : EnemyTransition
{
    private EnemyMovement _movement;

    private void Awake()
    {
        _movement = GetComponent<EnemyMovement>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _movement.SetTarget(null);
            NeedTransit = true;
        }
    }
}
