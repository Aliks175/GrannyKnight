using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class TriggerSound : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private EventReference _sound;
    [SerializeField] private bool isOnlyOnePlay = false;
    private EventInstance eventInstance;
    private bool isPlay = false;

    //private void Update()
    //{
    //    if (Keyboard.current.iKey.wasPressedThisFrame)
    //    {
    //        if (IsPlaying(eventInstance))
    //        {
    //            Debug.Log("Play");
    //        }
    //        else
    //        {
    //            Debug.Log("NotPlay");
    //        }
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (isPlay) return;
        Collider[] colliders = Physics.OverlapSphere(transform.position, 5, _playerLayer);
        if (colliders.Length > 0 && colliders != null)
        {
            eventInstance = PlaySound();
            Debug.Log("FindPlayer");
            isPlay = !isOnlyOnePlay;

            if (IsPlaying(eventInstance))
            {
                Debug.Log("Play");
            }
            else
            {
                Debug.Log("NotPlay");
            }
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }

    /// <summary>
    /// Проверяем запущен ли звук
    /// </summary>
    /// <param name="instance"></param>
    /// <returns></returns>
    private bool IsPlaying(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE state;
        instance.getPlaybackState(out state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }

    private EventInstance PlaySound() // Вызов другого звука 
    {
        FMOD.Studio.EventInstance playHit = RuntimeManager.CreateInstance(_sound); // Создаем событие Звука 
        playHit.set3DAttributes(RuntimeUtils.To3DAttributes(transform.position));
        playHit.start(); // Запускаем воспроизведение 
        playHit.release(); // освобождаем память от этого события 
        return playHit;
    }
}