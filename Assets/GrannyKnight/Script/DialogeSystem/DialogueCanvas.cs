using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueCanvas : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text _speakerText;
    [SerializeField] private CanvasGroup _skipCanvas;
    [Header("Настройка")]
    [SerializeField] private bool _animText = true;
    [SerializeField] private float _typingSpeed = 50f;
    [SerializeField] private float _visTime = 3f;
    [SerializeField] private InputActionReference _buttonSkip;
    private bool _slipRequested = false;
    private bool _skipVis = false;
    private CanvasGroup _canvasGroup;
    private CancellationTokenSource _skipCancellationToken;

    public event Action OnSkip;
    private DialogueManager _dialogueManager;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
        _skipCanvas.alpha = 0;
    }

    private void OnEnable()
    {
        _buttonSkip.action.started += ShowSkipOrSkip;
    }

    private void OnDisable()
    {
        _buttonSkip.action.started -= ShowSkipOrSkip;
        _skipCancellationToken?.Cancel();
        _skipCancellationToken?.Dispose();
        _skipCancellationToken = null;
    }

    public async UniTask ShowLine(string speaker, string text)
    {
        _slipRequested = false;
        _speakerText.text = "";
        if (_animText)
            await TypeText(speaker, text);
        else
            TypeTextIm(speaker, text);
    }
    private async UniTask TypeText(string speaker, string text)
    {
        _speakerText.text = speaker;

        float delay = 1f / _typingSpeed;

        for (int i = 0; i < text.Length; i++)
        {
            if (_slipRequested)
            {
                _speakerText.text = speaker + text;
                break;
            }

            _speakerText.text = speaker + ": " + text.Substring(0, i + 1);


            await UniTask.Delay((int)(delay * 1000));
        }

    }
    private void TypeTextIm(string speaker, string text)
    {
        _speakerText.text = speaker + text;
    }
    public void Hide()
    {
        if (_canvasGroup != null)
        {
            _canvasGroup.alpha = 0;
        }
    }

    public void Show()
    {
        _skipCanvas.alpha = 0;
        _canvasGroup.alpha = 1;
    }

    public void Skip()
    {
        _slipRequested = true;
        OnSkip?.Invoke();
        //Debug.Log("Skip");
        //_dialogueManager.SkipLine();
    }

    public void ShowSkipOrSkip(InputAction.CallbackContext context)
    {
        if (_skipCanvas.alpha > 0)
        {
            Skip();
        }
        else
        {
      
            ShowSkipCanvas().Forget();
        }
    }

    private async UniTaskVoid ShowSkipCanvas()
    {
        //Debug.Log("ShowSkipCanvas");
        _skipCancellationToken?.Cancel();
        _skipCancellationToken = new CancellationTokenSource();

        _skipCanvas.alpha = 1;

        await UniTask.Delay((int)(_visTime * 1000), cancellationToken: _skipCancellationToken.Token);
        _skipCanvas.alpha = 0;
    }

}
