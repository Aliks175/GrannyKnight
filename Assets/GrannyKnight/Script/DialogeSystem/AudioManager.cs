using FMOD.Studio;
using FMODUnity;

public static class AudioManager
{
    public static EventInstance Play(string eventPath)
    {
        var instance = RuntimeManager.CreateInstance(eventPath);
        instance.start();
        return instance;
    }
}