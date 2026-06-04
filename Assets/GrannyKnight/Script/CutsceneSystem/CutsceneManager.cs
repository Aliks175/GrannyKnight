using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager Instance;
    [SerializeField] private List<CutsceneStruct> cutscenes = new List<CutsceneStruct>();
    public static Dictionary<string, PlayableDirector> cutsceneDataBase = new Dictionary<string, PlayableDirector>();
    public static PlayableDirector activeCutscene;
    public static event Action OnStartCutscene;
    public static event Action OnEndCutscene;

    private void Awake()
    {
        Instance = this;
        InitializeCutsceneDataBase();
        foreach (var cutscene in cutsceneDataBase)
        {
            if (cutscene.Value != null) cutscene.Value.gameObject.SetActive(false);
        }
    }

    private void InitializeCutsceneDataBase()
    {
        cutsceneDataBase.Clear();
        for (int i = 0; i < cutscenes.Count; i++)
        {
            cutsceneDataBase.Add(cutscenes[i].cutsceneKey, cutscenes[i].cutsceneObject);
        }
    }

    public void StartCutscene(string cutsceneKey)
    {
        if (!cutsceneDataBase.ContainsKey(cutsceneKey))
        {
            Debug.LogError($" \"{cutsceneKey}\" cutsceneDataBase");
            return;
        }
        if (activeCutscene != null)
        {
            if (activeCutscene == cutsceneDataBase[cutsceneKey])
            {
                return;
            }
        }
        activeCutscene = cutsceneDataBase[cutsceneKey];
        foreach (var cutscene in cutsceneDataBase)
        {
            if (cutscene.Value != null) cutscene.Value.gameObject.SetActive(false);
        }
        PlayableDirector playableDirector = cutsceneDataBase[cutsceneKey];
        playableDirector.gameObject.SetActive(true);
        ControlStateCutscene(playableDirector);
    }

    public void EndCutscene()
    {
        if (activeCutscene != null)
        {
            activeCutscene.Stop();
            activeCutscene.gameObject.SetActive(false);
            activeCutscene = null;
        }
    }

    private void ControlStateCutscene(PlayableDirector cutscene)
    {
        cutscene.Play();
        cutscene.stopped += OnStopped;
        OnStartCutscene?.Invoke();
    }

    private void OnStopped(PlayableDirector cutscene)
    {
        cutscene.stopped -= OnStopped;
        OnEndCutscene?.Invoke();
    }
}

[System.Serializable]
public struct CutsceneStruct
{
    public string cutsceneKey;
    public PlayableDirector cutsceneObject;
}
