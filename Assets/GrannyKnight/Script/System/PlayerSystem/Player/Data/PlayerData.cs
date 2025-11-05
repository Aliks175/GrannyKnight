using UnityEngine;

public class PlayerData : MonoBehaviour, IPlayerDatable
{
    public PlayerChooseWeapon ChooseWeapon => _chooseWeapon;
    public PlayerInventory Inventory => _inventory;
    public PlayerCharacter PlayerCharacter => _playerCharacter;
    public PlayerWeaponThrow PlayerWeaponThrow => _playerWeaponThrow;
    [SerializeField] private PlayerChooseWeapon _chooseWeapon;
    [SerializeField] private PlayerInventory _inventory;
    [SerializeField] private PlayerWeaponThrow _playerWeaponThrow;
    private PlayerCharacter _playerCharacter;

    private void OnDisable()
    {
        _chooseWeapon.OnSetThrowWeapon -= _playerWeaponThrow.SetWeapon;
    }

    public void Initialization(PlayerCharacter playerCharacter, Camera camera)
    {
        _playerCharacter = playerCharacter;
        _inventory.Initialization(this);
        _chooseWeapon.Initialization();
        _playerWeaponThrow.Initialization(camera, _inventory);
        SetUp();
    }

    private void SetUp()
    {
        _chooseWeapon.OnSetThrowWeapon += _playerWeaponThrow.SetWeapon;
    }
}

public interface IPlayerDatable
{
    public PlayerInventory Inventory { get; }
    public PlayerCharacter PlayerCharacter { get; }
    public PlayerChooseWeapon ChooseWeapon { get; }
    public PlayerWeaponThrow PlayerWeaponThrow { get; }
    public void Initialization(PlayerCharacter playerCharacter, Camera camera);
}