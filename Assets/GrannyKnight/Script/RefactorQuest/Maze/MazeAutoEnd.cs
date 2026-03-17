using UnityEngine;
using UnityEngine.Events;

public class MazeAutoEnd : MonoBehaviour
{
    [SerializeField] private UnityEvent _events;
    [SerializeField] private float _delay;
    private bool _isInvoke = false;
    private void OnAutoEnd()
    {
        _events.Invoke();
        _isInvoke = true;
    }
    public void StopTimer()
    {
        if (_isInvoke == true) return;
        CancelInvoke(nameof(OnAutoEnd));
        OnAutoEnd();
    }
    public void StartTimer()
    {
        Invoke(nameof(OnAutoEnd), _delay);
    }
}
