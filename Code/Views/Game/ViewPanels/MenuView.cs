using Reflex.Attributes;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class MenuView : MonoBehaviour, IGameLevelView
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _levelNumber;

    [SerializeField] private LevelModeIcon[] _modeIcons;

    private CanvasGroup _canvasGroup;

    [Inject] private ICurrentLevelInfo _levelInfo;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        Hide();
    }

    public void Show()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;

        Time.timeScale = 0;

        UpdateData(_levelInfo.GetLevelType(), _levelInfo.GetLevelNumber());
    }

    public void Hide()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;

        Time.timeScale = 1;
    }

    private void UpdateData(LevelTypeMode levelType, int levelNumber)
    {
        ChangeModeIcon(levelType);
        _levelNumber.text = $"{levelNumber}";
    }

    private void ChangeModeIcon(LevelTypeMode mode)
    {
        Sprite icon = _modeIcons.FirstOrDefault(lvlMode => lvlMode.Type == mode).Icon;

        if (icon != null)
            _icon.sprite = icon;
    }
}
