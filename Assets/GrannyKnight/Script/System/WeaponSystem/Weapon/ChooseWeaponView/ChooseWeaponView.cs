using System;
using System.Collections.Generic;
using UnityEngine;

public class ChooseWeaponView : MonoBehaviour
{
    [SerializeField] private GameObject _handSlot;
    private List<WeaponEffect> _listWeaponEffect;
    private WeaponEffect _activeWeapon;
    private GameObject _weaponSlot;
    public event Action<WeaponEffect> OnWeaponEquip;

    public void Initialization()
    {
        _listWeaponEffect = new List<WeaponEffect>();
    }

    public void ChangeWeapon(Weapon weapon)
    {
        WeaponEffect tempWeaponEffect = CheckWeapon(weapon);
        if (tempWeaponEffect != null)
        {
            ActiveNewWeapon(tempWeaponEffect);
        }
        else
        {
            _weaponSlot = Instantiate(weapon.Model, _handSlot.transform);
            if (_weaponSlot.TryGetComponent(out WeaponEffect weaponEffect))
            {
                _listWeaponEffect.Add(weaponEffect);
                ActiveNewWeapon(weaponEffect);
            }
        }
    }

    private void ActiveNewWeapon(WeaponEffect weaponEffect)
    {
        if (_activeWeapon != null)
        {
            _activeWeapon.gameObject.SetActive(false);
        }
        _activeWeapon = weaponEffect;
        OnWeaponEquip?.Invoke(_activeWeapon);
        _activeWeapon.gameObject.SetActive(true);
    }

    private WeaponEffect CheckWeapon(Weapon weapon)
    {
        WeaponEffect tempWeaponEffect = null;

        foreach (var item in _listWeaponEffect)
        {
            if (item.IdWeapon == weapon.Id)
            {
                tempWeaponEffect = item;
                return tempWeaponEffect;
            }
        }
        return tempWeaponEffect;
    }
}