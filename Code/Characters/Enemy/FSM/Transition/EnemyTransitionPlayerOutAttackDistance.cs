using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(EnemyTransitionToAttack))]
public class EnemyTransitionPlayerOutAttackDistance : EnemyTransition
{
    private EnemyTransitionToAttack _attackDistance;
    private EnemyMovement _movement;
    private Transform _transform;

    private void Awake()
    {
        _attackDistance = GetComponent<EnemyTransitionToAttack>();
        _movement = GetComponent<EnemyMovement>();
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (_movement.PlayerTarget == null)
            return;

        if (Vector3.Distance(_transform.position, _movement.PlayerTarget.position) > _attackDistance.Distance)
        {
            NeedTransit = true;
        }
    }
}
