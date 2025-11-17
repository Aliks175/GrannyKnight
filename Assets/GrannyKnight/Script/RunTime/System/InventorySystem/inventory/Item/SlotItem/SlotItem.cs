using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс отвечает за слот инвенторя 
/// </summary>
public class SlotItem : MonoBehaviour
{
    public bool IsFreeSlot => !_item.Exists;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _countItem;
    private Item _item;
    private int _countChange;
    private bool _isCheckUse = false;
    public event Action OnUpdateView;

    public void Initialization()
    {
        _item = new Item();
        _isCheckUse = false;
    }

    /// <summary>
    /// Установить новый предмет
    /// </summary>
    /// <param name="bazeItem"></param>
    /// <param name="playerData"></param>
    public void SetItem(BazeItem bazeItem, IPlayerDatable playerData)
    {
        if (bazeItem.DataItem.TypeItem == TypeItem.Weapon)
        {
            Weapon weapon = new();
            _item = weapon;
            weapon.Initialization(bazeItem, playerData);
        }
        else
        {
            _item.Initialization(bazeItem, playerData);
        }
        UpdateView();
    }

    public int GetId()
    {
        int id = -1;
        if (_item != null)
            id = _item.Id;
        return id;
    }

    public void SendItem(Action<Item> action)
    {
        action?.Invoke(_item);
    }

    public bool CheckChangeCountItem(int count)
    {
        if (count == 0) return false;
        _isCheckUse = _item.CountItem + count >= 0;
        _countChange = _isCheckUse ? count : 0;
        return _isCheckUse;
    }

    public void ChangeCountItem()
    {
        if (!_isCheckUse) return;
        _item.CountItem += _countChange;
        _countChange = 0;
        if (_item.CountItem == 0)
        {
            ClearSlot();
        }
        UpdateView();
        return;
    }

    private void ClearSlot()
    {
        _item.ClearItem();
        UpdateView();
    }

    /// <summary>
    /// Обновление отображения UI
    /// </summary>
    public void UpdateView()
    {
        if (IsFreeSlot)
        {
            _countItem.SetText(string.Empty);
            _icon.sprite = null;
        }
        else
        {
            if (_item.IsStackable)
            {
                _countItem.SetText(_item.CountItem.ToString());
            }
            else
            {
                _countItem.SetText(string.Empty);
            }
            _icon.sprite = _item.IconItem;
        }
        OnUpdateView?.Invoke();
    }
}