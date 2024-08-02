using Reflex.Attributes;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelView : MonoBehaviour
{
    const int MaxLevelMode = 3;

    [SerializeField] private TMP_Text _levelNumber;
    [SerializeField] private TMP_Text _needStars;
    [SerializeField] private TMP_Text _allStars;
    [SerializeField] private Image _levelIcon;

    [SerializeField] private Button _play;
    [SerializeField] private Button _next;
    [SerializeField] private Button _prev;

    [SerializeField] private GameModeButton[] _modeButtons = new GameModeButton[MaxLevelMode];

    [Inject] private ILevelStorage _userLevelStorage;
    [Inject] private ILevelInfo _levelInfo;
    [Inject] private IFindLevel _findLevel;
    [Inject] private StateMashine _stateMashine;
    [Inject] private LocalizationTranslate _localizationTranslate;
    [Inject] private MessageBox _messageBox;

    private LevelTypeMode _currentTypeMode;
    private LevelModel _currentLevelMode;
    private int _currentLevelIndex = 0;

    private void OnEnable()
    {
        _play.onClick.AddListener(PlayGame);
        _next.onClick.AddListener(OnClickNext);
        _prev.onClick.AddListener(OnClickPrevios);

        for (int i = 0; i < _modeButtons.Length; i++)
            _modeButtons[i].Changed += ChangeTypeMode;

        Initialize();
    }

    private void OnDisable()
    {
        _play.onClick.RemoveListener(PlayGame);
        _next.onClick.RemoveListener(OnClickNext);
        _prev.onClick.RemoveListener(OnClickPrevios);

        for (int i = 0; i < _modeButtons.Length; i++)
            _modeButtons[i].Changed -= ChangeTypeMode;
    }

    private void OnValidate()
    {
        if (_modeButtons.Length != MaxLevelMode)
            Array.Resize(ref _modeButtons, MaxLevelMode);

        try
        {
            Validate();
        }
        catch(Exception ex) 
        { 
            enabled = false;
            throw ex;
        }
    }

    public void Initialize()
    {
        _allStars.text = _userLevelStorage.GetAllStars().ToString();

        InitCurrentLevel();
        ShowCurrentLevel();
    }

    public void PlayGame()
    {
        if (_currentLevelMode.IsOpen == false)
        {
            _messageBox.Show(_localizationTranslate.GetMessage(LocalizationMessageType.NeedMoreStars));
            return;
        }

        if ((int)_currentTypeMode > (int)_currentLevelMode.OpenMode)
        {
            _messageBox.Show(_localizationTranslate.GetMessage(LocalizationMessageType.LevelModeClose));
            return;
        }

        if(_findLevel.TryGetLevel(_currentLevelMode.Id, _currentTypeMode, out LevelMode level))
        {
            _stateMashine.EnterIn<LoadGameSceneState, LevelMode>(level);
        }
    }

    private void Validate()
    {
        if (_levelNumber == null)
            throw new ArgumentNullException(nameof(_levelNumber));

        if (_needStars == null)
            throw new ArgumentNullException(nameof(_needStars));

        if (_play == null)
            throw new ArgumentNullException(nameof(_play));

        if (_next == null)
            throw new ArgumentNullException(nameof(_next));

        if (_prev == null)
            throw new ArgumentNullException(nameof(_prev));
    }

    private void InitCurrentLevel()
    {
        _currentLevelMode = _userLevelStorage.GetLevels().Last(level => level.IsOpen);
        _currentLevelIndex = _currentLevelMode.Id;
    }

    private void ChangeTypeMode(LevelTypeMode mode)
    {
        _currentTypeMode = mode;
        ChangeFocusMode(mode);
    } 

    private void InitButtonMods()
    {
        ChangeCompletedMode();
        ChangeShowMode();
        ChangeFocusMode(_currentLevelMode.OpenMode);
    }

    private void ChangeFocusMode(LevelTypeMode mode)
    {
        for (int i = 0; i < _modeButtons.Length; i++)
            _modeButtons[i].OnFocused(_currentLevelMode.IsOpen && mode == _modeButtons[i].Type);
    }

    private void ChangeCompletedMode()
    {
        for (int i = 0; i < _modeButtons.Length; i++)
            _modeButtons[i].OnCompleted(i < _currentLevelMode.Stars);
    }

    private void ChangeShowMode()
    {
        for (int i = 0; i < _modeButtons.Length; i++)
        {
            bool hasOpenMode = (int)_currentLevelMode.OpenMode >= (int)_modeButtons[i].Type;
            bool isShow = hasOpenMode && _currentLevelMode.IsOpen;

            //_modeButtons[i].SetInteracteble(isShow);
        }
    }

    private void OnClickNext()
    {
        _currentLevelIndex++;
        ShowCurrentLevel();
    }

    private void OnClickPrevios()
    {
        _currentLevelIndex--;
        ShowCurrentLevel();
    }

    private void ShowCurrentLevel()
    {
        _currentLevelMode = _userLevelStorage.GetLevels()[_currentLevelIndex];

        _needStars.text = _currentLevelMode.NeedStarForOpen.ToString();
        _levelNumber.text = $"{_currentLevelMode.Id + 1}";
        _currentTypeMode = (LevelTypeMode)_currentLevelMode.Stars;
        _levelIcon.sprite = _levelInfo.GetIcon(_currentLevelIndex);

        InitButtonMods();
        CheckShowNavigationBtn();
    }

    private void CheckShowNavigationBtn()
    {
        if ((_currentLevelIndex + 1) == _userLevelStorage.GetLevels().Length)
            _next.gameObject.SetActive(false);
        else
            _next.gameObject.SetActive(true);

        if ((_currentLevelIndex - 1) == -1)
            _prev.gameObject.SetActive(false);
        else 
            _prev.gameObject.SetActive(true);
    }
}
