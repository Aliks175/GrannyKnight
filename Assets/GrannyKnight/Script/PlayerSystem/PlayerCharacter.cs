using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacter : MonoBehaviour
{
    public PlayerMove PlayerMove => _playerMove;
    public PlayerLook PlayerLook => _playerLook;
    public PlayerAim PlayerAim => _playerAim;
    public PlayerWeapon PlayerWeapon => _playerWeapon;
    public PlayerHealth PlayerHealth => _playerHealth;
    public CharacterController CharacterController => _characterController;

    private CharacterController _characterController;
    private PlayerMove _playerMove;
    private PlayerWeapon _playerWeapon;
    private PlayerHealth _playerHealth;
    private PlayerLook _playerLook;
    private PlayerAim _playerAim;
    private SystemBuss _systemBuss;

    [Inject]
    public void Construct(PlayerMove playerMove, PlayerLook playerLook, PlayerAim playerAim, PlayerWeapon playerWeapon, SystemBuss systemBuss, PlayerHealth playerHealth)
    {
        _playerMove = playerMove;
        _playerLook = playerLook;
        _playerAim = playerAim;
        _playerHealth = playerHealth;
        _playerWeapon = playerWeapon;
        _systemBuss = systemBuss;
        _characterController = GetComponent<CharacterController>();
    }

    private void Awake()
    {
        _systemBuss.SpawnPlayer(this);
    }

    public void SetStrategyHealtheble(IPlayerStrategyHealtheble strategy)
    {
        PlayerHealth.SetStrategyHealtheble(strategy);
    }

    public void TakeDamage(int Damage)
    {
        _playerHealth.TakeDamage(Damage);
    }

    public void GiveWeapon(EquipHand equipHand)
    {
        PlayerWeapon.GiveWeapon(equipHand);
    }

    public void ChangeSize(float size, float minSize = 0.5f)
    {
        size = Mathf.Clamp01(size);
        size = size >= 0.1f ? size : minSize;
        transform.localScale = Vector3.one * size;
        _playerMove.ChangeCoefficientKidSpeedMove(0.8f);
    }
}