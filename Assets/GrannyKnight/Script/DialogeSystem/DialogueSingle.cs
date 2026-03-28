using UnityEngine;
using Zenject;
using Cysharp.Threading.Tasks;

public class DialogueSingle : MonoBehaviour
{
    [SerializeField] private DialogueID _dialogueID;
    private DialogueManager dialogueManager;

    [Inject]
    public void Construct(DialogueManager dialogueManager)
    {
        this.dialogueManager = dialogueManager;
    }

    [Tooltip("метод начала диалога")]
    public async void StartDialogue()
    {
        if (_dialogueID == null)
        {
            return;
        }
        await dialogueManager.StartDialogue(_dialogueID);
    }

}