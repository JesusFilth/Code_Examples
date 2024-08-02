using IJunior.TypedScenes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuNavigation : MonoBehaviour
{
    [SerializeField] private GameObject[] _screens;

    private void Awake()
    {
        ShowScreen(ScreenMainMenu.Main);
    }

    public void ShowScreen(ScreenMainMenu screen)
    {
        if (_screens == null || _screens.Length == 0)
            return;

        for (int i = 0; i < _screens.Length; i++)
        {
            _screens[i].SetActive(i==(int)screen);
        }
    }

    public void ToMain() => ShowScreen(ScreenMainMenu.Main);

    public void ToLevels() => ShowScreen(ScreenMainMenu.Levels);

    public void ToStats() => ShowScreen(ScreenMainMenu.Stats);

    public void ToShop() => ShowScreen(ScreenMainMenu.Shop);

    public void ToLeaders() => ShowScreen(ScreenMainMenu.Leaders);
}
