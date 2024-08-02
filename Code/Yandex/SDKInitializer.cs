using Agava.YandexGames;
using IJunior.TypedScenes;
using System.Collections;
using UnityEngine;

public class SDKInitializer : MonoBehaviour
{
    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
        yield return YandexGamesSdk.Initialize(OnInitialized);
    }

    private void OnInitialized()
    {
        MainMenu.Load();
    }
}
