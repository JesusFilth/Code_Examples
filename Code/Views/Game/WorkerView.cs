using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorkerView : MonoBehaviour, IWorkerView
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Button _click;
    [SerializeField] private Image _fill;
    [SerializeField] private TMP_Text _countText;

    private IWorkerProcess _currentWorkProcess;

    private void Awake()
    {
        Hide();
    }

    private void OnValidate()
    {
        if (_click == null)
            throw new ArgumentNullException(nameof(_click));

        if (_canvasGroup == null)
            throw new ArgumentNullException(nameof(_canvasGroup));

        if (_fill == null)
            throw new ArgumentNullException(nameof(_fill));
    }

    private void OnEnable()
    {
        _click.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _click.onClick.RemoveListener(OnClick);
    }

    public void Binding(IWorkerProcess workerProcess)
    {
        Show();

        _currentWorkProcess = workerProcess;
        _currentWorkProcess.ChangeProgress += ChangeProgressBar;
        _currentWorkProcess.ChangeCount += ChangeCount;
    }

    public void Unbinding()
    {
        if (_currentWorkProcess == null)
            return;

        Hide();

        _currentWorkProcess.ChangeProgress -= ChangeProgressBar;
        _currentWorkProcess.ChangeCount -= ChangeCount;
        _currentWorkProcess = null;
    }

    private void Show()
    {
        if (_canvasGroup == null)
            return;

        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }

    private void Hide()
    {
        if (_canvasGroup == null)
            return;

        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }

    private void ChangeCount(int count)
    {
        _countText.text = count.ToString();
    }

    private void ChangeProgressBar(float value)
    {
        _fill.fillAmount = value;
    }

    private void OnClick()
    {
        if (_currentWorkProcess == null)
            throw new ArgumentNullException(nameof(_currentWorkProcess));

        _currentWorkProcess.ToWork();
    }
}
