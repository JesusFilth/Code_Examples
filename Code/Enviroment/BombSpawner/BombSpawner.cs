using Reflex.Attributes;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] private float _delay = 5;
    [SerializeField, Range(0,100)] private int _chance = 50;
    [SerializeField] private Bomb _bomb;

    private WaitForSeconds _waitForSeconds;
    private Coroutine _delaing;

    [Inject] private ILevelBombSettings _levelBombSettings;

    private void Awake()
    {
        GameLevelConteinerDI.Instance.InjectRecursive(gameObject);

        _delay = _levelBombSettings.GetDelay();
        _chance = _levelBombSettings.GetChance();

        _waitForSeconds = new WaitForSeconds(_delay);
    }

    private void OnEnable()
    {
        if (_delaing == null)
            _delaing = StartCoroutine(Delaing());
    }

    private void OnDisable()
    {
        if (_delaing != null)
        {
            StopCoroutine(_delaing);
            _delaing = null;
        }
    }

    private IEnumerator Delaing()
    {
        while (enabled)
        {
            yield return _waitForSeconds;

            if (IsCreate())
            {
                _bomb.Create();
            }
        }
    }

    private bool IsCreate()
    {
        const int MaxChance = 100;

        int randomChanse = Random.Range(0, MaxChance);

        return randomChanse <= _chance;
    }
}
