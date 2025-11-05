using TMPro;
using UnityEngine;

public class PlayerUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textDescription;

    public void UpdateText(string text)
    {
        if (_textDescription.text == text) return;
        _textDescription.text = text;
    }
}