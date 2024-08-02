using Agava.YandexGames;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    private void Awake()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        YandexGamesSdk.GameReady();
#endif
    }
}
