using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckBagView : MonoBehaviour
{
    private int _childCount;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
        _childCount = _transform.childCount;
    }

    public void UpdateBagConteiner(float currentMineralCount, float maxMineralCount)
    {
        const int MaxPercent = 100;

        float fillMinetalPercent = (currentMineralCount / maxMineralCount) * MaxPercent;
        int counBagElement = (int)(_childCount * (fillMinetalPercent / MaxPercent));

        for (int i = 0; i < _transform.childCount; i++)
            _transform.GetChild(i).gameObject.SetActive(counBagElement >= i);
    }
}
