using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterAnimation))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    public Transform PlayerTarget { get; private set; }
    public bool HasTarget => PlayerTarget != null;

    private NavMeshAgent _navAgent;
    private CharacterAnimation _movebeAnimation;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
        _movebeAnimation = GetComponent<CharacterAnimation>();
        _navAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        ChangeWalkAnimation();
        Rotate();
    }

    public void SetTarget(Transform target)
    {
        PlayerTarget = target;
    }

    public void StartFallowTarget()
    {
        if (PlayerTarget == null)
            return;

        _navAgent.SetDestination(PlayerTarget.position);
        _navAgent.isStopped = false;
    }

    public void StopFallowTarget()
    {
        _navAgent.isStopped = true;
    }

    private void Rotate()
    {
        if (PlayerTarget == null)
            return;

        Vector3 moveDirection = PlayerTarget.position - _transform.position;

        float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
        _transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    private void ChangeWalkAnimation()
    {
        bool isWalk = _navAgent.velocity != Vector3.zero;
        _movebeAnimation.ToWalk(isWalk);
    }
}
