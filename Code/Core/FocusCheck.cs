using UnityEngine;
using Agava.WebUtility;

public class FocusCheck : MonoBehaviour
{
    private void OnEnable()
    {
        Application.focusChanged += OnInBackgroundChangeApp;
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeWeb;
    }

    private void OnDisable()
    {
        Application.focusChanged -= OnInBackgroundChangeApp;
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeWeb;
    }

    private void MuteAudio(bool value)
    {
        AudioListener.volume = value ? 0 : 1;
    }

    private void PauseGame(bool value)
    {
        Time.timeScale = value ? 0 : 1;
    }

    private void OnInBackgroundChangeApp(bool value)
    {
        MuteAudio(!value);
        PauseGame(!value);
    }

    private void OnInBackgroundChangeWeb(bool value)
    {
        MuteAudio(value);
        PauseGame(value);
    }
}
