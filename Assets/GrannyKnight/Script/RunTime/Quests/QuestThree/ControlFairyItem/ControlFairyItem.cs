using System.Collections.Generic;
using UnityEngine;

public class ControlFairyItem : MonoBehaviour
{
    [SerializeField] private List<FairyItem> _item;

    public void Initialization()
    {
        foreach (var item in _item)
        {
            item.OnLost += CheckLostItems;
        }
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

    }

}
