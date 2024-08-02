using Reflex.Attributes;
using System.Collections;
using UnityEngine;

public class FloorLava : MonoBehaviour
{
    const float TimeScale = 0.1f;

    [SerializeField] private Transform _startPoint;
    [SerializeField] private float _duration = 0.1f;
    [SerializeField] private float _delayHideUI = 1;
    [SerializeField] private float _durationUI = 1.5f;

    private Coroutine _showing;

    [Inject] private GameUIStateMashine _gameUI;
    [Inject] private IFlooLavaSound _sound;

    private void Awake()
    {
        GameLevelConteinerDI.Instance.InjectRecursive(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            StartShow(player);
        }
    }

    private IEnumerator Showing(Player player)
    {
        Time.timeScale = TimeScale;

        _gameUI.EnterIn<LavaUIState>();

        yield return new WaitForSeconds(_duration);

        if (player.gameObject.TryGetComponent(out CharacterController controller))
        {
            controller.enabled = false;
            player.gameObject.transform.position = _startPoint.position;//?
            controller.enabled = true;
        }

        Time.timeScale = 1;

        yield return new WaitForSeconds(_delayHideUI);
        _gameUI.EnterIn<GameUIState>();

        if (player.gameObject.TryGetComponent(out Stats stat))
        {
            stat.AddLife(-1);
        }

        _showing = null;
    }

    private void StartShow(Player player)
    {
        _sound.Play();

        if (_showing == null)
            _showing = StartCoroutine(Showing(player));
    }

    private void StopShow()
    {
        if(_showing != null)
        {
            StopCoroutine(_showing);
            _showing = null;
        }
    }
}
