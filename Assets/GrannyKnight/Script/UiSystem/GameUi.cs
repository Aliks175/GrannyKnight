using UnityEngine;
using Zenject;

public class GameUi : MonoBehaviour
{
    [SerializeField] private UISettings _settings;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _background;
    private GameManager _gameManager;
    //[SerializeField] private ControlLoading _sceneToLoad;
    //[SerializeField] private InputControl _inputControl;
    //[SerializeField] private PlayerLook _playerLook;

    private bool _isPaused = false;
    private SystemBuss _systemBuss;

    [Inject]
    public void Construct(SystemBuss systemBuss, GameManager gameManager)
    {
        _systemBuss = systemBuss;
        _gameManager = gameManager;
    }

    //private void Awake()
    //{
    //    _settings.OnChangeSensity += ChangeSensity;
    //}

    //private void OnDisable()
    //{
    //    _settings.OnChangeSensity -= ChangeSensity;
    //}

    public void Start()
    {
        ControlPause(false);
        _settings.Initialization();
        _systemBuss.ConstructGameUi(this);
        //Debug.Log("Start");
    }

    public void OnPause()
    {
        //Debug.Log("OnPause");
        _isPaused = !_isPaused;
        if (_isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        ControlPause(true);
        //Debug.Log("PauseGame");
    }

    public void ResumeGame()
    {
        ControlPause(false);
       
        //Debug.Log("ResumeGame");
    }

    public void OnMenuButton()
    {
        ControlPause(true);
        Time.timeScale = 1.0f;
        _gameManager.LoadMenu();
    }

    public void ExitGame()
    {
        _gameManager.Exit();
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
        //_playerLook.Sensitivity = PlayerPrefs.GetFloat(SaveName.Sensitivity.ToString(), 1f);
    }

    private void ControlPause(bool isActive)
    {
        if (isActive)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            _pauseMenu.SetActive(true);
            _background.SetActive(true);
            _settings.gameObject.SetActive(false);
            _isPaused = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1.0f;
            _pauseMenu.SetActive(false);
            _background.SetActive(false);
            _settings.gameObject.SetActive(false);
            _isPaused = false;
        }
    }
}