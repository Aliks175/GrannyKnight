using System;
using System.Collections.Generic;
using UnityEngine;

public class ChooseWeaponView : MonoBehaviour
{
    [SerializeField] private PlayerControlAnimation _playerControlAnimation;
    [SerializeField] private GameObject _armorHand;
    [SerializeField] private GameObject _glovesHand;
    [SerializeField] private List<WeaponEffectAbstract> _listWeaponEffect;
    private WeaponEffectAbstract _activeWeapon;

    public event Action<WeaponEffectAbstract> OnWeaponPhisicEquip;
    public event Action<WeaponEffectAbstract> OnWeaponRaycastEquip;

    public void Initialization()
    {

    }

    public void ClearHand(EquipHand equipHand)
    {
        _playerControlAnimation.ChangeHand(equipHand);
        Clear();
    }

    private void Clear()
    {
        foreach (var item in _listWeaponEffect)
        {
            item.gameObject.SetActive(false);
        }
    }

    public void ChangeWeapon(Weapon weapon)
    {
        if (weapon == null)
        {
            _activeWeapon = null;
            OnWeaponPhisicEquip?.Invoke(null);
            OnWeaponRaycastEquip?.Invoke(null);
            return;
        }
        WeaponEffectAbstract tempWeaponEffect = CheckWeapon(weapon);
        if (tempWeaponEffect != null)
        {
            ActiveNewWeapon(tempWeaponEffect, weapon);
        }
    }

    private void ActiveNewWeapon(WeaponEffectAbstract weaponEffect, Weapon weapon)
    {
        if (_activeWeapon != null)
        {
            _activeWeapon.gameObject.SetActive(false);
            _armorHand.SetActive(false);
            _glovesHand.SetActive(false);
        }
        _activeWeapon = weaponEffect;

        if (weapon.TypeShoot == TypeShootPhysics.Physics)
        {
            OnWeaponPhisicEquip?.Invoke(_activeWeapon);
        }
        else if (weapon.TypeShoot == TypeShootPhysics.Raycast)
        {
            OnWeaponRaycastEquip?.Invoke(_activeWeapon);
        }
        _playerControlAnimation.ChangeAnimator(_activeWeapon.AnimatorWeapon);
    }

    private WeaponEffectAbstract CheckWeapon(Weapon weapon)
    {
        WeaponEffectAbstract tempWeaponEffect = null;

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