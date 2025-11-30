using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class SkipDialogue : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private string signalPrefix = "Dialogue_";
    private List<SignalEmitter> dialogueSignals = new List<SignalEmitter>();
    private int currentSignalIndex = -1;
    void Start()
    {
        FindDialogueSignals();
        
    }
     private void FindDialogueSignals()
    {
        dialogueSignals.Clear();
        
        if (director == null)
        {
            Debug.LogError("PlayableDirector is null!");
            return;
        }
        
        if (director.playableAsset == null)
        {
            Debug.LogError("PlayableAsset is null!");
            return;
        }

        TimelineAsset timeline = director.playableAsset as TimelineAsset;
        if (timeline == null)
        {
            Debug.LogError("PlayableAsset is not a TimelineAsset!");
            return;
        }
        
        Debug.Log($"Timeline found: {timeline.name}");

        // Ищем Signal Track
        bool signalTrackFound = false;
        foreach (var track in timeline.GetOutputTracks())
        {
            Debug.Log($"Track found: {track.name} (Type: {track.GetType().Name})");
            
            if (track is SignalTrack signalTrack)
            {
                signalTrackFound = true;
                Debug.Log($"Signal track found with {signalTrack.GetMarkers().Count()} markers");
                
                foreach (var marker in signalTrack.GetMarkers())
                {
                    if (marker is SignalEmitter signalEmitter)
                    {
                        string signalName = signalEmitter.asset?.name ?? "Unknown";
                        Debug.Log($"Signal: {signalName} (prefix check: {signalName.StartsWith(signalPrefix)})");
                        
                        if (signalName.StartsWith(signalPrefix))
                        {
                            dialogueSignals.Add(signalEmitter);
                            Debug.Log($"Found dialogue signal: {signalName} at {signalEmitter.time:F2}s");
                        }
                    }
                }
                break;
            }
        }
        
        if (!signalTrackFound)
        {
            Debug.LogWarning("No Signal Track found in Timeline!");
        }
        
        // Сортируем по времени
        dialogueSignals.Sort((a, b) => a.time.CompareTo(b.time));
        
        Debug.Log($"Total dialogue signals found: {dialogueSignals.Count}");
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
                director.time = dialogueSignals[i].time;
                currentSignalIndex = i;
                Debug.Log($"Skipped to: {dialogueSignals[i].asset?.name}");
                return;
            }
        }
        
        // Если это последняя реплика - пропускаем до конца
        director.time = director.duration;
    }
}
