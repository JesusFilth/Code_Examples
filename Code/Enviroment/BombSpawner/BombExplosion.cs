using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BombExplosion : MonoBehaviour
{
    private const int LifeDamage = 1;
    private const float TimeColladerEnter = 0.3f;

    [SerializeField] private ParticleSystem _explosion;
    [SerializeField] private Collider _collider;
    [SerializeField] private float _delayCollider;

    public UnityEvent OnActive;

    private Coroutine _showing;
    private WaitForSeconds _waitDelayColliderExit;
    private WaitForSeconds _waitDelayColliderEnter;

    private void Awake()
    {
        _waitDelayColliderExit = new WaitForSeconds(_delayCollider);
        _waitDelayColliderEnter = new WaitForSeconds(TimeColladerEnter);
    }

    private void OnDisable()
    {
        if (_showing != null)
        {
            StopCoroutine(_showing);
            _showing = null;
        }
    }

    public void ToActive()
    {
        _explosion.Play();
        OnActive?.Invoke();

        if (_showing == null)
            _showing = StartCoroutine(ColliderShowing());
    }

    private IEnumerator ColliderShowing()
    {
        yield return _waitDelayColliderEnter;
        _collider.enabled = true;

        yield return _waitDelayColliderExit;
        _collider.enabled = false;

        _showing = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerStats playerStats))
        {
            playerStats.AddLife(-LifeDamage);
        }
    }
}
