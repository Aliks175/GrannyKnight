using Cysharp.Threading.Tasks;
using FMODUnity;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class OneSound : MonoBehaviour
{
    [SerializeField] private EventReference _sound;
    //private FMOD.Studio.EventInstance _activeSound;
    private SoundSystem _soundSystem;
    private CancellationTokenSource _cts;
    private bool _isActive;
    public UnityEvent OnStartSound;
    public UnityEvent OnEndSound;
    public UnityEvent OnPlayingSound;
    //public UnityEvent OnSTOPPEDSound;

    private void OnEnable()
    {
        if (_cts != null)
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }
    }

    private void OnDisable()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = null;
    }

    private void Start()
    {
        _soundSystem = GameObject.FindFirstObjectByType<SoundSystem>();
        _isActive = false;
    }

    public void Active()
    {
        if (_isActive) return;
        _isActive = true;
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = new CancellationTokenSource();
        FMOD.Studio.EventInstance _activeSound = _soundSystem.PlaySound(_sound);
        WaitEndSound(_activeSound, _cts).Forget();
    }

    private async UniTaskVoid WaitEndSound(FMOD.Studio.EventInstance eventInstance, CancellationTokenSource cancel)
    {
            OnStartSound?.Invoke();
        Debug.Log($"OnStartSound - {gameObject.name}");
        try
        {
            await UniTask.WaitUntil(
                () => ControlEndSound(eventInstance),
                cancellationToken: cancel.Token                                                   //cancellationToken: _cts.Token
            );
            //if (_isActive)
            //{
            //    _isActive = false;
            //    return;
            //}
            Debug.Log($"OnEndSound - {gameObject.name}");
            OnEndSound?.Invoke();

        }
        catch (OperationCanceledException)
        {
            //OnSTOPPEDSound?.Invoke();
        }
    }

    private bool ControlEndSound(FMOD.Studio.EventInstance _activeSound)
    {
        bool result = false;
        _activeSound.getPlaybackState(out FMOD.Studio.PLAYBACK_STATE state);
        if (state == FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            OnPlayingSound?.Invoke();
        }
        if (state == FMOD.Studio.PLAYBACK_STATE.STOPPING)
        {
            result = true;
            _isActive = false;
        }
        return result;
    }
}