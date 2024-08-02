using System;
using TMPro;
using UnityEngine;

public class LeaderboadElement : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerName;
    [SerializeField] private TMP_Text _playerScore;

    private void OnEnable()
    {
        try
        {
            Validate();
        }
        catch(ArgumentNullException ex)
        {
            enabled = false;
            throw ex;
        }
    }

    public void Init(LeaderboardPlayer player)
    {
        _playerName.text = player.Name;
        _playerScore.text = player.Score.ToString();
    }

    private void Validate()
    {
        if (_playerName == null)
            throw new ArgumentNullException(nameof(_playerName));

        if (_playerScore == null)
            throw new ArgumentNullException(nameof(_playerScore));
    }
}
