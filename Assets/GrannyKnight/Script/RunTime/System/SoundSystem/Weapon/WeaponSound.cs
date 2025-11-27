using FMODUnity;
using UnityEngine;

public class WeaponSound : MonoBehaviour
{
    [SerializeField] private EventReference _fire;
    private bool _isSystemFire = true;
    private FMOD.Studio.EventInstance _onFire;

    private void OnDestroy()
    {
        _onFire.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        _onFire.release();
    }

    private void PlaySound(ref FMOD.Studio.EventInstance eventInstance, EventReference eventReference) // ¬ызов другого звука 
    {
        eventInstance.getPlaybackState(out FMOD.Studio.PLAYBACK_STATE state);
        if (state == FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        eventInstance = RuntimeManager.CreateInstance(eventReference); // —оздаем событие «вука 
        eventInstance.start(); // «апускаем воспроизведение 

        //        PLAYING Ч звук играет.
        //STOPPED Ч не играет.
        //STARTING Ч запускаетс€.
        //STOPPING Ч останавливаетс€.
    }

    public void Fire()
    {
        if (!_isSystemFire) return;
        _onFire.getPlaybackState(out FMOD.Studio.PLAYBACK_STATE state);
        if (state != FMOD.Studio.PLAYBACK_STATE.STOPPED) return;
        PlaySound(ref _onFire, _fire);
    }

    public void StopSound()
    {
        _onFire.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void SystemDisableSound()
    {
        _isSystemFire = false;
        StopSound();
    }
}