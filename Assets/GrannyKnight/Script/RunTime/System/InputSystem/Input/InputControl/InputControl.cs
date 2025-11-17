using UnityEngine;

public class InputControl : MonoBehaviour
{
    private PlayerSystemActions _playerInput;
    private PlayerSystemActions.PlayerActions _playerActions;
    private PlayerMover _playerMover;
    private PlayerLook _playerLook;
    private PlayerAim _playerAim;
    private PlayerInteracteble _playerInteracteble;
    private PlayerChooseWeapon _playerChooseWeapon;
    private PlayerWeaponThrow _playerWeaponThrow;
    private WeaponSystem _weaponSystem;
    private InventorySystem _inventorySystem;

    public void Initialization(WeaponSystem weaponSystem, InventorySystem inventorySystem)
    {
        _playerInput = new PlayerSystemActions();
        _playerActions = _playerInput.Player;
        _playerMover = GetComponent<PlayerMover>();
        _playerLook = GetComponent<PlayerLook>();
        _playerAim = GetComponent<PlayerAim>();
        _playerInteracteble = GetComponent<PlayerInteracteble>();
        _playerChooseWeapon = GetComponent<PlayerChooseWeapon>();
        _playerWeaponThrow = GetComponent<PlayerWeaponThrow>();
        _weaponSystem = weaponSystem;
        _inventorySystem = inventorySystem;
        SetUp();
    }

    private void SetUp()
    {
        _playerActions.Enable();
        _playerActions.Jump.performed += Context => _playerMover.Jump();
        _playerActions.Aim.started += Context => _playerAim.AimingOn();
        _playerActions.Aim.canceled += Context => _playerAim.AimingOff();
        _playerActions.Aim.started += _playerMover.ActiveAimSpeed;
        _playerActions.Aim.canceled += _playerMover.ActiveAimSpeed;
        _playerActions.Interact.started += Context => _playerInteracteble.OnInteracteble(true);
        _playerActions.Interact.canceled += Context => _playerInteracteble.OnInteracteble(false);
        _playerActions.Inventory.started += Context => _inventorySystem.ShowInventory();
        _playerActions.FastSlotOne.started += Context => _playerChooseWeapon.GiveWeapon(SlotNumber.OneSlot);
        _playerActions.FastSlotTwo.started += Context => _playerChooseWeapon.GiveWeapon(SlotNumber.TwoSlot);
        _playerActions.ChangeItem.started += Context => _playerChooseWeapon.ChangeSlot();
        _playerActions.Shoot.started += _weaponSystem.Shoot;
        _playerActions.Shoot.canceled += _weaponSystem.Shoot;
        _playerActions.Throw.performed += Context => _playerWeaponThrow.Throw();
    }

    private void OnDisable()
    {
        _playerActions.Jump.performed -= Context => _playerMover.Jump();
        _playerActions.Aim.started -= Context => _playerAim.AimingOn();
        _playerActions.Aim.canceled -= Context => _playerAim.AimingOff();
        _playerActions.Aim.started -= _playerMover.ActiveAimSpeed;
        _playerActions.Aim.canceled -= _playerMover.ActiveAimSpeed;
        _playerActions.Interact.started -= Context => _playerInteracteble.OnInteracteble(true);
        _playerActions.Interact.canceled -= Context => _playerInteracteble.OnInteracteble(false);
        _playerActions.Inventory.started -= Context => _inventorySystem.ShowInventory();
        _playerActions.FastSlotOne.started -= Context => _playerChooseWeapon.GiveWeapon(SlotNumber.OneSlot);
        _playerActions.FastSlotTwo.started -= Context => _playerChooseWeapon.GiveWeapon(SlotNumber.TwoSlot);
        _playerActions.ChangeItem.started -= Context => _playerChooseWeapon.ChangeSlot();
        _playerActions.Shoot.started -= _weaponSystem.Shoot;
        _playerActions.Shoot.canceled -= _weaponSystem.Shoot;
        _playerActions.Throw.performed -= Context => _playerWeaponThrow.Throw();
        _playerActions.Disable();
    }

    private void Update()
    {
        _playerMover.ProcessMove(_playerActions.Move.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        _playerLook.ProcessLook(_playerActions.Look.ReadValue<Vector2>());
    }
}