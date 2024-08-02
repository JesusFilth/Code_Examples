using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class SpawnObjectPool<TSpawn> : MonoBehaviour where TSpawn : MonoBehaviour, ISpawnObject
{
    [SerializeField] private Transform _conteiner;

    private List<TSpawn> _spawnObjects = new List<TSpawn>();
    private int _capasity;

    private void OnValidate()
    {
        if (_conteiner == null)
            throw new ArgumentNullException(nameof(_conteiner));
    }

    public void Create(Transform point)
    {
        if (_spawnObjects.Count == 0)
        {
            Debug.Log("Spawn Objects count is 0");
            return;
        }

        TSpawn[] hideObjects = _spawnObjects.Where(obj => obj.gameObject.activeSelf == false).ToArray();

        if (hideObjects == null || hideObjects.Length == 0)
        {
            int randomObjectIndex = Random.Range(0, _spawnObjects.Count);
            TSpawn temp = Instantiate(_spawnObjects[randomObjectIndex], _conteiner);
            temp.Init(point);
            _spawnObjects.Add(temp);

            return;
        }

        int randomIndex = Random.Range(0, hideObjects.Length);
        TSpawn spawnObj = hideObjects[randomIndex];

        spawnObj.gameObject.SetActive(true);
        spawnObj.Init(point);
    }

    public void Init(SpawnObjectModel[] spawnObjects, int capasity)
    {
        if (spawnObjects == null || spawnObjects.Length == 0)
        {
            Debug.Log("No spawnObjects assigned");
            throw new ArgumentNullException(nameof(spawnObjects));
        }

        _capasity = capasity;

        float totalWeight = spawnObjects.Sum(spawnModel => spawnModel.Weight);
        Dictionary<TSpawn, int> countCreatSpawnObject = new Dictionary<TSpawn, int>();

        foreach (var spawnModel in spawnObjects)
        {
            int count = (int)Mathf.Round(spawnModel.Weight / totalWeight * _capasity);

            if (spawnModel.Prefab.TryGetComponent(out TSpawn obj))
                countCreatSpawnObject.Add(obj, count);
        }

        foreach (var objCreate in countCreatSpawnObject)
        {
            for (int i = 0; i < objCreate.Value; i++)
            {
                TSpawn temp = Instantiate(objCreate.Key, _conteiner);
                temp.gameObject.SetActive(false);
                _spawnObjects.Add(temp);
            }
        }
    }
}
