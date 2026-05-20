using Zenject;

public class DialogueManagerCanvasImporter : IInitializable
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
    }

    private void OnConstructPlayerUi(PlayerUi obj)
    {
        _systemBuss.OnConstructPlayerUi -= OnConstructPlayerUi;
        _dialogueManager.Construct(obj.DialogueCanvas);
    }
}