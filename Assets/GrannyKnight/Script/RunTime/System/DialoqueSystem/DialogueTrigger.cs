using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue _dialoque;

    public void Active()
    {
        DialogueManager.Instance.StartDialogue(_dialoque);
    }
}