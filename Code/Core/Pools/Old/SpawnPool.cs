using System.Collections.Generic;
using System.Linq;
using UnityEngine.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnPool<T> : MonoBehaviour where T : MonoBehaviour, ISpawnObject
{
    [SerializeField] private SpawnObjectModel[] _spawnObjects;
    [SerializeField] private int _capasity = 10;
    [SerializeField] private int _maxSize = 10;

    public static SpawnPool<T> Instance { get; private set; }

    private ObjectPool<T> _pool;
    private List<T> _poolCurrentObj = new();
    private Transform _currentPoint;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Initilize();
        }
    }

    public void Create(Transform point)
    {
        _currentPoint = point;
        _pool.Get();
    }

    public void Release(T obj)
    {
        _pool.Release(obj);
    }

    private void Initilize()
    {
        InitializeEnemys();
        InitializePool();
    }

    private T InstantiateRandomObj()
    {
        int randomIndex = Random.Range(0, _poolCurrentObj.Count);

        return Instantiate(_poolCurrentObj[randomIndex]);
    }

    private void InitializeEnemys()
    {
        float totalWeight = _spawnObjects.Sum(spawnModel => spawnModel.Weight);
        Dictionary<T, int> countCreatEnemys = new Dictionary<T, int>();

        foreach (var spawnModel in _spawnObjects)
        {
            int count = (int)Mathf.Round(spawnModel.Weight / totalWeight * _capasity);

            if (spawnModel.Prefab.TryGetComponent(out T obj))
                countCreatEnemys.Add(obj, count);
        }

        foreach (var enemyCreate in countCreatEnemys)
        {
            for (int i = 0; i < enemyCreate.Value; i++)
            {
                _poolCurrentObj.Add(enemyCreate.Key);
            }
        }
    }

    private void InitializePool()
    {
        _pool = new ObjectPool<T>(
            createFunc: () => InstantiateRandomObj(),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => ActionOnRelease(obj),
            actionOnDestroy: (obj) => Destroy(obj.gameObject),
            collectionCheck: true,
            defaultCapacity: _capasity,
            maxSize: _maxSize
            );
    }

    private void ActionOnGet(T obj)
    {
        obj.gameObject.SetActive(true);
        obj.Init(_currentPoint);
    }

    private void ActionOnRelease(T obj)
    {
        obj.gameObject.SetActive(false);
        //Destroy(obj.gameObject);
    }
}
