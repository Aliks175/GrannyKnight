using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class SkipDialogue : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private string signalPrefix = "Dialogue_";
    [SerializeField] private SignalReceiver signalReceiver;
    private List<SignalEmitter> dialogueSignals = new List<SignalEmitter>();
    
    private int currentSignalIndex = -1;
    private List<SignalEmitter> allSignals = new List<SignalEmitter>();
    void Start()
    {
        FindDialogueSignals();
        
    }
     private void FindDialogueSignals()
    {
        dialogueSignals.Clear();
        
        TimelineAsset timeline = director.playableAsset as TimelineAsset;
        
        foreach (var track in timeline.GetOutputTracks())
        {
            if (track is SignalTrack signalTrack)
            {
                foreach (var marker in signalTrack.GetMarkers())
                {
                    if (marker is SignalEmitter signalEmitter)
                    {
                        string signalName = signalEmitter.asset?.name ?? "Unknown";

                        if (signalName.StartsWith(signalPrefix))
                        {
                            dialogueSignals.Add(signalEmitter);
                        }
                        else
                        {
                            allSignals.Add(signalEmitter);
                        }
                    }
                }
            }
        }
        // Сортируем по времени
        dialogueSignals.Sort((a, b) => a.time.CompareTo(b.time));
        allSignals.Sort((a, b) => a.time.CompareTo(b.time));
    }
    void Update()
    {
        UpdateCurrentSignalIndex();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SkipToNextDialogue();
        }
    }

    private void UpdateCurrentSignalIndex()
    {
        if (director == null || !director.playableGraph.IsValid()) return;
        
        double currentTime = director.time;
        
        for (int i = 0; i < dialogueSignals.Count; i++)
        {
            if (currentTime >= dialogueSignals[i].time)
            {
                currentSignalIndex = i;
            }
            else
            {
                break;
            }
        }
    }
     public void SkipToNextDialogue()
    {
        if (dialogueSignals.Count == 0) return;
        
        double currentTime = director.time;
        
        // Ищем следующий сигнал после текущего времени
        for (int i = 0; i < dialogueSignals.Count; i++)
        {
            if (dialogueSignals[i].time > currentTime + 0.1f)
            {
                director.Pause();
                FireSignalsBetween(director.time, dialogueSignals[i].time);
                director.time = dialogueSignals[i].time;
                currentSignalIndex = i;
                director.Play();
                return;
            }
        }
        
        // Если это последняя реплика - пропускаем до конца
        director.time = director.duration;
    }
    private void FireSignalsBetween(double startTime, double endTime)
    {
        foreach (var signal in allSignals)
        {
            // Если сигнал находится между startTime и endTime, вызываем его
            if (signal.time > startTime && signal.time <= endTime)
            {
                // Вызываем сигнал вручную
                FireSignal(signal);
            }
        }
    }
    private void FireSignal(SignalEmitter signalEmitter)
    {
        if (signalEmitter.asset != null)
        {
            if (signalReceiver.GetReaction(signalEmitter.asset) != null)
                {
                    signalReceiver.GetReaction(signalEmitter.asset).Invoke();
                }
        }
    }
    
    public void DebugCheck()
    {
        Debug.Log("Поймал");
    }
}