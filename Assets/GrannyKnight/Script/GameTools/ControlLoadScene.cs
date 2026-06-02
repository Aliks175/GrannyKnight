using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class ControlLoadScene : MonoBehaviour
{
    [SerializeField] private UnityEvent _use;
    private CutsceneScreen _cutsceneScreen;

    [Inject]
    private void Construct(CutsceneScreen cutsceneScreen)
    {
        _cutsceneScreen = cutsceneScreen;
    }

    public void StartLoad()
    {
        _cutsceneScreen.StartLoadScene(Active);
    }

    private void Active()
    {
        _use?.Invoke();
    }
}