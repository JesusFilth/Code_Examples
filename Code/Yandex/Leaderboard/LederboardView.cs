using System.Collections.Generic;
using UnityEngine;

public class LederboardView : MonoBehaviour
{
    [SerializeField] private Transform _conteinerTop3;
    [SerializeField] private Transform _conteinerTop6;

    public void Construct(List<LeaderboardPlayer> leaderboardPlayers)
    {
        const int Top3 = 3;
        const int Top6 = 6;

        Clear();

        int count = 0;
      
        for (int i = 0; i < leaderboardPlayers.Count; i++)
        {
            if (count == Top3)
                break;

            if (_conteinerTop3.GetChild(i).gameObject.TryGetComponent(out LeaderboadElement element))
            {
                element.gameObject.SetActive(true);
                element.Init(leaderboardPlayers[i]);
            }

            count++;
        }


        if (leaderboardPlayers.Count <= Top3)
            return;

        int indexElement = 0;

        for (int i = count; i < leaderboardPlayers.Count; i++)
        {
            if (count == Top6)
                break;

            if (_conteinerTop3.GetChild(indexElement).gameObject.TryGetComponent(out LeaderboadElement element))
            {
                element.gameObject.SetActive(true);
                element.Init(leaderboardPlayers[i]);
            }

            indexElement++;
            count++;
        }
    }

    private void Clear()
    {
        for (int i = 0; i < _conteinerTop3.childCount; i++)
        {
            _conteinerTop3.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < _conteinerTop6.childCount; i++)
        {
            _conteinerTop6.GetChild(i).gameObject.SetActive(false);
        }
    }
}
