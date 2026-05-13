using UnityEngine;
using UnityEngine.Events;

public class MazeCollect : MonoBehaviour
{
    [SerializeField] private int _toCollect;
    [SerializeField] private UnityEvent _onCollect;
    private int _collected = 0;

    public void AddCollect()
    {
        _collected++;
        CheckCollected();
    }

    private void CheckCollected()
    {
        if (_collected >= _toCollect)
        {
            _onCollect?.Invoke();
        }
    }
}