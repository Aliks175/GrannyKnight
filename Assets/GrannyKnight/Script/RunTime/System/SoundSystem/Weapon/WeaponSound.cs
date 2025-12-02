
using FMODUnity;
using UnityEngine;

public class WeaponSound : MonoBehaviour
{
    [SerializeField] private EventReference _fire;
    [SerializeField] private EventReference _preFire;
    private bool _isSystemFire = true;
    private bool _isStartPreFire = false;
    private FMOD.Studio.EventInstance _onFire;
    private FMOD.Studio.EventInstance _onPreFire;

    private void OnDestroy()
    {
        _onPreFire.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        _onFire.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        _onPreFire.release();
        _onFire.release();
    }

    private void PlaySound(ref FMOD.Studio.EventInstance eventInstance, FMODUnity.EventReference eventReference) // ¬ызов другого звука 
    {
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
        if (_isStartPreFire) return;
        Debug.Log("PreFire");
        PlaySound(ref _onPreFire, _preFire);
        //_onPreFire = RuntimeManager.CreateInstance(_preFire); // —оздаем событие «вука 
        //_onPreFire.start();
        _isStartPreFire = true;
    }

    public void Fire()
    {
        if (!_isSystemFire) return;
        Debug.Log("Fire");
        PlaySound(ref _onFire, _fire);
    }

    public void StopSound()
    {
        StopSound(_onPreFire);
        _isStartPreFire = false;
    }


    public void SystemDisableSound()
    {
        _isSystemFire = false;
        StopSound(_onFire);
        StopSound(_onPreFire);
        _isStartPreFire = false;
    }

    private void StopSound(FMOD.Studio.EventInstance eventInstance)
    {
        eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}