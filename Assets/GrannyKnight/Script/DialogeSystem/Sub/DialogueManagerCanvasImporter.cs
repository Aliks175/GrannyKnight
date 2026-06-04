using System;
using Zenject;

public class DialogueManagerCanvasImporter : IInitializable , IDisposable
{
    private DialogueManager _dialogueManager;
    private SystemBuss _systemBuss;

    public DialogueManagerCanvasImporter(DialogueManager dialogueManager, SystemBuss systemBuss)
    {
        _dialogueManager = dialogueManager;
        _systemBuss = systemBuss;
    }

    public void Initialize()
    {
        _systemBuss.OnConstructPlayerUi += OnConstructPlayerUi;
        _dialogueManager.OnEndDialog += OnEndDialog;
    }

    public void Dispose()
    {
        _systemBuss.OnConstructPlayerUi -= OnConstructPlayerUi;
        _dialogueManager.OnEndDialog -= OnEndDialog;
    }

    private void OnEndDialog()
    {
        _systemBuss.EndDialog();
    }

    private void OnConstructPlayerUi(PlayerUi obj)
    {
        _systemBuss.OnConstructPlayerUi -= OnConstructPlayerUi;
        _dialogueManager.Construct(obj.DialogueCanvas);
    }
}