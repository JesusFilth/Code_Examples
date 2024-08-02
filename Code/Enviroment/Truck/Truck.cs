using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TruckMovement))]
public class Truck : MonoBehaviour, ITruckView, IMineralView, IMineralCubeViewFinalPosition
{
    [SerializeField] private Transform _cubeEndPoint;
    [SerializeField] private TruckBagView _bagView;
    [SerializeField] private Transform _target;

    public Transform CubeEndPoint => _cubeEndPoint;
    public bool IsFull => _currentMineralCount == _maxMineralCount;

    public event Action<int, int> ValueChanged;
    public event Action<IReadOnlyDictionary<MineralType, int>> BagChanged;

    private int _currentMineralCount = 0;
    private int _maxMineralCount;
    private Dictionary<MineralType, int> _minerals = new Dictionary<MineralType, int>();

    private TruckMovement _movement;
    private Coroutine _destroingCube;
    private WaitForSeconds _waitForSeconds = new WaitForSeconds(0.3f);

    private void Awake()
    {
        _movement = GetComponent<TruckMovement>();
        _movement.SetTarget(_target);
    }

    private void OnEnable()
    {
        try
        {
            Validate();
        }
        catch(Exception ex)
        {
            enabled = false;
            throw ex;
        }
    }

    public void RemoveCube(MineralCubeView mineralCube)
    {
        if (_destroingCube == null)
        {
            _destroingCube = StartCoroutine(DestroingCube(mineralCube));
        }
    }

    public void ToBuild(TempleBlock block)
    {
        MineralMovmentSettings mineralSettings = new MineralMovmentSettings();
        mineralSettings.StartPoint = _cubeEndPoint;
        mineralSettings.EndPoint = block.transform;
        mineralSettings.Type = block.Type;
        mineralSettings.FinalAction = block;

        RemoveMineral(block.Type);

        MineralPool.Instance.Create(mineralSettings);
    }

    public void SetMaxMineral(int value)
    {
        _maxMineralCount = Mathf.Clamp(value, 0, int.MaxValue);
        ValueChanged?.Invoke(_currentMineralCount, _maxMineralCount);
    }

    public void SetMovementTarget(Transform target)
    {
        _movement.SetTarget(target);
    }

    public bool HasMineral(MineralType type)
    {
        return _minerals.ContainsKey(type);
    }

    public void AddMineral(MineralType type)
    {
        if(IsFull)
            return;

        if (_minerals.ContainsKey(type) == false)
            _minerals[type] = 0;

        _minerals[type]++;
        _currentMineralCount++;

        ValueChanged?.Invoke(_currentMineralCount, _maxMineralCount);
        BagChanged?.Invoke(_minerals);

        _bagView.UpdateBagConteiner(_currentMineralCount, _maxMineralCount);
    }

    private void RemoveMineral(MineralType mineral)
    {
        _minerals[mineral]--;

        if (_minerals[mineral] == 0)
            _minerals.Remove(mineral);

        _currentMineralCount--;

        ValueChanged?.Invoke(_currentMineralCount, _maxMineralCount);
        BagChanged?.Invoke(_minerals);

        _bagView.UpdateBagConteiner(_currentMineralCount, _maxMineralCount);
    }

    private IEnumerator DestroingCube(MineralCubeView mineralCube)
    {
        mineralCube.DisableKinematic();

        yield return _waitForSeconds;

        MineralPool.Instance.Release(mineralCube);
        _destroingCube = null;
    }

    private void Validate()
    {
        if (_cubeEndPoint == null)
            throw new ArgumentNullException(nameof(_cubeEndPoint));

        if (_bagView == null)
            throw new ArgumentNullException(nameof(_bagView));

        if (_target == null)
            throw new ArgumentNullException(nameof(_target));
    }
}
