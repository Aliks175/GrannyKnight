using FMODUnity;
using System;
using UnityEngine;

public class SimpleDialog : MonoBehaviour
{
    [SerializeField] private EventReference _oneSound;
    [SerializeField] private EventReference _twoSound;
    private int CountDialog;

    public void PlayDialog()
    {
        CountDialog++;
        if (CountDialog < 2)
        {
            PlaySound(_oneSound);
        }
        else
        {
            PlaySound(_twoSound);
        }
    }

    private void PlaySound(EventReference eventReference)
    {
        if (!eventReference.IsNull)
        {
            RuntimeManager.PlayOneShot(eventReference, transform.position);
        }
    }
}