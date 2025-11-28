using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIGame : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private ControlLoading _sceneToLoad;
    [SerializeField] private InputControl _inputControl;

    private void Awake()
    {
        _pauseMenu.SetActive(false);
    }

    //public void OnEscButton(InputAction.CallbackContext context)
    //{
    //    if (!context.performed) return;

    //    if (_pauseMenu.activeSelf) ResumeGame();
    //    else PauseGame();
    //}

    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        _pauseMenu.SetActive(true);
        _inputControl.ControlMovePlayer(false);
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _pauseMenu.SetActive(false);
        _inputControl.ControlMovePlayer(true);
    }

    public void OnMenuButton()
    {
        Cursor.lockState = CursorLockMode.None;
        _sceneToLoad.LoadMenu();
    }

    public void ExitGame()
    {
        _sceneToLoad.Exit();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_pauseMenu.activeSelf) ResumeGame();
            else PauseGame();
        }
    }
}