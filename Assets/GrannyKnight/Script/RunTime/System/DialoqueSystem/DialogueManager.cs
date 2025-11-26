using UnityEngine;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI Elements")]
    [SerializeField]private GameObject _dialogueUI;
    [SerializeField] private TMP_Text _dialogueText, _nameText;

    private string[] _lines, _names;
    private int currentLine;

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
        _dialogueText.text = _lines[currentLine];
        _nameText.text = _names[currentLine];
    }

    private void ControlVisible(bool isVisible)
    {
        _dialogueUI.SetActive(isVisible);
    }
}