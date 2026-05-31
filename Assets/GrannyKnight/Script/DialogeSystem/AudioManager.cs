using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public static class AudioManager
{
    private const string DEFAULT_DIALOGUE_SOUND = "event:/Dialogs/BaseSoundDialog";

    public static EventInstance Play(string eventPath)
    {
        try
        {
            var instance = RuntimeManager.CreateInstance(eventPath);
            instance.start();
            return instance;
        }
        catch (System.Exception ex) when (ex.Message.Contains("Event not found"))
        {
            Debug.LogWarning($"FMOD Event not found: '{eventPath}', using default sound");
            var defaultInstance = RuntimeManager.CreateInstance(DEFAULT_DIALOGUE_SOUND);
            defaultInstance.start();
            return defaultInstance;
        }
    }
}