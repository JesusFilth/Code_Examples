using UnityEngine;
using UnityEngine.Pool;

public class MineralPool : MonoBehaviour
{
    [SerializeField] private MineralCubeView _mineralPrefab;
    [SerializeField] private int _capasity = 10;
    [SerializeField] private int _maxSize = 10;

    public static MineralPool Instance;

    private ObjectPool<MineralCubeView> _pool;

    private MineralMovmentSettings _currentMineralSettings;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            Initilize();
        }
    }

    public void Create(MineralMovmentSettings mineralSettings)
    {
        _currentMineralSettings = mineralSettings;

        _pool.Get();
    }

    public void Release(MineralCubeView mineral)
    {
        _pool.Release(mineral);
    }

    private void Initilize()
    {
        _pool = new ObjectPool<MineralCubeView>(
            createFunc: () => Instantiate(_mineralPrefab),
            actionOnGet: (mineral) => ActionOnGet(mineral),
            actionOnRelease: (mineral) => ActionOnRelease(mineral),
            actionOnDestroy: (mineral) => Destroy(mineral.gameObject),
            collectionCheck: true,
            defaultCapacity: _capasity,
            maxSize: _maxSize
            );
    }

    private void ActionOnGet(MineralCubeView mineral)
    {
        mineral.gameObject.SetActive(true);
        mineral.Init(_currentMineralSettings);
    }

    private void ActionOnRelease(MineralCubeView mineral)
    {
        mineral.gameObject.transform.parent = null;
        mineral.gameObject.SetActive(false);
    }
}
