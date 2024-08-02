using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyStateMashine))]
public class Enemy : MonoBehaviour, ISpawnObject
{
    private Transform _transform;
    private EnemyStateMashine _stateMashine;

    private void Awake()
    {
        _transform = transform;
        _stateMashine = GetComponent<EnemyStateMashine>();
    }

    private void OnEnable()
    {
        try
        {
            Validate();
        }
        catch (Exception ex)
        {
            enabled = false;
            throw ex;
        }
    }

    public void Init(Transform point)
    {
        _transform.position = point.position;
        _stateMashine.ResetToFirstState();
    }

    private void Validate()
    {
        
    }
}
