using System;
using UnityEngine;

public class GameManagementSystem : MonoBehaviour
{
    [SerializeField] private ControlLoading _controlLoading;

    // У нас должна быть озвучка инвентовя
    // звуки эмбиента
    // музыка работает при переходе из меню в игру
    // настройки громкости сохраняются при переходе в игру мб Player Prefs

    private void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        _controlLoading.Initialization();
    }

    public void StartGame()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ChangeFullScreen(Boolean isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        Debug.Log($"Result = {isFullScreen}");
    }
}