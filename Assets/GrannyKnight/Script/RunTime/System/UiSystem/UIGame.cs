using UnityEngine;
using UnityEngine.Rendering;

public class UIGame : MonoBehaviour
{
    [SerializeField] private UISettings _settings;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private ControlLoading _sceneToLoad;
    [SerializeField] private InputControl _inputControl;
    [SerializeField] private PlayerLook _playerLook;

    private bool _isPaused = false;

    private void Awake()
    {
        _pauseMenu.SetActive(false);
        _settings.gameObject.SetActive(false);

        _settings.OnChangeSensity += ChangeSensity;
    }

    private void OnDisable()
    {
        _settings.OnChangeSensity -= ChangeSensity;
    }

    public void Start()
    {
        _settings.Initialization();
    }

    public void ControlStateGame()
    {
        if (_isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        _pauseMenu.SetActive(true);
        _settings.gameObject.SetActive(false);
        _inputControl.ControlMovePlayer(false);
        _isPaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _pauseMenu.SetActive(false);
        _settings.gameObject.SetActive(false);
        _inputControl.ControlMovePlayer(true);
        _isPaused = false;
        Time.timeScale = 1f;
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
        _settings.gameObject.SetActive(true);
    }

    public void OnBackButton()
    {
        _pauseMenu.SetActive(true);
        _settings.gameObject.SetActive(false);
    }

    private void ChangeSensity()
    {
        _playerLook.Sensitivity = PlayerPrefs.GetFloat(SaveName.Sensitivity.ToString(), 1f);
    }
}