using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelNextButton : MonoBehaviour
{
    [Inject] private ILevelStorage _levelStorage;
    [Inject] private ICurrentLevelInfo _levelInfo;
    [Inject] private StateMashine _stateMashine;
    [Inject] private IFindLevel _findLevel;

    private Button _button;

    private void Awake() => _button = GetComponent<Button>();

    private void OnEnable() => _button.onClick.AddListener(OnClick);

    private void OnDisable() => _button.onClick.RemoveListener(OnClick);

    private void OnClick()
    {
        const int MaxStars = 3;

        LevelModel[] levels = _levelStorage.GetLevels();

        for (int i = _levelInfo.GetLevelNumber() - 1; i < levels.Length; i++)
        {
            if (levels[i].IsOpen == false)
                break;

            if (levels[i].Stars == MaxStars)
                continue;

            LevelTypeMode mode = (LevelTypeMode)levels[i].Stars;

            if (_findLevel.TryGetLevel(i, mode, out LevelMode level))
            {
                _stateMashine.EnterIn<LoadGameSceneState,LevelMode>(level);
            }

            return;
        }

        for (int i = _levelInfo.GetLevelNumber() - 1; i >= 0; i++)
        {
            if (levels[i].Stars == MaxStars)
                continue;

            LevelTypeMode mode = (LevelTypeMode)levels[i].Stars;

            if (_findLevel.TryGetLevel(i, mode, out LevelMode level))
            {
                _stateMashine.EnterIn<LoadGameSceneState, LevelMode>(level);
            }

            return;
        }
    }
}
