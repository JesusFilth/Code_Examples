using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TampleProgressView : MonoBehaviour, ITempleProgressView
{
    [SerializeField] private Slider _progress;
    [SerializeField] private TMP_Text _textPercent;
    [SerializeField] private TMP_Text _countCurrentMineral;
    [SerializeField] private Image _currentMineralIcon;
    [SerializeField] private Image _nextMineralIcon;
    [SerializeField] private MeneralIconSetting _mineralIcons;

    private void OnValidate()
    {
        if (_progress == null)
            throw new ArgumentNullException(nameof(_progress));

        if (_textPercent == null)
            throw new ArgumentNullException(nameof(_textPercent));

        if (_countCurrentMineral == null)
            throw new ArgumentNullException(nameof(_countCurrentMineral));

        if (_mineralIcons == null)
            throw new ArgumentNullException(nameof(_mineralIcons));

        if (_currentMineralIcon == null)
            throw new ArgumentNullException(nameof(_currentMineralIcon));

        if (_nextMineralIcon == null)
            throw new ArgumentNullException(nameof(_nextMineralIcon));
    }

    public void ChangeBuildProgressBar(float percent, int blockCount)
    {
        _progress.value = percent;
        _textPercent.text = $"{_progress.value}%";

        _countCurrentMineral.text = blockCount.ToString();
    }

    public void ChangeCurrentMineral(MineralType type)
    {
        _currentMineralIcon.sprite = _mineralIcons.GetIcon(type);
    }

    public void ChangeNextMineral(bool hasMineral, MineralType type)
    {
        if (hasMineral == false)
        {
            _nextMineralIcon.enabled = false;
            return;
        }

        _nextMineralIcon.sprite = _mineralIcons.GetIcon(type);
    }
}
