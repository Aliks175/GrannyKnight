using UnityEngine;

public class ControlMenu : MonoBehaviour
{
    [SerializeField] private UISettings _uISettings;

    private void Start()
    {
        _uISettings.Initialization();
        Cursor.lockState = CursorLockMode.None;
    }
}