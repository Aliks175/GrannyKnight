using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue _dialoque;


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DialogueManager.Instance.StartDialogue(_dialoque);
        }
    }
}