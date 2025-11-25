using UnityEngine;
using UnityEngine.Events;

public class HistorySystem : MonoBehaviour
{
    public UnityEvent OnStartGame;

    public void Initialization()
    {

    }

    public void StartGame()
    {
        OnStartGame?.Invoke();
    }
}
