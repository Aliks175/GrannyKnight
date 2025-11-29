using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGame : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu, _settingsMenu;
    [SerializeField] private ControlLoading _sceneToLoad;
    [SerializeField] private InputControl _inputControl;
    private bool _isPaused = false;

    private void Awake()
    {
        _pauseMenu.SetActive(false);
        _settingsMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
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
        _settingsMenu.SetActive(false);
        _inputControl.ControlMovePlayer(false);
        _isPaused = true;
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _pauseMenu.SetActive(false);
        _settingsMenu.SetActive(false);
        _inputControl.ControlMovePlayer(true);
        _isPaused = false;
    }

    public void OnMenuButton()
    {
        Cursor.lockState = CursorLockMode.None;
        _sceneToLoad.LoadMenu();
    }

    public void ExitGame()
    {
        _sceneToLoad.Exit();
        _isPaused = false;
    }
    public void OnSettingsButton()
    {
        _pauseMenu.SetActive(false);
        _settingsMenu.SetActive(true);
    }
    public void OnBackButton()
    {
        _pauseMenu.SetActive(true);
        _settingsMenu.SetActive(false);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused) ResumeGame();
            else PauseGame();
        }
    }
}