using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestOrder : MonoBehaviour
{
    [SerializeField] private int[] _rightOrder;
    [SerializeField] private int[] _currentOrder;
    [SerializeField] private float _returnToStartTime;
    [SerializeField] private bool _needToReset;
    [SerializeField] private UnityEvent _onComplete, _onLose, _returnToStart;
    private void Awake()
    {
        _currentOrder = new int[_rightOrder.Length];
        InvokeReturnToStart();
    }
    public void SetElement(int value)
    {
        for (int i = 0; i < _currentOrder.Length; i++)
        {
            if (_currentOrder[i] == 0)
            {
                _currentOrder[i] = value;
                CheckOrder();
                break;
            }
        }
    }
    private void CheckOrder()
    {
        if (_currentOrder[_currentOrder.Length - 1] == 0)
            return;

        for (int i = 0; i < _currentOrder.Length; i++)
        {
            if(_currentOrder[i] != _rightOrder[i])
            {
                _currentOrder = new int[_rightOrder.Length];
                _onLose.Invoke();
                Invoke(nameof(InvokeReturnToStart), _returnToStartTime);
                return;
            }
        }
        _currentOrder = new int[_rightOrder.Length];
        _onComplete.Invoke();
        if (_needToReset) Invoke(nameof(InvokeReturnToStart), _returnToStartTime);
    }
    private void InvokeReturnToStart()
    {
        _returnToStart.Invoke();
    }    
}
