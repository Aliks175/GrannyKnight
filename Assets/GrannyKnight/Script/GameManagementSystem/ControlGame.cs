using UnityEngine;
using Zenject;

public class ControlGame : MonoBehaviour
{
    private GameManager _gameManager;

    [Inject]
    public void Construct(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void LoadMenu()
    {
        _gameManager.LoadMenu();
    }
}