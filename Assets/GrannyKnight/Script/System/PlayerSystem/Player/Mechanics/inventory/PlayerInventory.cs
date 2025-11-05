using System;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private SlotItem[] slotItems;
    private IPlayerDatable _playerData;
    public event Action<Item> OnAddItem;

    public void Initialization(IPlayerDatable playerData)
    {
        _playerData = playerData;
        if (slotItems == null || slotItems.Length == 0) return;
        for (int i = 0; i < slotItems.Length; i++)
        {
            slotItems[i].Initialization();
        }
    }

    public bool CheckSlot(BazeItem item)
    {
        bool isSuccess;
        if (item.DataItem.IsStackable)
        {
            isSuccess = AddStackableItem(item);
        }
        else
        {
            isSuccess = AddUnStackableItem(item);
        }
        return isSuccess;
    }

    public SlotItem FindSlotItem(int id)
    {
        SlotItem tempSlot = null;
        foreach (SlotItem slotItem in slotItems)
        {
            if (!slotItem.IsFreeSlot)
            {
                if (slotItem.GetId() == id)
                {
                    tempSlot = slotItem;
                    return tempSlot;
                }
            }
        }
        return tempSlot;
    }

    private bool AddUnStackableItem(BazeItem item)
    {
        for (int i = 0; i < slotItems.Length; i++)
        {
            if (slotItems[i].IsFreeSlot)
            {
                slotItems[i].SetItem(item, _playerData);
                slotItems[i].SendItem(OnAddItem);
                return true;
            }
        }
        return false;
    }

    private bool AddStackableItem(BazeItem item)
    {
        for (int i = 0; i < slotItems.Length; i++)
        {
            if (!slotItems[i].IsFreeSlot && slotItems[i].GetId() == item.DataItem.Id)
            {
                if (slotItems[i].CheckChangeCountItem(item.DataItem.CountItem))
                {
                    slotItems[i].ChangeCountItem();
                    slotItems[i].SendItem(OnAddItem);
                }
                return true;
            }
        }
        return AddUnStackableItem(item);
    }
}