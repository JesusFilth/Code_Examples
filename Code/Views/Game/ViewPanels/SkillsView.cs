using Reflex.Attributes;
using UnityEngine;
using System;

[RequireComponent(typeof(CanvasGroup))]
public class SkillsView : MonoBehaviour, IGameLevelView
{
    [SerializeField] private SkillItemView _firstSkill;
    [SerializeField] private SkillItemView _secondSkill;

    private CanvasGroup _canvasGroup;

    [Inject] private SkillStorage _skillStorage;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        Hide();
    }

    public void Hide()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;

        Time.timeScale = 1;
    }

    public void Show()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;

        Time.timeScale = 0;

        UpdateData();
    }

    private void UpdateData()
    {
        const int MaxSkillCount = 2;
        const int FirstSkillIndex = 0;
        const int SecondSkillIndex = 1;

        Skill[] skills = _skillStorage.GetTwoSkills();

        if (skills == null || skills.Length != MaxSkillCount)
            throw new InvalidOperationException(nameof(skills));

        _firstSkill.Init(skills[FirstSkillIndex]);
        _secondSkill.Init(skills[SecondSkillIndex]);
    }
}
