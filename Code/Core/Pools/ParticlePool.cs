using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ParticlePool : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;

    [SerializeField] private int _capasity = 10;
    [SerializeField] private int _maxSize = 10;

    public static ParticlePool Instance;

    private ObjectPool<ParticleSystem> _pool;
    private Transform _currentPoint;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Initilize();
        }
    }

    private void Initilize()
    {
        _pool = new ObjectPool<ParticleSystem>(
            createFunc: () => Instantiate(_particle),
            actionOnGet: (particle) => ActionOnGet(particle),
            actionOnRelease: (particle) => ActionOnRelease(particle),
            actionOnDestroy: (particle) => Destroy(particle.gameObject),
            collectionCheck: true,
            defaultCapacity: _capasity,
            maxSize: _maxSize
            );
    }

    public ParticleSystem Create(Transform point)
    {
        _currentPoint = point;

        return _pool.Get();
    }

    public void Release(ParticleSystem particle)
    {
        _pool.Release(particle);
    }

    private void ActionOnGet(ParticleSystem particle)
    {
        particle.gameObject.SetActive(true);
        particle.transform.position = _currentPoint.position;
    }

    private void ActionOnRelease(ParticleSystem particle)
    {
        particle.gameObject.SetActive(false);
    }
}
