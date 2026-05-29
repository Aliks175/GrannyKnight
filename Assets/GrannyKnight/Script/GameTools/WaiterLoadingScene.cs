using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class WaiterLoadingScene : MonoBehaviour
{
    [SerializeField] private UnityEvent _use;
    private GameManager _gameManager;
    private CutsceneScreen _cutsceneScreen;

    [Inject]
    private void Construct(GameManager gameManager, CutsceneScreen cutsceneScreen)
    {
        _gameManager = gameManager;
        _cutsceneScreen = cutsceneScreen;
    }

    public void Active()
    {
        _cutsceneScreen.OnBlackOut(true);
        _gameManager.CurrentOperation.allowSceneActivation = true;
        _gameManager.CurrentOperation.completed += Completed;
    }

    private void Completed(AsyncOperation obj)
    {
        _use?.Invoke();
        _cutsceneScreen.OnBlackOut(false);
    }
}