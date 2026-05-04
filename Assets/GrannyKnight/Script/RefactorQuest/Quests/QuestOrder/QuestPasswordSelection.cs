using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestPasswordSelection : Quest
{
    [SerializeField] private List<int> _rightOrder = new List<int>();
    private List<ItemPassword> _itemPasswordsList = new();
    private int _indexCurrentPassword;

    public override event Action<QuestEnding> OnEnd;
    public override event Action OnStart;

    private void Awake()
    {
        _indexCurrentPassword = 0;
    }

    public void RegisterButton(ItemPassword ItemPassword)
    {
        _itemPasswordsList.Add(ItemPassword);
    }

    public void CheckItem(ItemPassword itemPassword)
    {
        CheckMaxIndex();

        if (itemPassword.Number == _rightOrder[_indexCurrentPassword])
        {
            itemPassword.StateActiveItem();
            _indexCurrentPassword++;
        }
        else
        {
            ResetItem();
        }

        if (_indexCurrentPassword == _rightOrder.Count)
        {
            StopQuest(QuestEnding.Good);
        }
    }

    public override void StartQuest()
    {
        ResetItem();
        OnStart?.Invoke();
    }

    public override void StopQuest(QuestEnding quest)
    {
        OnEnd?.Invoke(quest);
        OffItem();
    }

    private void CheckMaxIndex()
    {
        if (_indexCurrentPassword > _rightOrder.Count)
        {
            _indexCurrentPassword = _rightOrder.Count;
        }
    }

    private void ResetItem()
    {
        foreach (var item in _itemPasswordsList)
        {
            _indexCurrentPassword = 0;
            item.ResetItem();
        }
    }

    private void OffItem()
    {
        foreach (var item in _itemPasswordsList)
        {
            item.StateActiveItem();
        }
    }
}
