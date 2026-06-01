using UnityEngine;
using Zenject;

public class DIalogueQuestEnding : MonoBehaviour
{
    [SerializeField] private FilePath _dialoguePath;
    private string path = "Dialogues/";
    private DialogueManager dialogueManager;

    [Inject]
    public void Construct(DialogueManager dialogueManager)
    {
        this.dialogueManager = dialogueManager;
    }

    [Tooltip("метод начала диалога")]
    public async void StartDialogue(DialogueQuestEnd ending)
    {
        if (!System.Enum.IsDefined(typeof(FilePath), _dialoguePath))
        {
            return;
        }
        await dialogueManager.StartDialogue(path + _dialoguePath.ToString(), ending.ToString());
    }
}
public enum DialogueQuestEnd
{
    good,
    mid,
    bad
}
