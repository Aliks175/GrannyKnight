using UnityEngine;

public class UIGame : MonoBehaviour
{
    [SerializeField] private UISettings _settings;

    [SerializeField] private GameObject _pauseMenu;//, _settingsMenu;
    [SerializeField] private ControlLoading _sceneToLoad;
    [SerializeField] private InputControl _inputControl;
    private bool _isPaused = false;

    private void Awake()
    {
        _pauseMenu.SetActive(false);
        _settings.gameObject.SetActive(false);
    }

    //public void OnEscButton(InputAction.CallbackContext context)
    //{
    //    if (!context.performed) return;

    //    if (_pauseMenu.activeSelf) ResumeGame();
    //    else PauseGame();
    //}


    public void Start()
    {
        _settings.Initialization();
    }

    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        _pauseMenu.SetActive(true);
        _settings.gameObject.SetActive(false);
        _inputControl.ControlMovePlayer(false);
        _isPaused = true;
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _pauseMenu.SetActive(false);
        _settings.gameObject.SetActive(false);
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
        _settings.gameObject.SetActive(true);
    }

    public void OnBackButton()
    {
        _pauseMenu.SetActive(true);
        _settings.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused) ResumeGame();
            else PauseGame();
        }
    }
}