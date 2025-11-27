using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
    [SerializeField] private EventReference _move;
    [SerializeField] private EventReference _idle;
    [SerializeField] private EventReference _hit;
    [SerializeField] private EventReference _die;

    [SerializeField] private bool _isMoveForever;

    private FMOD.Studio.EventInstance _onMove;
    private FMOD.Studio.EventInstance _onActive;

    private void OnDestroy()
    {
        _onMove.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        _onActive.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        _onMove.release();
        _onActive.release();
    }

    private void Start()
    {
        PlaySound(ref _onMove, _move);
        PlaySound(ref _onActive, _idle);
    }

    public void PlaySound(ref FMOD.Studio.EventInstance eventInstance, EventReference eventReference) // ¬ызов другого звука 
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

    public void OnMove()
    {
        if (_isMoveForever) return;

        _onMove.getPlaybackState(out FMOD.Studio.PLAYBACK_STATE state);
        if (state == FMOD.Studio.PLAYBACK_STATE.PLAYING) return;
        PlaySound(ref _onMove, _move);
    }

    public void OnIdle()
    {
        PlaySound(ref _onActive, _idle);
    }

    public void OnHit()
    {
        PlaySound(ref _onActive, _hit);
    }

    public void OnDie()
    {
        if (!_die.IsNull)
        {
            RuntimeManager.PlayOneShot(_die);
        }
        StopSound();
    }

    public void StopSound()
    {
        _onMove.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        _onActive.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}