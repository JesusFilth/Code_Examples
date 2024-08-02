using System;
using UnityEngine;

public class LevelMap : MonoBehaviour
{
    private const int MaxCountMode = 3;

    [SerializeField] private LevelMapMode[] _maps = new LevelMapMode[MaxCountMode];

    private void OnValidate()
    {
        if (_maps.Length != MaxCountMode)
            _maps = new LevelMapMode[MaxCountMode];

        foreach (var map in _maps)
        {
            if(map == null)
                throw new ArgumentNullException(nameof(_maps));
        }
    }

    public void Init(LevelTypeMode mode)
    {
        foreach(var map in _maps)
        {
            if ((int)map.Mode <= (int)mode)
                map.gameObject.SetActive(true);
            else
                Destroy(map.gameObject);
        }
    }
}
