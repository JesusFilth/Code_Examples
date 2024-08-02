using System;
using UnityEngine;

public class TruckMovement : MonoBehaviour
{
    [SerializeField] private float _minDistance = 2f;
    [SerializeField] private float _speed;

    private Transform _target;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        if (_target == null)
            return;

        if (Vector3.Distance(_transform.position, _target.position) > _minDistance)
        {
            _transform.LookAt(_target);
            _transform.position = Vector3.Lerp(_transform.position, _target.position, _speed * Time.deltaTime);
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}
