using System;
using UnityEngine;

public class Item
{
    public GameObject Model;
    public Sprite IconItem;
    public bool Exists;
    private DataItem DataItem;
    public string Name => DataItem.Name;
    public int Id => DataItem.Id;
    public bool IsStackable => DataItem.IsStackable;
    public int CountItem
    {
        get { return DataItem.CountItem; }
        set { DataItem.CountItem = value; }
    }
    public TypeUse TypeUse => DataItem.TypeUse;
    public TypeItem TypeItem => DataItem.TypeItem;
    [TextArea(2, 5)]
    public string DescriptionItem => DataItem.DescriptionItem;
    protected IPlayerDatable _characterData;

    public Item()
    {
        Model = null;
        IconItem = null;
        DataItem = new DataItem();
        _characterData = null;
        Exists = false;
    }

    public virtual void Initialization(BazeItem bazeItem, IPlayerDatable characterData)
    {
        Model = bazeItem.Model;
        IconItem = bazeItem.IconItem;
        _characterData = characterData;
        DataItem = bazeItem.DataItem;
        Exists = true;
    }

    public void ClearItem()
    {
        Model = null;
        IconItem = null;
        Exists = false;
        DataItem = new DataItem();
        _characterData = null;
    }
}

[Serializable]
public struct DataItem
{
    public string Name;
    public int Id;
    public bool IsStackable;
    public int CountItem;
    public TypeUse TypeUse;
    public TypeItem TypeItem;
    [TextArea(2, 5)]
    public string DescriptionItem;
}

public enum TypeItem
{
    none,
    Weapon,
    Potion,
    Other
}

public enum TypeUse
{
    None,
    Expendable,
    Permanent
}