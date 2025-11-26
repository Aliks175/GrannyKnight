using FMODUnity;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    public static SoundSystem instance;// мы создали общедоступную переменную этого класса 
    [Header("PlayerMove")]
    [SerializeField] private EventReference _jump1;// список проигрываемых звуков 
    [SerializeField] private EventReference _jump2;
    [Header("Voice")]
    [SerializeField] private EventReference _heroWork_1;
    [SerializeField] private EventReference _heroWork_2;
    [SerializeField] private EventReference _babka;

    private FMOD.Studio.EventInstance _activeSound;

    public void Initialization()
    {
        instance = this;
    }

    public void PlayJump(bool id)
    {
        if (id)
        {
            if (!_jump1.IsNull)
            {
                RuntimeManager.PlayOneShot(_jump1);
            }
        }
        else
        {
            if (!_jump2.IsNull)
            {
                RuntimeManager.PlayOneShot(_jump2);
            }
        }
    }

    public void PlayHero(bool id)
    {
        if (id)
        {
            if (!_heroWork_1.IsNull)
            {
                RuntimeManager.PlayOneShot(_heroWork_1);
            }
        }
        else
        {
            if (!_heroWork_2.IsNull)
            {
                RuntimeManager.PlayOneShot(_heroWork_2);
            }
        }
    }

    public void PlayBabka()
    {
        if (!_babka.IsNull)
        {
            RuntimeManager.PlayOneShot(_babka);
        }
    }

    public void StopSound()
    {
        _activeSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void PlaySound(EventReference eventReference) // ¬ызов другого звука 
    {
        _activeSound.getPlaybackState(out FMOD.Studio.PLAYBACK_STATE state);
        if (state == FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            _activeSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        _activeSound = RuntimeManager.CreateInstance(eventReference); // —оздаем событие «вука 
        _activeSound.start(); // «апускаем воспроизведение 
        _activeSound.release(); // освобождаем пам€ть от этого событи€ 

        //        PLAYING Ч звук играет.
        //STOPPED Ч не играет.
        //STARTING Ч запускаетс€.
        //STOPPING Ч останавливаетс€.
    }
}