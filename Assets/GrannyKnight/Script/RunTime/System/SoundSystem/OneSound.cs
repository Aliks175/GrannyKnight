using FMODUnity;
using UnityEngine;

public class OneSound : MonoBehaviour
{
    [SerializeField] private EventReference _sound;
    private SoundSystem _soundSystem;

    private void Start()
    {
        _soundSystem = GameObject.FindFirstObjectByType<SoundSystem>();
    }

    public void Active()
    {
        _soundSystem.PlaySound(_sound);
    }
}