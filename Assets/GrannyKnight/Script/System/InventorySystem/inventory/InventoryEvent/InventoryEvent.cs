using System.Collections.Generic;
using UnityEngine;

public class InventoryEvent : MonoBehaviour
{
    [SerializeField] private float _timerView = 1f;
    [SerializeField] private float _speedView = 0.2f;
    [SerializeField] private float _speedHide = 0.5f;
    [SerializeField] private List<SlotEvent> _list;
    private PlayerInventory _playerInventory;

    private void OnDisable()
    {
        _playerInventory.OnAddItem -= ChangeInventory;
    }

    public void Initialization(PlayerInventory playerInventory)
    {
        _playerInventory = playerInventory;
        foreach (var item in _list)
        {
            item.Initialization(_timerView, _speedView, _speedHide);
        }
        _playerInventory.OnAddItem += ChangeInventory;
    }

    /// <summary>
    /// Метод реагирует на добавление предметов в инвентарь отображая интерактивную подсказку
    /// </summary>
    /// <param name="item"></param>
    public void ChangeInventory(Item item)
    {
        bool Success = false;
        foreach (var slot in _list)
        {
            if (!slot.IsView)
            {
                slot.SetItem(item);
                Success = true;
                return;
            }
        }

        if (!Success)
        {
            _list[0].SetItem(item);
        }
    }
}