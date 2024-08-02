using Reflex.Attributes;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float _markerDelay;
    [SerializeField] private ParticleSystem _marker;
    [SerializeField] private BombExplosion _explosion;

    public UnityEvent OnCreate;

    private Coroutine _markerShowing;
    private WaitForSeconds _waitForSeconds;

    [Inject] private IPlayerPosition _playerPosition;
    [Inject] private ILevelBombSettings _levelBombSettings;

    private void Awake()
    {
        GameLevelConteinerDI.Instance.InjectRecursive(gameObject);
        _markerDelay = _levelBombSettings.GetMaerkerDelay();

        _waitForSeconds = new WaitForSeconds(_markerDelay);
    }

    private void OnDisable()
    {
        if(_markerShowing != null)
        {
            StopCoroutine(_markerShowing);
            _markerShowing = null;
        }
    }

    public void Create()
    {
        gameObject.transform.position = _playerPosition.GetPosition().position;
        OnCreate?.Invoke();

        if(_markerShowing == null)
            _markerShowing = StartCoroutine(MarkerShowing());
    }

    private IEnumerator MarkerShowing()
    {
        _marker.Play();
        yield return _waitForSeconds;
        _marker.Stop();

        _explosion.ToActive();

        _markerShowing = null;
    }
}
