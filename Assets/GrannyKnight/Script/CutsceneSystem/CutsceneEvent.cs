using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class CutsceneEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent _startCutscene;
    [SerializeField] private UnityEvent _endCutscene;
    private PlayableDirector _playable;

    private void OnEnable()
    {
        if (_playable == null)
        {
            _playable = GetComponent<PlayableDirector>();
        }
        _playable.played += Played;
        _playable.stopped += Stopped;
    }

    private void OnDisable()
    {
        _playable.played -= Played;
        _playable.stopped -= Stopped;
    }

    public void StartCutscene()
    {
        Debug.Log("StartCutscene");
        _startCutscene?.Invoke();
    }

    public void EndCutscene()
    {
        Debug.Log("EndCutscene");
        _endCutscene?.Invoke();
    }
    private void Played(PlayableDirector obj)
    {
        StartCutscene();
    }

    private void Stopped(PlayableDirector obj)
    {
        obj.gameObject.SetActive(false);
        EndCutscene();
    }
}