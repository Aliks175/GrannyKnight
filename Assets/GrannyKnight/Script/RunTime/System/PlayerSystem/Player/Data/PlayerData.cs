using UnityEngine;

public class PlayerData : MonoBehaviour, IPlayerDatable
{
    public PlayerChooseWeapon ChooseWeapon => _chooseWeapon;
    public PlayerCharacter PlayerCharacter => _playerCharacter;
    public PlayerQuestControl PlayerQuestControl => _playerQuestControl;

    [SerializeField] private PlayerChooseWeapon _chooseWeapon;
    [SerializeField] private PlayerQuestControl _playerQuestControl;
    private PlayerCharacter _playerCharacter;

    public void Initialization(PlayerCharacter playerCharacter)
    {
        _playerCharacter = playerCharacter;
        _chooseWeapon.Initialization(this);
        _playerQuestControl.Initialization(this);
    }
}

public interface IPlayerDatable
{
    public PlayerCharacter PlayerCharacter { get; }
    public PlayerChooseWeapon ChooseWeapon { get; }
    public PlayerQuestControl PlayerQuestControl { get; }
    public void Initialization(PlayerCharacter playerCharacter);
}