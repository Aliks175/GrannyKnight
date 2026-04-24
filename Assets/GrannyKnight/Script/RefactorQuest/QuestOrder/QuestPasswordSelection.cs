using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class QuestPasswordSelection : Quest
{
    [SerializeField]private List<int> _rightOrder = new List<int>();
    private List<int> _currentOrder = new List<int>();
    private List<PasswordButton> _buttons = new();

    public override event Action<QuestEnding> OnEnd;
    public override event Action OnStart;

    public void RegisterButton(PasswordButton button)
    {
        _buttons.Add(button);
    }

    public void SetElement(int value)
    {
        int index = _currentOrder.Count;

        if (_rightOrder[index] != value)
        {
            foreach (var button in _buttons) button.ResetState();
            OnEnd?.Invoke(QuestEnding.Bad);
            _currentOrder.Clear();
            Debug.Log("Again");
            return;
        }

        _currentOrder.Add(value);

        if (_currentOrder.Count == _rightOrder.Count)
        {
            OnEnd?.Invoke(QuestEnding.Good);
            foreach (var button in _buttons) button.IsActive = false;
            Debug.Log("Good ending");
        }
    }

    public override void StartQuest()
    {
        _currentOrder.Clear();
        OnStart?.Invoke();
    }

    public override void StopQuest(QuestEnding quest)
    {
        //
    }
}
