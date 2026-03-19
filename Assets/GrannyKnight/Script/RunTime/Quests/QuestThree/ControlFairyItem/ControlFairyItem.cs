using System;
using System.Collections.Generic;
using UnityEngine;

public class ControlFairyItem : MonoBehaviour
{
    public int CountItem => _item.Count;
    [SerializeField] private List<FairyItem> _item;
    [Header("PositionBlins")]
    [SerializeField] private List<Transform> _positionBlins;
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

    public void SpawnBlins()
    {
        for (int i = 0; i < _item.Count; i++)
        {
            _item[i].transform.rotation = Quaternion.identity;
            _item[i].transform.position = _positionBlins[i].transform.position;
            _item[i].ResetItem();
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

    public void ResetFairyItem()
    {
        _countFairyItem = _item.Count;
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
        if (_countFairyItem <= 0)
        {
            _countFairyItem = 0;
            OnEnd?.Invoke();
        }
        OnLostItem?.Invoke(_countFairyItem);
    }
}