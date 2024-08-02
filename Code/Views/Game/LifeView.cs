using System;
using UnityEngine;

public class LifeView : MonoBehaviour
{
    [SerializeField] private Transform _conteiner;
    [SerializeField] private Stats _stats;

    private void OnValidate()
    {
        if (_stats == null)
            throw new ArgumentNullException(nameof(_stats));
    }

    private void OnEnable()
    {
        _stats.LifeChanged += ChangeValue;
    }

    private void OnDisable()
    {
        _stats.LifeChanged -= ChangeValue;
    }

    private void ChangeValue(int value)
    {
        for (int i = 0; i < _conteiner.childCount; i++)
        {
            _conteiner.GetChild(i).gameObject.SetActive((i+1)<=value);
        }
    }
}
