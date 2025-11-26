using UnityEngine;

public class DialogueSingle : MonoBehaviour
{
    [SerializeField] private Dialogue _dialoque;

    public void Active()
    {
        DialogueManager.Instance.StartDialogue(_dialoque);
    }
}