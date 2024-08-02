using Reflex.Attributes;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Stats))]
public class Loot : MonoBehaviour
{
    [SerializeField] private float _droopChance = 30f;

    private Transform _transform;
    private Stats _stats;

    [Inject] private SpawnItemPool _itemPool;

    private void Awake()
    {
        _stats = GetComponent<Stats>();
        _transform = transform;

        GameLevelConteinerDI.Instance.InjectRecursive(gameObject);
    }

    private void OnEnable()
    {
        _stats.Died += Drop;
    }

    private void OnDisable()
    {
        _stats.Died -= Drop;
    }

    private void Drop()
    {
        if (IsDrop())
            _itemPool.Create(_transform);
    }

    private bool IsDrop()
    {
        const float MaxDropChance = 100;

        float chance = Random.Range(0,MaxDropChance+1);

        if(chance<=_droopChance)
            return true;

        return false;
    }
}
