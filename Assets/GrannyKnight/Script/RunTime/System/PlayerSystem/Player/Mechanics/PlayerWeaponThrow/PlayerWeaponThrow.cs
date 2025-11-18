using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponThrow : MonoBehaviour
{
    [SerializeField] WeaponThrowPool _weaponThrowPool;
    [SerializeField] private float _forceThrow;
    [SerializeField] private float _directionY = 15;
    [SerializeField] private Transform _firePoint;
    private PlayerInventory _playerInventory;
    private Transform _head;
    private Weapon _equipWeapon;
    private List<Weapon> _throwWeaponsSlot;
    private float _nextTimeToFire;
    public event Action<Weapon> OnChangeWeapon;

    public void Initialization(Transform head, PlayerInventory playerInventory)
    {
        _playerInventory = playerInventory;
        _head = head;
        _throwWeaponsSlot = new List<Weapon>();
        _weaponThrowPool.Initialization();
    }

    public void Throw()
    {
        SlotItem slotItem = CheckItem();
        if (slotItem != null)
        {
            if (Time.time >= _nextTimeToFire)
            {
                _nextTimeToFire = Time.time + _equipWeapon.TimeWaitFire;

                if (GetWeaponThrow())
                {
                    slotItem.ChangeCountItem();
                    CheckLostWeapon();
                }
            }
        }
    }

    private bool GetWeaponThrow()
    {
        GameObject tempWeapon = _weaponThrowPool.GetWeaponGameObject(_equipWeapon);
        if (tempWeapon == null) return false;
        Rigidbody rb = tempWeapon.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.position = _firePoint.position;
        Vector3 tempDirection = Quaternion.AngleAxis(-_directionY, _head.right) * _head.forward;
        rb.AddForce(tempDirection * _forceThrow, ForceMode.Impulse);
        return true;
    }

    public Weapon GetEquipWeapon()
    {
        if (_equipWeapon != null)
        {
            return _equipWeapon;
        }
        return null;
    }

    private SlotItem CheckItem()
    {
        SlotItem slotItem = null;
        if (_equipWeapon == null)
        {
            return slotItem;
        }
        if (_equipWeapon.CountItem <= 0)
        {
            return slotItem;
        }
        slotItem = _playerInventory.FindSlotItem(_equipWeapon.Id);
        if (slotItem == null) return null;
        if (!slotItem.CheckChangeCountItem(-1))
        {
            slotItem = null;
        }
        return slotItem;
    }

    private void CheckLostWeapon()
    {
        if (_equipWeapon != null)
        {
            if (_equipWeapon.CountItem <= 0)
            {
                if (_throwWeaponsSlot.Contains(_equipWeapon))
                {
                    _throwWeaponsSlot.Remove(_equipWeapon);
                    _equipWeapon = null;
                }
            }
        }
        ChooseWeapon();
    }

    private void ChooseWeapon()
    {
        if (_equipWeapon != null) return;

        foreach (var item in _throwWeaponsSlot)
        {
            if (item.CountItem > 0)
            {
                _equipWeapon = item;
                _weaponThrowPool.SetWeaponThrow(_equipWeapon);
                OnChangeWeapon?.Invoke(_equipWeapon);
                return;
            }
        }
    }

    //public void SetWeapon(Weapon weapon)
    //{
    //    if (weapon.TypeWeapon == TypeWeapon.Grenade)
    //    {
    //        _throwWeaponsSlot.Add(weapon);
    //        ChooseWeapon();
    //    }
    //}
}