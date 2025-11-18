using UnityEngine;

public class PlayerData : MonoBehaviour, IPlayerDatable
{
    public PlayerChooseWeapon ChooseWeapon => _chooseWeapon;
    public PlayerInventory Inventory => _inventory;
    public PlayerCharacter PlayerCharacter => _playerCharacter;
    //public PlayerWeaponThrow PlayerWeaponThrow => _playerWeaponThrow;
    public PlayerQuestControl PlayerQuestControl => _playerQuestControl;

    [SerializeField] private PlayerChooseWeapon _chooseWeapon;
    [SerializeField] private PlayerInventory _inventory;
    [SerializeField] private PlayerWeaponThrow _playerWeaponThrow;
    [SerializeField] private PlayerQuestControl _playerQuestControl;
    private PlayerCharacter _playerCharacter;

    //private void OnDisable()
    //{
    //    _chooseWeapon.OnSetThrowWeapon -= _playerWeaponThrow.SetWeapon;
    //}

    public void Initialization(PlayerCharacter playerCharacter, Transform head)
    {
        _playerCharacter = playerCharacter;
        _inventory.Initialization(this);
        _chooseWeapon.Initialization();
        _playerWeaponThrow.Initialization(head, _inventory);
        _playerQuestControl.Initialization(this);
        //SetUp();
    }

    //private void SetUp()
    //{
    //    _chooseWeapon.OnSetThrowWeapon += _playerWeaponThrow.SetWeapon;
    //}
}

public interface IPlayerDatable
{
    public PlayerInventory Inventory { get; }
    public PlayerCharacter PlayerCharacter { get; }
    public PlayerChooseWeapon ChooseWeapon { get; }
    public PlayerQuestControl PlayerQuestControl { get; }
    //public PlayerWeaponThrow PlayerWeaponThrow { get; }
    public void Initialization(PlayerCharacter playerCharacter, Transform head);
}