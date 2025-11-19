using System;
using System.Collections.Generic;
using UnityEngine;

public class ControlFairyItem : MonoBehaviour
{
    public int CountItem => _item.Count;
    [SerializeField] private List<FairyItem> _item;
    private int _countFairyItem;

    public Action OnEnd;
    public Action<int> OnLostItem;

    private void OnDisable()
    {
        foreach (var item in _item)
        {
            item.OnLost -= CheckLostItems;
        }
    }

    public void Initialization()
    {
        _countFairyItem = _item.Count;
        foreach (var item in _item)
        {
            item.OnLost += CheckLostItems;
        }
        OnLostItem?.Invoke(_countFairyItem);
    }

    public FairyItem GetFairyTarget()
    {
        FairyItem fairyTarget = null;
        for (int i = 0; i < _item.Count; i++)
        {
            if (_item[i].CheckFree)
            {
                fairyTarget = _item[i];
                return fairyTarget;
            }
        }
        return fairyTarget;
    }

    public void CheckLostItems(FairyItem fairyItem)
    {
        _countFairyItem--;
        OnLostItem?.Invoke(_countFairyItem);
        if (_countFairyItem <= 0)
        {
            OnEnd?.Invoke();
        }
    }
}