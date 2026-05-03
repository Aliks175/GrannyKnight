using UnityEngine;
using Zenject;
using Cysharp.Threading.Tasks;

public class DialogueSingle : MonoBehaviour
{
    [SerializeField] private FilePath _dialoguePath;
    [Range(0,15)]public int DialogueId;
    private string path = "Dialogues/";
    private DialogueManager dialogueManager;

    [Inject]
    public void Construct(DialogueManager dialogueManager)
    {
        this.dialogueManager = dialogueManager;
    }

    [Tooltip("метод начала диалога")]
    public async void StartDialogue()
    {
        if (!System.Enum.IsDefined(typeof(FilePath), _dialoguePath))
        {
            return;
        }
        await dialogueManager.StartDialogue(path + _dialoguePath.ToString(), DialogueId.ToString());
    }
}

public enum FilePath
{
    RoomGG
}