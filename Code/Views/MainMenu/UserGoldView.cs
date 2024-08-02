using Reflex.Attributes;
using TMPro;
using UnityEngine;

public class UserGoldView : MonoBehaviour
{
    [SerializeField] private TMP_Text _gold;

    [Inject] private IGoldStorage _storage;

    private void OnEnable()
    {
        _storage.GoldChanged += UpdateValue;    
    }

    private void OnDisable()
    {
        _storage.GoldChanged += UpdateValue;
    }

    private void Start()
    {
        UpdateValue(_storage.GetGold());
    }

    private void UpdateValue(int value)
    {
        _gold.text = value.ToString();
    }
}
