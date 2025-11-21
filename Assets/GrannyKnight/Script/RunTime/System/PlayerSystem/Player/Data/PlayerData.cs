using UnityEngine;

public class PlayerData : MonoBehaviour, IPlayerDatable
{
    public PlayerChooseWeapon ChooseWeapon => _chooseWeapon;
    public PlayerInventory Inventory => _inventory;
    public PlayerCharacter PlayerCharacter => _playerCharacter;
    public PlayerQuestControl PlayerQuestControl => _playerQuestControl;

    [SerializeField] private PlayerChooseWeapon _chooseWeapon;
    [SerializeField] private PlayerInventory _inventory;
    [SerializeField] private PlayerQuestControl _playerQuestControl;
    private PlayerCharacter _playerCharacter;

    public void Initialization(PlayerCharacter playerCharacter)
    {
        _playerCharacter = playerCharacter;
        _inventory.Initialization(this);
        _chooseWeapon.Initialization();
        _playerQuestControl.Initialization(this);
    }
}

public interface IPlayerDatable
{
    public PlayerInventory Inventory { get; }
    public PlayerCharacter PlayerCharacter { get; }
    public PlayerChooseWeapon ChooseWeapon { get; }
    public PlayerQuestControl PlayerQuestControl { get; }
    public void Initialization(PlayerCharacter playerCharacter);
}