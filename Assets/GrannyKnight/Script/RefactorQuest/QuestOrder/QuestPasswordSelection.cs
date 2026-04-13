using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestPasswordSelection : Quest
{
    private List<int> _rightOrder = new List<int>();
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
            foreach (var button in _buttons) button.ResetState(PasswordButton.ButtonPasswordState.Lose);
            OnEnd?.Invoke(QuestEnding.Bad);
            _currentOrder.Clear();
            StopQuest(QuestEnding.Bad);
            return;
        }

        _currentOrder.Add(value);

        if (_currentOrder.Count == _rightOrder.Count)
        {
            OnEnd?.Invoke(QuestEnding.Good);
            foreach (var button in _buttons) button.ResetState(PasswordButton.ButtonPasswordState.Win);
        }
    }

    public override void StartQuest()
    {
        _currentOrder.Clear();
        OnStart?.Invoke();
        foreach (var button in _buttons) button.ResetState(PasswordButton.ButtonPasswordState.Base);
    }
    public void SetOrder(List<int> rightOrder)
    {
        _rightOrder = rightOrder;
    }

    public override void StopQuest(QuestEnding quest)
    {
        throw new NotImplementedException();
    }
}
