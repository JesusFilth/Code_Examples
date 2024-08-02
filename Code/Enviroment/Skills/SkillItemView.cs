using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using Reflex.Attributes;


public class SkillItemView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Button _button;

    private Skill _skill;

    [Inject] IPlayerStats _playerStats;
    [Inject] GameUIStateMashine _gameUIState;
    [Inject] LocalizationTranslate _translate;

    private void OnEnable()
    {
        try
        {
            Validate();
        }
        catch(ArgumentNullException ex) 
        {
            throw ex;
        }

        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    public void Init(Skill skill)
    {
        _skill = skill;

        _icon.sprite = _skill.Icon;
        _name.text = $"+{_skill.Value} {_translate.GetStatName(_skill.Type)}";
    }

    private void OnClick()
    {
        _skill.ActiveSkill(_playerStats.GetStats());
        _gameUIState.EnterIn<GameUIState>();
    }

    private void Validate()
    {
        if (_icon == null)
            throw new ArgumentNullException(nameof(_icon));

        if (_name == null)
            throw new ArgumentNullException(nameof(_name));

        if (_button == null)
            throw new ArgumentNullException(nameof(_button));
    }
}
