using UnityEngine;

public class StartHistory : MonoBehaviour
{
    [SerializeField] private EventHistory _history;

    private void Start()
    {
        _history.ActiveHistory();
    }
}