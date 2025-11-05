using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChooseWeapon : MonoBehaviour
{
    private List<Weapon> _oneSlot;
    private List<Weapon> _twoSlot;
    //...
    private Weapon _equipWeapon;
    private SlotNumber _slotNumber;
    private int _indexWeapon;
    public event Action<Weapon> OnChangeWeapon;
    public event Action<Weapon> OnSetThrowWeapon;

    public void Initialization()
    {
        _indexWeapon = 0;
        _slotNumber = SlotNumber.None;
        _oneSlot = new List<Weapon>();
        _twoSlot = new List<Weapon>();
    }

    public void GiveWeapon(SlotNumber slotNumber)
    {
        Weapon tempWeapon = null;
        switch (slotNumber)
        {
            case SlotNumber.OneSlot:
                GiveWeaponForSlot(ref tempWeapon, _oneSlot, slotNumber);
                break;
            case SlotNumber.TwoSlot:
                GiveWeaponForSlot(ref tempWeapon, _twoSlot, slotNumber);
                break;
            //case SlotNumber.ThreeSlot:
            //    break;
            //case SlotNumber.FourSlot:
            //    break;
            //case SlotNumber.FiveSlot:
            //    break;
            //case SlotNumber.SixSlot:
            //    break;
            //case SlotNumber.SevenSlot:
            //    break;
            //case SlotNumber.EightSlot:
            //    break;
            //case SlotNumber.NineSlot:
            //    break;
            default:
                break;
        }
    }

    public void ChangeSlot()
    {
        if (_slotNumber == SlotNumber.OneSlot)
        {
            GiveWeapon(SlotNumber.TwoSlot);
        }
        else if (_slotNumber == SlotNumber.TwoSlot)
        {
            GiveWeapon(SlotNumber.OneSlot);
        }
    }

    private void GiveWeaponForSlot(ref Weapon tempWeapon, List<Weapon> weaponsSlot, SlotNumber slotNumber)
    {
        tempWeapon = GetWeapon(weaponsSlot, slotNumber);
        if (tempWeapon != null)
        {
            _equipWeapon = tempWeapon;
            OnChangeWeapon?.Invoke(_equipWeapon);
        }
    }

    private void AddIndex(List<Weapon> weaponsSlot, SlotNumber slotNumber)
    {
        if (slotNumber != _slotNumber)
        {
            _indexWeapon = 0;
            _slotNumber = slotNumber;
        }
        else
        {
            _indexWeapon = weaponsSlot.Count - 1 == _indexWeapon ? 0 : _indexWeapon + 1;
        }
    }

    public void SetWeapon(TypeWeapon typeWeapon, Weapon Weapon)
    {
        switch (typeWeapon)
        {
            case TypeWeapon.none:
                break;
            case TypeWeapon.Pistol:
                CheckWeapon(_oneSlot, Weapon);
                break;
            case TypeWeapon.Stick:
                CheckWeapon(_twoSlot, Weapon);
                break;
            case TypeWeapon.Grenade:
                OnSetThrowWeapon?.Invoke(Weapon);
                break;
            default:
                break;
        }
    }

    private Weapon GetWeapon(List<Weapon> weaponsSlot, SlotNumber slotNumber)
    {
        AddIndex(weaponsSlot, slotNumber);
        Weapon weapon = null;
        if (weaponsSlot == null) return weapon;

        if (_indexWeapon < weaponsSlot.Count)
        {
            if (weaponsSlot[_indexWeapon] != _equipWeapon && weaponsSlot[_indexWeapon] != null)
            {
                weapon = weaponsSlot[_indexWeapon];
            }
        }
        return weapon;
    }

    private void CheckWeapon(List<Weapon> weaponsSlot, Weapon weapon)
    {
        if (weaponsSlot.Contains(weapon))
        {
            Debug.Log("Добавили два предмета");
        }
        else
        {
            weaponsSlot.Add(weapon);
        }
    }
}

public enum SlotNumber
{
    None,
    OneSlot,
    TwoSlot,
    //ThreeSlot,
    //FourSlot,
    //FiveSlot,
    //SixSlot,
    //SevenSlot,
    //EightSlot,
    //NineSlot
}