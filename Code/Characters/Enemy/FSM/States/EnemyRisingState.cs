using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(EnemyComponentHide))]
[RequireComponent(typeof(CharacterAnimation))]
public class EnemyRisingState : EnemyState
{
    const float RisingTime = 3f;

    public bool IsFinished { get; private set; }

    private Coroutine _rising;

    private Transform _transform;
    private EnemyStats _stats;
    private CharacterAnimation _animations;
    private EnemyComponentHide _hideComponent;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _stats = GetComponent<EnemyStats>();
        _hideComponent = GetComponent<EnemyComponentHide>();
        _animations = GetComponent<CharacterAnimation>();
    }

    private void OnEnable()
    {
        IsFinished = false;
        _stats.ResetToDefault();
        _animations.ToIdel();
        _hideComponent.Off();

        if (_rising == null)
            _rising = StartCoroutine(Rising());
    }

    private void OnDisable()
    {
        if(_rising != null)
        {
            StopCoroutine(_rising);
            _rising = null;
        }
    }

    private IEnumerator Rising()
    {
        ParticleSystem particle = ParticlePool.Instance.Create(_transform);

        Vector3 originPosition = _transform.position;
        Vector3 startPosition = new Vector3(_transform.position.x, -_transform.position.y, _transform.position.z);
        _transform.position = startPosition;

        float elapsedTime = 0;

        while (elapsedTime < RisingTime)
        {
            float progress = elapsedTime / RisingTime;
            _transform.position = Vector3.Lerp(startPosition, originPosition, progress);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ParticlePool.Instance.Release(particle);
        _transform.position = originPosition;

        IsFinished = true;
        _hideComponent.On();

        _rising = null;
    }
}
