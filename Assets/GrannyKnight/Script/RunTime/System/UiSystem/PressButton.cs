using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PressButton : MonoBehaviour
{
    [SerializeField] private InputActionReference _button;
    [SerializeField] private float _fillTime = 0.4f;
    private Image _image;
    private bool _isPressed;

    void Awake()
    {
        ExtractHoldSettings(_button.action);
    }
    void OnEnable()
    {
        _image = GetComponent<Image>();
        _image.fillAmount = 1;
        _isPressed = false;
        _button.action.started += OnButtonPressed;
        _button.action.canceled += OnButtonReleased;
    }
    
    void OnDisable()
    {
        _button.action.started -= OnButtonPressed;
        _button.action.canceled -= OnButtonReleased;
    }
    
    private void OnButtonPressed(InputAction.CallbackContext context)
    {
        _isPressed = true;
    }
    
    private void OnButtonReleased(InputAction.CallbackContext context)
    {
        _isPressed = false;
        _image.fillAmount = 1f;
    }
    public void ExtractHoldSettings(InputAction inputAction)
    {
        _fillTime = 0.4f; // Значение по умолчанию из Input System

        if (inputAction == null) return;

        foreach (var interaction in inputAction.interactions)
        {
            string interactionStr = interaction.ToString();
            if (interactionStr.StartsWith("Hold"))
            {
                ParseHoldInteraction(interactionStr);
                break;
            }
        }
    }

    private void ParseHoldInteraction(string interactionString)
    {
        string parameters = interactionString
                .Replace("Hold(", "")
                .Replace(")", "");
            
        // Разделяем параметры
        string[] paramPairs = parameters.Split(',');
        
        foreach (var pair in paramPairs)
        {
            string[] keyValue = pair.Split('=');
            if (keyValue.Length == 2)
            {
                string key = keyValue[0].Trim();
                string value = keyValue[1].Trim();
                
                if (key == "duration" && float.TryParse(value, out float duration))
                {
                    _fillTime = duration;
                }
            }
        }
    }
    
    void Update()
    {
        if (_isPressed)
        {
            _image.fillAmount -= Time.deltaTime / _fillTime;
            if (_image.fillAmount <= 0)
                _image.fillAmount = 0;
        }
    }
}
