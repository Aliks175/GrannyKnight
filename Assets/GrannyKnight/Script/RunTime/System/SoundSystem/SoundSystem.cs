using FMODUnity;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    public static SoundSystem instance;// мы создали общедоступную переменную этого класса 
    [Header("Settings")]
    [SerializeField] private float _timeWaitStep = 1.2f;
    [Header("PlayerMove")]
    [SerializeField] private EventReference _jump;
    [SerializeField] private EventReference _moveArmor;
    [SerializeField] private EventReference _moveGloves;
    [Header("Music")]
    [SerializeField] private EventReference _simpleMusic;

    private float _nextTimeToStep;
    private const string IsActiveParametr = "IsActive";
    private FMOD.Studio.EventInstance _activeSound;
    private FMOD.Studio.EventInstance _activeMusic;

    public void Initialization()
    {
        _timeWaitStep = 1.2f;
        instance = this;
        PlayMusic();
    }

    public void PlayJump()
    {
        if (!_jump.IsNull)
        {
            RuntimeManager.PlayOneShot(_jump);
        }
    }

    public void PlayWalk(bool isArmor)
    {
        if (Time.time >= _nextTimeToStep)
        {
            _nextTimeToStep = Time.time + _timeWaitStep;
            if (isArmor)
            {
                RuntimeManager.PlayOneShot(_moveArmor);
            }
            else
            {
                RuntimeManager.PlayOneShot(_moveGloves);
            }
        }
    }

    public void ControlFullVolumeMusic(bool isActive)
    {
        if (isActive)
        {
            _activeMusic.setParameterByName(IsActiveParametr, 0);
        }
        else
        {
            _activeMusic.setParameterByName(IsActiveParametr, 1);
        }
    }

    public void StopSound()
    {
        _activeSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void PlaySound(EventReference eventReference) // Вызов другого звука 
    {
        _activeSound.getPlaybackState(out FMOD.Studio.PLAYBACK_STATE state);
        if (state == FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            _activeSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
        _activeSound = RuntimeManager.CreateInstance(eventReference); // Создаем событие Звука 
        _activeSound.start(); // Запускаем воспроизведение 
        _activeSound.release(); // освобождаем память от этого события 

        //        PLAYING — звук играет.
        //STOPPED — не играет.
        //STARTING — запускается.
        //STOPPING — останавливается.
    }

    private void ControlSoundStep()
    {

        

    }

    private void PlayMusic() // Вызов другого звука 
    {
        _activeMusic = RuntimeManager.CreateInstance(_simpleMusic); // Создаем событие Звука 
        _activeMusic.setParameterByName(IsActiveParametr, transform.localScale.x);
        _activeMusic.start(); // Запускаем воспроизведение 
        _activeMusic.release(); // освобождаем память от этого события 
    }
}