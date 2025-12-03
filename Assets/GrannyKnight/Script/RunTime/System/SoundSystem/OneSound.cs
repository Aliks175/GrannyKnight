using Cysharp.Threading.Tasks;
using FMODUnity;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class OneSound : MonoBehaviour
{
    [SerializeField] private EventReference _sound;
    private SoundSystem _soundSystem;
    private FMOD.Studio.EventInstance _activeSound;
    private CancellationTokenSource _cts;
    private bool _isEnd;
    public UnityEvent OnStartSound;
    public UnityEvent OnEndSound;

    private void OnEnable()
    {
        if (_cts != null)
        {
            _cts.Dispose();
        }
        _cts = new CancellationTokenSource();
    }

    private void OnDisable()
    {
        _cts?.Cancel();
        _cts.Dispose();
        _cts = null;
    }

    private void Start()
    {
        _soundSystem = GameObject.FindFirstObjectByType<SoundSystem>();
        _isEnd = false;
    }

    public void Active()
    {
        _isEnd = false;
        _activeSound = _soundSystem.PlaySound(_sound);
        _cts?.Cancel();
        WaitEndSound().Forget();
    }

    private async UniTaskVoid WaitEndSound()
    {
        _cts = new CancellationTokenSource();
        OnStartSound?.Invoke();
        Debug.Log($"OnStartSound - {gameObject.name}");
        ;
        try
        {
            await UniTask.WaitUntil(
                () => ControlEndSound(),
                cancellationToken: _cts.Token                                                   //cancellationToken: _cts.Token
            );
            if (!_isEnd)
            {
                Debug.Log($"OnEndSound - {gameObject.name}");
                OnEndSound?.Invoke();
                _isEnd = true;
            }
        }
        catch (OperationCanceledException)
        {
            if (!_isEnd)
            {
                Debug.Log($"OnEndSound - {gameObject.name}");
                //OnEndSound?.Invoke();
                _isEnd = true;
            }
        }
        //if (state == FMOD.Studio.PLAYBACK_STATE.STOPPED)
        //{
        //    OnEndSound?.Invoke();
        //    Debug.Log("STOPPED");
        //}
        //else if (state == FMOD.Studio.PLAYBACK_STATE.PLAYING)
        //{
        //    Debug.Log("Playing");
        //}
    }

    private bool ControlEndSound()
    {
        _activeSound.getPlaybackState(out FMOD.Studio.PLAYBACK_STATE state);
        return state == FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }

    //private void Update()
    //{
    //    if (!_isPlaying) return;
    //    _activeSound.getPlaybackState(out FMOD.Studio.PLAYBACK_STATE state);
    //    if (state == FMOD.Studio.PLAYBACK_STATE.STOPPED)
    //    {
    //        Debug.Log("STOPPED");
    //        _isPlaying = false;
    //    }
    //    else if (state == FMOD.Studio.PLAYBACK_STATE.PLAYING)
    //    {
    //        Debug.Log("Playing");
    //    }
    //}
}