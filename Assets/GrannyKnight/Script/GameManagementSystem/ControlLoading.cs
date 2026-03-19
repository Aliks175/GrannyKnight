using UnityEngine;
using Zenject;

public class ControlLoading : MonoBehaviour
{
    private GameManager _gameManager;

    [Inject]
    public void Construct(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void LoadGame()
    {
        _gameManager.LoadGame();
        //_testLoading.LoadSingle(ListScene.Game);
    }

    public void LoadMenu()
    {
        _gameManager.LoadMenu();
        //_testLoading.LoadSingle(ListScene.Menu);
    }

    public void LoadFreeGame()
    {
        _gameManager.LoadFreeGame();
    }

    public void Exit()
    {
        Application.Quit();
    }
}