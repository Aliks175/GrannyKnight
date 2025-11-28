using FMODUnity;
using UnityEngine;

public class SlingshotWeaponSound : MonoBehaviour
{
    [SerializeField] private EventReference _preFire;
    [SerializeField] private EventReference _fire;
    private bool _isSystemFire = true;
    private FMOD.Studio.EventInstance _onPreFire;
    private FMOD.Studio.EventInstance _onFire;

    private void OnDestroy()
    {
        _onPreFire.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        _onFire.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        _onPreFire.release();
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

    public void PreFire()
    {
        if (!_isSystemFire) return;
        //_onFire.getPlaybackState(out FMOD.Studio.PLAYBACK_STATE state);
        //if (state == FMOD.Studio.PLAYBACK_STATE.PLAYING) return;
        PlaySound(ref _onPreFire, _preFire);
    }

    public void Fire()
    {
        if (!_isSystemFire) return;
        PlaySound(ref _onFire, _fire);
        _onPreFire.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void StopSound()
    {
        _onPreFire.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void SystemDisableSound()
    {
        _isSystemFire = false;
        StopSound();
    }
}