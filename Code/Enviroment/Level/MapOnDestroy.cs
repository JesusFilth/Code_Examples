using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapOnDestroy : MonoBehaviour
{
    [SerializeField] private Transform _pointsConteiner;

    private void OnEnable()
    {
        ChangeActive(true);
    }

    private void OnDisable()
    {
        ChangeActive(false);
    }

    private void ChangeActive(bool isActive)
    {
        for (int i = 0; i < _pointsConteiner.childCount; i++)
        {
            _pointsConteiner.GetChild(i).gameObject.SetActive(isActive);
        }
    }
}
