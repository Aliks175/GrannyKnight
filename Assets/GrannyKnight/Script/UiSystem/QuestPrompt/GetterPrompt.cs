using UnityEngine;
using Zenject;

public class GetterPrompt : MonoBehaviour
{
    private PromptManager _promptManager;

    [Inject]
    public void Construct(PromptManager dialogueManager)
    {
        _promptManager = dialogueManager;
    }

    public void SetPromptID(int Id)
    {
        _promptManager.GetPrompt(Id);
    }
}
