using System.Collections.Generic;
using System;
using UnityEngine;

public class MineralBagView : MonoBehaviour
{
    [SerializeField] private MeneralIconSetting _mineralIcons;
    [SerializeField] private MineralView _viewPrefab;
    [SerializeField, SerializeInterface(typeof(IMineralView))] private GameObject _truck; 

    private IMineralView _mineralView;
    private Transform _transform;

    private void Awake()
    {
        _mineralView = _truck.GetComponent<IMineralView>();
        _transform = transform;
        Initialize();
    }

    private void OnEnable()
    {
        _mineralView.BagChanged += ChangeBag;
    }

    private void OnDisable()
    {
        _mineralView.BagChanged -= ChangeBag;
    }

    private void OnValidate()
    {
        if (_truck == null)
            throw new ArgumentNullException(nameof(_truck));
    }

    private void Initialize()
    {
        for (int i = 0; i < _mineralIcons.MineralIcons.Length; i++)
        {
            MineralType mineralType = _mineralIcons.MineralIcons[i].Type;

            MineralView view = Instantiate(_viewPrefab);
            view.transform.SetParent(transform, false);
            view.Init(_mineralIcons.GetIcon(mineralType), mineralType);
            view.gameObject.SetActive(false);
        }
    }

    private void ChangeBag(IReadOnlyDictionary<MineralType, int> minerals)
    {
        AllHide();

        foreach (var mineral in minerals)
        {
            CheckMineral(mineral.Key, mineral.Value);
        }
    }

    private void CheckMineral(MineralType type, int count)
    {
        for (int i = 0; i < _transform.childCount; i++)
        {
            if(_transform.GetChild(i).TryGetComponent(out MineralView view))
            {
                if (view.MineralType == type)
                {
                    view.gameObject.SetActive(true);
                    view.SetCount(count);
                }
            }
        }
    }

    private void AllHide()
    {
        for (int i = 0; i < _transform.childCount; i++)
        {
            _transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
