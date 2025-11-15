using TMPro;
using UnityEngine;

public class UiEquip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameEquipWeapon;
    [SerializeField] private TextMeshProUGUI _nameEquipBonusWeapon;
    private PlayerInventory _playerInventory;
    private PlayerWeaponThrow _playerWeaponThrow;
    private SlotItem _slotItem;

    public void Initialization(PlayerInventory playerInventory, PlayerWeaponThrow playerWeaponThrow)
    {
        _playerInventory = playerInventory;
        _playerWeaponThrow = playerWeaponThrow;
    }

    public void ChangeBonusWeapon()
    {
        Weapon tempWeapon = _playerWeaponThrow.GetEquipWeapon();
        if (tempWeapon == null)
        {
            if (_slotItem != null)
            {
                _slotItem.OnUpdateView -= UpdateViewBonusWeapon;
            }
            return;
        }
        if (_slotItem != null)
        {
            _slotItem.OnUpdateView -= UpdateViewBonusWeapon;
            _slotItem = _playerInventory.FindSlotItem(tempWeapon.Id);
            _slotItem.OnUpdateView += UpdateViewBonusWeapon;
        }
        else
        {
            _slotItem = _playerInventory.FindSlotItem(tempWeapon.Id);
            _slotItem.OnUpdateView += UpdateViewBonusWeapon;
        }
        UpdateViewBonusWeapon();
    }

    private void ChangeBonusWeaponName(Item item)
    {
        string name = "Empty";
        if (item is Weapon weapon)
        {
            if (weapon.TypeWeapon != TypeWeapon.Grenade) return;

            if (!string.IsNullOrEmpty(weapon.Name))
            {
                name = $"{item.Name}\nx {item.CountItem}";
            }
        }
        PrintName(name, _nameEquipBonusWeapon);
    }

    private void UpdateViewBonusWeapon()
    {
        _slotItem.SendItem(ChangeBonusWeaponName);
    }

    public void ChangeWeaponName(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            PrintName(name, _nameEquipWeapon);
        }
    }

    private void PrintName(string name, TextMeshProUGUI nameEquip)
    {
        if (nameEquip.text != name)
        {
            nameEquip.SetText(name);
        }
    }
}