using Agava.YandexGames;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class YandexLederboard : MonoBehaviour
{
    private const string LeaderboardName = "Leaderboard";
    private const string AnonymousName = "Anonymous";
    private const int MaxPlayers = 6;

    [SerializeField] private LederboardView _leaderboadrView;

    private readonly List<LeaderboardPlayer> _leaderboardPlayers = new ();

    private void Awake()
    {
        Initialize();
        Fill();
    }

    private void OnValidate()
    {
        if (_leaderboadrView == null)
            throw new ArgumentNullException(nameof(_leaderboadrView));
    }

    private void Fill()
    {
        if(PlayerAccount.IsAuthorized == false)
            return;

        _leaderboardPlayers.Clear();

        Leaderboard.GetEntries(LeaderboardName, (result) =>
        {
            foreach (var entry in result.entries)
            {
                int rank = entry.rank;
                int score = entry.score;
                string name = entry.player.publicName;

                if (string.IsNullOrEmpty(name))
                    name = AnonymousName;

                _leaderboardPlayers.Add(new LeaderboardPlayer(rank, name, score));
            }

            _leaderboardPlayers.OrderByDescending(player => player.Rank).ToList();
            _leaderboadrView.Construct(_leaderboardPlayers.Take(MaxPlayers).ToList());
        });
    }

    private void Initialize()
    {
        if (PlayerAccount.IsAuthorized == false)
            return;

        PlayerAccount.RequestPersonalProfileDataPermission();
    }
}
