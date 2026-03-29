using UnityEngine;
using Zenject;

public class PlayerCharacter : MonoBehaviour
{
    public PlayerMove PlayerMove => _playerMove;
    public PlayerLook PlayerLook => _playerLook;
    public PlayerAim PlayerAim => _playerAim;
    public PlayerWeapon PlayerWeapon => _playerWeapon;

    private PlayerMove _playerMove;
    private PlayerWeapon _playerWeapon;
    private PlayerLook _playerLook;
    private PlayerAim _playerAim;
    private SystemBuss _systemBuss;

    [Inject]
    public void Construct(PlayerMove playerMove, PlayerLook playerLook, PlayerAim playerAim, PlayerWeapon playerWeapon, SystemBuss systemBuss)
    {
        _playerMove = playerMove;
        _playerLook = playerLook;
        _playerAim = playerAim;
        _playerWeapon = playerWeapon;
        _systemBuss = systemBuss;
    }

    private void Awake()
    {
        _systemBuss.SpawnPlayer(this);
    }

    public void GiveWeapon(EquipHand equipHand)
    {
        PlayerWeapon.GiveWeapon(equipHand);
    }

    public void Teleport()
    {

    }
}