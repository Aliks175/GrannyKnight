using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIGame : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private string _sceneToLoad;
    void Awake()
    {
        _pauseMenu.SetActive(false);
    }
    public void OnEscButton(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        if (_pauseMenu.activeSelf) ResumeGame();
        else PauseGame();
    }
    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        _pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void OnExitGameButton()
    {
        Application.Quit();
    }
    public void OnMenuButton()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(_sceneToLoad);
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
