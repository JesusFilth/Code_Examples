using Reflex.Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsView : MonoBehaviour
{
    [SerializeField] private TMP_Text _health;
    [SerializeField] private TMP_Text _damage;
    [SerializeField] private TMP_Text _buildSpeed;
    [SerializeField] private TMP_Text _craftSpeed;
    [SerializeField] private TMP_Text _mineralConteiner;

    [SerializeField] private TMP_Text _priceHealth;
    [SerializeField] private TMP_Text _priceDamage;
    [SerializeField] private TMP_Text _priceBuildSpeed;
    [SerializeField] private TMP_Text _priceCraftSpeed;
    [SerializeField] private TMP_Text _priceMineralConteiner;

    [SerializeField] private Button _addHealth;
    [SerializeField] private Button _addDamage;
    [SerializeField] private Button _addBuildSpeed;
    [SerializeField] private Button _addCraftSpeed;
    [SerializeField] private Button _addMineralConteiner;

    [Inject] private IStateStorage _storage;
    [Inject] private LocalizationTranslate _localizationTranslate;
    [Inject] private MessageBox _messageBox;

    private void OnEnable()
    {
        _storage.StatsChanged += UpdateStats;

        _addHealth.onClick.AddListener(OnClickAddHealth);
        _addDamage.onClick.AddListener(OnClickAddDamage);
        _addBuildSpeed.onClick.AddListener(OnClickAddBuildSpeed);
        _addCraftSpeed.onClick.AddListener(OnClickAddCraftSpeed);
        _addMineralConteiner.onClick.AddListener(OnClickMineralConteiner);

        UpdateStats(_storage.GetStats());
    }

    private void OnDisable()
    {
        _storage.StatsChanged -= UpdateStats;

        _addHealth.onClick.RemoveListener(OnClickAddHealth);
        _addDamage.onClick.RemoveListener(OnClickAddDamage);
        _addBuildSpeed.onClick.RemoveListener(OnClickAddBuildSpeed);
        _addCraftSpeed.onClick.RemoveListener(OnClickAddCraftSpeed);
        _addMineralConteiner.onClick.RemoveListener(OnClickMineralConteiner);
    }

    private void OnClickAddCraftSpeed()
    {
        if (_storage.AddStat(ref _storage.GetStats().CraftSpeed))
            PurchaseCompleted();
        else
            PurchaseNotCompleted();
    }

    private void OnClickAddBuildSpeed()
    {
        if (_storage.AddStat(ref _storage.GetStats().BuildSpeed))
            PurchaseCompleted();
        else
            PurchaseNotCompleted();
    }

    private void OnClickAddDamage()
    {
        if (_storage.AddStat(ref _storage.GetStats().Damage))
            PurchaseCompleted();
        else
            PurchaseNotCompleted();
    }

    private void OnClickAddHealth()
    {
        if (_storage.AddStat(ref _storage.GetStats().Health))
            PurchaseCompleted();
        else
            PurchaseNotCompleted();
    }   

    private void OnClickMineralConteiner()
    {
        if (_storage.AddStat(ref _storage.GetStats().MaxMineralConteiner))
            PurchaseCompleted();
        else
            PurchaseNotCompleted();
    }

    private void PurchaseCompleted()
    {
        _messageBox.Show(_localizationTranslate.GetMessage(LocalizationMessageType.SkillUp));
    }

    private void PurchaseNotCompleted()
    {
        _messageBox.Show(_localizationTranslate.GetMessage(LocalizationMessageType.NotEnoughGold));
    }

    private void UpdateStats(UserStatsModel stats)
    {
        _health.text = stats.Health.ToString();
        _priceHealth.text = _storage.GetPurchaseGold(stats.Health).ToString();

        _damage.text = stats.Damage.ToString();
        _priceDamage.text = _storage.GetPurchaseGold(stats.Damage).ToString();

        _buildSpeed.text = stats.BuildSpeed.ToString();
        _priceBuildSpeed.text = _storage.GetPurchaseGold(stats.BuildSpeed).ToString();

        _craftSpeed.text = stats.CraftSpeed.ToString();
        _priceCraftSpeed.text = _storage.GetPurchaseGold(stats.CraftSpeed).ToString();

        _mineralConteiner.text = stats.MaxMineralConteiner.ToString();
        _priceMineralConteiner.text = _storage.GetPurchaseGold(stats.MaxMineralConteiner).ToString();
    }
}
