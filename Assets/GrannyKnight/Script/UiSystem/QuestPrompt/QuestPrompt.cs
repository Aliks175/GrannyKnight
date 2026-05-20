using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuestPrompt : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] protected RectTransform _backImage;
    [SerializeField] protected RectTransform _bodyText;
    [SerializeField] protected TextMeshProUGUI _textPrompt;
    [SerializeField] protected float _endPosition;
    private Vector2 _bodyTextStartPos;
    private Vector2 _bodyTextEndPos;
    private Tween _open;
    private Tween _close;
    private Tween _shake;
    private bool _isVisible;

    private void Start()
    {
        _bodyTextStartPos = _bodyText.anchoredPosition;
        _bodyTextEndPos = _bodyTextStartPos;
        _bodyTextEndPos.y = _endPosition;
        _isVisible = false;

        _open = _bodyText.DOAnchorPos(_bodyTextStartPos, 1.2f)
                .SetEase(Ease.OutBack)
                .From(_bodyTextEndPos)
                .SetAutoKill(false);

        _close = _bodyText.DOAnchorPos(_bodyTextEndPos, 1f)
               .SetEase(Ease.InSine)
               .From(_bodyTextStartPos)
               .SetAutoKill(false);

        _shake = _backImage.DOShakeAnchorPos(0.5f, 20, 10)
                .SetAutoKill(false);
    }

    private void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            if (_isVisible)
            {
                _open.Restart();
            }
            else
            {
                _close.Restart();
            }
            if (!_shake.IsPlaying())
            {
                _shake.Restart();
            }
            _isVisible = !_isVisible;
        }
    }

    public void SetText(string tempText)
    {
        _textPrompt.SetText(tempText);
    }
}