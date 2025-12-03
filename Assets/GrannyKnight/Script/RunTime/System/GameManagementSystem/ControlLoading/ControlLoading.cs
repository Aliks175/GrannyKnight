using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlLoading : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadSceneAsync((int)ListScene.Game, LoadSceneMode.Single);
    }

    public void LoadMenu()
    {
        SceneManager.LoadSceneAsync((int)ListScene.Menu, LoadSceneMode.Single);
    }
    public void LoadFreeGame()
    {
        SceneManager.LoadSceneAsync((int)ListScene.FreeGame, LoadSceneMode.Single);
    }

    public void Exit()
    {
        Application.Quit();
    }
}

public enum ListScene
{
    Menu = 0,
    Game = 1,
    FreeGame = 2
}