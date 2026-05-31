using UnityEngine;
using Zenject;

public class ControlMenu : MonoBehaviour
{
    [SerializeField] private UISettings _uISettings;
    private GameManager _gameManager;

    [Inject]
    public void Construct(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    private void Start()
    {
        _uISettings.Initialization();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadGane()
    {
        _gameManager.LoadGame();
    }

    public void Exit()
    {
        _gameManager.Exit();
    }
}