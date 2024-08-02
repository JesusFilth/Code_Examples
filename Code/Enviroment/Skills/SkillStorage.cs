using UnityEngine;
using System;
using Random = UnityEngine.Random;
using Reflex.Attributes;

public class SkillStorage : MonoBehaviour
{
    private const int MaxGetSkill = 2;

    [SerializeField] private Skill[] _skills;

    [Inject] private ILevelSkillSetting _levelSetting;

    private void Awake()
    {
        Initialize();
    }

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
    }

    public Skill[] GetTwoSkills()
    {
        const int MinValue = 1;

        Skill[] skills = GetTwoRandomSkills();

        foreach (Skill skill in skills)
        {
            skill.SetValue(Random.Range(MinValue,_levelSetting.GetMaxValue()));
        }

        return skills;
    }

    private Skill[] GetTwoRandomSkills() 
    {
        int firstIndex = Random.Range(0,_skills.Length);
        int secondIndex;

        do
        {
            secondIndex = Random.Range(0, _skills.Length);

        } while (firstIndex == secondIndex);


        return new Skill[] { _skills[firstIndex], _skills[secondIndex] };
    }

    private void Initialize()
    {
        for (int i = 0; i < _skills.Length; i++)
        {
            _skills[i] = Instantiate(_skills[i]);
        }
    } 

    private void Validate()
    {
        if (_skills.Length < MaxGetSkill)
            throw new InvalidOperationException(nameof(_skills));
    }
}