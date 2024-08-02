using UnityEngine;
using System;
using Reflex.Attributes;

public abstract class Item : MonoBehaviour, ISpawnObject
{
    [SerializeField] private Vector3 _spawnPointOffset = new Vector3(0,0.5f,0);
    [SerializeField] private AudioClip _useSound;

    [Inject] private IItemSounds _sounds;

    private void Awake()
    {
        GameLevelConteinerDI.Instance.InjectRecursive(gameObject);
    }

    private void OnEnable()
    {
        try
        {
            Validate();
        }
        catch(ArgumentNullException ex)
        {
            enabled = false;
            throw ex;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _sounds.PlayClip(_useSound);
            Use(player);

            Destroy();
        }
    }

    public void Init(Transform point)
    {
        transform.position = point.position + _spawnPointOffset;
    }

    protected abstract void Use(Player player);

    private void Validate()
    {
        if (_useSound == null)
            throw new ArgumentNullException(nameof(_useSound));
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }
}
