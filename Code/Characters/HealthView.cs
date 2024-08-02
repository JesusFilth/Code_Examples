using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class HealthView : MonoBehaviour 
{
    [SerializeField] private LookAtConstraint _lookAtConstraint;
    [SerializeField] private Image _healFilling;
    [SerializeField] private Stats _stats;
    [SerializeField] private float _maxDeltaChange = 0.07f;

    private IEnumerator _changingHealsBarView;
    private float _targePercentValue = 0;

    private void Awake()
    {
        _lookAtConstraint.AddSource(new ConstraintSource { sourceTransform = Camera.main.transform, weight = 1 });
    }

    private void OnEnable()
    {
        try
        {
            Validate();
        }
        catch (Exception ex)
        {
            enabled = false;
            throw ex;
        }

        _stats.HealthChanged += ChangeValue;
        _stats.Died += Hide;
    }

    private void OnDisable()
    {
        _stats.HealthChanged -= ChangeValue;
        _stats.Died -= Hide;

        if (_changingHealsBarView != null)
        {
            StopCoroutine(_changingHealsBarView);
            _changingHealsBarView = null;
        }
    }

    private void Validate()
    {
        if (_healFilling == null)
            throw new ArgumentNullException(nameof(_healFilling));

        if (_stats == null)
            throw new ArgumentNullException(nameof(_stats));

        if (_lookAtConstraint == null)
            throw new ArgumentNullException(nameof(_lookAtConstraint));
    }

    private void ChangeValue(float valuePercent)
    {
        _targePercentValue = valuePercent;

        if (_changingHealsBarView == null)
        {
            _changingHealsBarView = ChangingTargetValue();
            StartCoroutine(_changingHealsBarView);
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator ChangingTargetValue()
    {
        while (_healFilling.fillAmount != _targePercentValue)
        {
            _healFilling.fillAmount = Mathf.MoveTowards(_healFilling.fillAmount, _targePercentValue, _maxDeltaChange);
            yield return null;
        }

        _changingHealsBarView = null;
    }
}
