using Reflex.Attributes;
using System;
using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
[RequireComponent(typeof(CharacterAnimation))]
public class Player : MonoBehaviour, IWallet, IPlayerPosition, IPlayerStats
{
    [SerializeField] private MeleeAttack _meleeAttack;
    [SerializeField] private KeyCode _attack = KeyCode.Mouse1;

    public event Action<int> CoinChanged;

    private PlayerStats _stats;
    private CharacterAnimation _animation;
    private Transform _transform;

    [Inject] private IPlayerInput _playerInput;
    [Inject] private GameUIStateMashine _gameUI;

    private Wallet _wallet = new Wallet();

    private void Awake()
    {
        _stats = GetComponent<PlayerStats>();
        _animation = GetComponent<CharacterAnimation>();

        _transform = transform;
    }

    private void Start()
    {
        CoinChanged?.Invoke(_wallet.Coin);
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

        _stats.Died += Die;
    }

    private void OnDisable()
    {
        _stats.Died -= Die;
    }

    private void Update()
    {
        if (_playerInput.IsAttack())
        {
            Attack();
        }
    }

    public PlayerStats GetStats() => _stats;

    public void SetPosition(Transform point)
    {
        if(_transform == null)
            _transform = transform;

        _transform.position = point.position;
    }

    public Transform GetPosition() => _transform;

    public int GetCoin() => _wallet.Coin;

    public void AddCoin(int coin)
    {
        _wallet.AddCoin(coin);
        CoinChanged?.Invoke(_wallet.Coin);
    }

    private void Validate()
    {
        if (_meleeAttack == null)
            throw new ArgumentNullException(nameof(_meleeAttack));
    }

    private void Attack()
    {
        if (_meleeAttack.TryHit(_stats.Damage))
        {
            _animation.ToAttack();
        }
    }

    private void Die()
    {
        _animation.ToDie();   
        gameObject.SetActive(false);

        _gameUI.EnterIn<LoseWindowUIState>();
    }
}
