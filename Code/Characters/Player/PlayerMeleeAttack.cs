using System;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMeleeAttack : MeleeAttack, IPlayerNearEnemys
{
    [SerializeField] private float _sphereNearRadius = 1f;

    private Collider[] _colliders;
    private List<Stats> _nearEnemys = new List<Stats>();

    private void FixedUpdate()
    {
        CheckNearEnemys();
    }

    public bool IsNear()
    {
        return _nearEnemys.Count > 0;   
    }

    protected override bool TryGetTarget(out Stats target)
    {
        target = null;

        if (TryGetCastSphereTarget(out target) == false)
        {
            target = GetNearTarget();

            if(target == null)
                return false;
        }

        return true;
    }

    private void CheckNearEnemys()
    {
        _colliders = Physics.OverlapSphere(Transform.position, _sphereNearRadius, LayerMask, QueryTriggerInteraction.Ignore);

        if (_colliders != null && _colliders.Length > 0)
        {
            _nearEnemys.Clear();

            foreach (Collider collider in _colliders)
            {
                if(collider.TryGetComponent(out Stats stat))
                    if(stat.IsDead() == false)
                        _nearEnemys.Add(stat);
            }

            return;
        }

        _nearEnemys.Clear();
    }

    private Stats GetNearTarget()
    {
        if (_nearEnemys.Count == 0)
            return null;

        Stats nearEnemy = null;
        float minDistance = float.MaxValue;

        foreach (Stats stat in _nearEnemys)
        {
            float distance = Vector3.Distance(Transform.position, stat.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                nearEnemy = stat;
            }
        }

        return nearEnemy;
    }
}
