using Reflex.Attributes;
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemysSpawner : MonoBehaviour
{
    private const float MinPitchCreateSound = 0.8f;
    private const float MaxPitchCreateSound = 1.2f;
    const float MaxChange = 100;

    [SerializeField] [Range(0, MaxChange)]  private float _chance = 50f;
    [SerializeField] private float _delay = 5f;
    [SerializeField] private Transform _pointsConteiner;
    [SerializeField] private AudioSource _createSound;

    [SerializeField] private bool _isLevelModeSettingOverride;

    private Transform[] _points;
    private Coroutine _creating;
    private Coroutine _cooldawnWaiting;
    private WaitForSeconds _waitForSeconds;

    private bool _hasPlayer;

    [Inject] private SpawnEnemyPool _pool;
    [Inject] private ILevelEnemySettings _enemySetting;

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(_delay);
        Initialize();
    }

    private void OnDisable()
    {
        if(_creating!=null)
        {
            StopCoroutine(_creating);
            _creating = null;
        }

        if (_cooldawnWaiting != null)
        {
            StopCoroutine(_cooldawnWaiting);
            _cooldawnWaiting = null;
        }
    }

    private void OnValidate()
    {
        if (_pointsConteiner == null)
            throw new ArgumentNullException(nameof(_pointsConteiner));

        if (_createSound == null)
            throw new ArgumentNullException(nameof(_createSound));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            StartCreating();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            StopCreating();
        }
    }

    private void Initialize()
    {
        _points = new Transform[_pointsConteiner.childCount];

        for (int i = 0; i < _pointsConteiner.childCount; i++)
            _points[i] = _pointsConteiner.GetChild(i);

        GameLevelConteinerDI.Instance.InjectRecursive(gameObject);

        if (_isLevelModeSettingOverride)
            return;

        _chance = _enemySetting.GetSpawnChance();
        _delay = _enemySetting.GetSpawnDelay();

        _waitForSeconds = new WaitForSeconds(_delay);
    }

    private IEnumerator Creating()
    {
        while (enabled)
        {
            if (IsChangeCreate())
            {
                _pool.Create(GetRandomPoint());
                PlayCreateSound();
            }

            yield return _waitForSeconds;
        }
    }

    private IEnumerator CooldawnWaiting()
    {
        yield return _waitForSeconds;

        if (_hasPlayer)
        {
            if (_creating == null)
                _creating = StartCoroutine(Creating());
        }

        _cooldawnWaiting = null;
    }

    private void PlayCreateSound()
    {
        _createSound.pitch = Random.Range(MinPitchCreateSound,MaxPitchCreateSound);
        _createSound.Play();
    }

    private void StartCreating()
    {
        _hasPlayer = true;

        if (_cooldawnWaiting == null)
        {
            _cooldawnWaiting = StartCoroutine(CooldawnWaiting());

            if (_creating == null)
                _creating = StartCoroutine(Creating());
        }
    } 

    private void StopCreating()
    {
        _hasPlayer = false;

        if (_creating != null)
        {
            StopCoroutine(_creating);
            _creating = null;
        }
    }

    private bool IsChangeCreate()
    {
        float randomChance = Random.Range(0, MaxChange);

        if(randomChance <=_chance)
            return true;

        return false;
    }

    private Transform GetRandomPoint()
    {
        int ranndomIndex = UnityEngine.Random.Range(0, _points.Length);
        
        return _points[ranndomIndex];
    }
}
