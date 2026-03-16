using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textDescription;
    [SerializeField] private Image _cursor;

    private SystemBuss _systemBuss;

    [Inject]
    public void Construct(SystemBuss systemBuss)
    {
        _systemBuss = systemBuss;
    }

    private void Start()
    {
        _systemBuss.ConstructPlayerUi(this);
    }

    public void UpdateText(string text)
    {
        if (_textDescription.text == text) return;
        _textDescription.text = text;
    }

    public void ControlVisibleCursor(bool visible)
    {
        _cursor.enabled = visible;
    }
}