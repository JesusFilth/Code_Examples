using Agava.YandexGames;
using UnityEngine;

public class LeaderboardAutorizationCheck : MonoBehaviour
{
    [SerializeField] private YandexLederboard _board;
    [SerializeField] private AutorizationView _authView;

    private void OnEnable()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if(PlayerAccount.IsAuthorized)
            _board.gameObject.SetActive(true);
        else
            _authView.gameObject.SetActive(true);
#endif
    }
}
