using UnityEngine;

public class UiSystem : MonoBehaviour
{
    //[SerializeField] private UiEquip _uiEquip;
    private PlayerChooseWeapon _playerChooseWeapon;
    //private PlayerWeaponThrow _playerWeaponThrow;
    private PlayerInventory _playerInventory;

    private void OnDisable()
    {
        //_playerChooseWeapon.OnChangeWeapon -= Context => _uiEquip.ChangeWeaponName(Context.Name);
        //_playerWeaponThrow.OnChangeWeapon -= Context => _uiEquip.ChangeBonusWeapon();
    }

    public void Initialization(IPlayerDatable playerDatable)
    {
        _playerChooseWeapon = playerDatable.ChooseWeapon;
        //_playerWeaponThrow = playerDatable.PlayerWeaponThrow;
        _playerInventory = playerDatable.Inventory;
        //_uiEquip.Initialization(_playerInventory, _playerWeaponThrow);
        SetUp();
    }

    private void SetUp()
    {
        //_playerWeaponThrow.OnChangeWeapon += Context => _uiEquip.ChangeBonusWeapon();
        //_playerChooseWeapon.OnChangeWeapon += Context => _uiEquip.ChangeWeaponName(Context.Name);
    }
}