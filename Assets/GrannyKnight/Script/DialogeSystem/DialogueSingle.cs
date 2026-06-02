using UnityEngine;
using Zenject;

public class DialogueSingle : MonoBehaviour
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
    public async void StartDialogue(int dialogueId)
    {
        if (!System.Enum.IsDefined(typeof(FilePath), _dialoguePath))
        {
            return;
        }
        await dialogueManager.StartDialogue(path + _dialoguePath.ToString(), dialogueId.ToString());
    }
}

public enum FilePath
{
    RoomGG,
    SychQuest,
    GvinkaQuest,
    QapchaQuest,
    BAQuest,
    SkladQuest,
    AlchemiaQuest
}