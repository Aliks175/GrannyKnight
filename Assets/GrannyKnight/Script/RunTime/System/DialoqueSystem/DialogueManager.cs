using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;
using System.Threading;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI Elements")]
    [SerializeField]private GameObject _dialogueUI;
    [SerializeField] private TMP_Text _dialogueText, _nameText;
    [SerializeField] private float _delayBetweenChars = 0.05f;
    [SerializeField] private float _delayAfterPunctuation = 0.5f;

    private string[] _lines, _names;
    private int currentLine;
    private string _fullText;
    private bool _isTyping = false;
    private CancellationTokenSource _typingCancellationTokenSource;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        _dialogueUI.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        _lines = new string[dialogue.Lines.Length];
        for (int i = 0; i < dialogue.Lines.Length; i++)
        {
            _lines[i] = dialogue.Lines[i].Line;
        }
        _names = new string[dialogue.Lines.Length];
        for (int i = 0; i < dialogue.Lines.Length; i++)
        {
            _names[i] = dialogue.Lines[i].Character.ToString();
        }
        currentLine = 0;
        ControlVisible(true);
        UpdateDialogue();
    }

    public void HideDialog()
    {
        ControlVisible(false);
    }

    public void ShowDialog()
    {
        ControlVisible(true);
    }

    public void NextLine()
    {
        currentLine++;
        if (currentLine < _lines.Length)
        {
            UpdateDialogue();
        }
        else
        {
            ControlVisible(false);
        }
    }

    private void UpdateDialogue()
    {
        _nameText.text = _names[currentLine];
        _fullText = _lines[currentLine];
        if (!_isTyping) StartTyping();
        else NextText();
    }
    public void StartTyping()
    {
        if (!_isTyping)
        {
            _typingCancellationTokenSource?.Cancel();
            _typingCancellationTokenSource = new CancellationTokenSource();
            TypeText(_typingCancellationTokenSource.Token).Forget();
        }
    }
    private async UniTaskVoid TypeText(CancellationToken cancellationToken)
    {
        _isTyping = true;
        _dialogueText.text = _fullText;
        _dialogueText.maxVisibleCharacters = 0;
        _dialogueText.ForceMeshUpdate();

        int totalCharacters = _fullText.Length;
        
        for (int i = 0; i <= totalCharacters; i++)
        {
            if (cancellationToken.IsCancellationRequested) return;
            
            _dialogueText.maxVisibleCharacters = i;
            
            // Проверяем текущий символ на пунктуацию для добавления задержки
            if (i > 0 && i < totalCharacters)
            {
                char currentChar = _fullText[i - 1]; 
                if (currentChar == '.' || currentChar == '!' || currentChar == '?')
                {
                    await UniTask.Delay((int)(_delayAfterPunctuation * 1000), cancellationToken: cancellationToken);
                }
            }
            
            await UniTask.Delay((int)(_delayBetweenChars * 1000), cancellationToken: cancellationToken);
        }
        
        _isTyping = false;
    }
    public void SkipTyping()
    {
        if (_isTyping)
        {
            _typingCancellationTokenSource?.Cancel();
            _dialogueText.maxVisibleCharacters = _dialogueText.textInfo.characterCount;
            _isTyping = false;
        }
    }
    public void NextText()
    {
        _typingCancellationTokenSource?.Cancel();
        _typingCancellationTokenSource = new CancellationTokenSource();
        TypeText(_typingCancellationTokenSource.Token).Forget();
    }

    private void ControlVisible(bool isVisible)
    {
        _dialogueUI.SetActive(isVisible);
    }
}