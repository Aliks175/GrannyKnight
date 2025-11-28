using UnityEngine;

public class InputControl : MonoBehaviour
{
    [SerializeField] private PlayerControlAnimation playerControlAnimation;
    private PlayerSystemActions _playerInput;
    private PlayerSystemActions.PlayerActions _playerActions;
    private PlayerMover _playerMover;
    private PlayerLook _playerLook;
    private PlayerAim _playerAim;
    private PlayerInteracteble _playerInteracteble;
    private PlayerChooseWeapon _playerChooseWeapon;
    private WeaponSystem _weaponSystem;
    private bool _isPlayerControl;

    public void Initialization(WeaponSystem weaponSystem)
    {
        _playerInput = new PlayerSystemActions();
        _playerActions = _playerInput.Player;
        _playerMover = GetComponent<PlayerMover>();
        _playerLook = GetComponent<PlayerLook>();
        _playerAim = GetComponent<PlayerAim>();
        _playerInteracteble = GetComponent<PlayerInteracteble>();
        _playerChooseWeapon = GetComponent<PlayerChooseWeapon>();
        _weaponSystem = weaponSystem;
        _isPlayerControl = true;
        SetUp();
    }

    public void ControlMovePlayer(bool isPlayerControl)
    {
        _isPlayerControl = isPlayerControl;
        playerControlAnimation.ControlMovePlayer(isPlayerControl);
    }

    private void SetUp()
    {
        _playerActions.Enable();
        _playerActions.Jump.performed += Context =>
        {
            if (_isPlayerControl)
            {
            _playerMover.Jump();
            }
        };
        _playerActions.Aim.started += Context => _playerAim.AimingOn();
        _playerActions.Aim.canceled += Context => _playerAim.AimingOff();
        _playerActions.Aim.started += _playerMover.ActiveAimSpeed;
        _playerActions.Aim.canceled += _playerMover.ActiveAimSpeed;
        _playerActions.Interact.started += Context => _playerInteracteble.OnInteracteble(true);
        _playerActions.Interact.canceled += Context => _playerInteracteble.OnInteracteble(false);
        _playerActions.Shoot.started += _weaponSystem.Shoot;
        _playerActions.Shoot.canceled += _weaponSystem.Shoot;
    }

    private void OnDisable()
    {
        _playerActions.Jump.performed -= Context =>
        {
            if (_isPlayerControl)
            {
                _playerMover.Jump();
            }
        };
        _playerActions.Aim.started -= Context => _playerAim.AimingOn();
        _playerActions.Aim.canceled -= Context => _playerAim.AimingOff();
        _playerActions.Aim.started -= _playerMover.ActiveAimSpeed;
        _playerActions.Aim.canceled -= _playerMover.ActiveAimSpeed;
        _playerActions.Interact.started -= Context => _playerInteracteble.OnInteracteble(true);
        _playerActions.Interact.canceled -= Context => _playerInteracteble.OnInteracteble(false);
        _playerActions.Shoot.started -= _weaponSystem.Shoot;
        _playerActions.Shoot.canceled -= _weaponSystem.Shoot;
        _playerActions.Disable();
    }

    private void Update()
    {
        if (_isPlayerControl)
        {
            _playerMover.ProcessMove(_playerActions.Move.ReadValue<Vector2>());
        }
    }

    private void LateUpdate()
    {
        if (_isPlayerControl)
        {
            _playerLook.ProcessLook(_playerActions.Look.ReadValue<Vector2>());
        }
    }
}