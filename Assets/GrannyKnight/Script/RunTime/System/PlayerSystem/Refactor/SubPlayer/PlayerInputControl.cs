using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Refactor
{
    public class PlayerInputControl : IDisposable, IInitializable, ILateTickable, ITickable
    {
        private TestPlayerMove _playerMover;
        private TestPlayerLook _playerLook;
        private TestPlayerAim _playerAim;
        private TestPlayerInteracteble _playerInteracteble;
        //[SerializeField] private PlayerControlAnimation playerControlAnimation;
        private PlayerSystemActions _playerInput;
        private PlayerSystemActions.PlayerActions _playerActions;
        //private PlayerMover _playerMover;
        //private PlayerAim _playerAim;
        //private PlayerInteracteble _playerInteracteble;
        //private PlayerChooseWeapon _playerChooseWeapon;
        //private WeaponSystem _weaponSystem;
        //private PlayerHintSystem _playerHintSystem;
        private bool _isPlayerControl;

        public PlayerInputControl(TestPlayerCharacter testPlayerCharacter, PlayerSystemActions inputActions, TestPlayerInteracteble testPlayerInteracteble)
        {
            //_playerCharacter = playerCharacter;
            _playerInteracteble = testPlayerInteracteble;
            _playerMover = testPlayerCharacter.PlayerMove;
            _playerLook = testPlayerCharacter.PlayerLook;
            _playerAim = testPlayerCharacter.PlayerAim;
            _playerInput = inputActions;
            _playerActions = inputActions.Player;
        }

        public void Dispose()
        {
            _playerActions.Jump.performed -= Jump;
            _playerActions.Aim.started -= AimControl;
            _playerActions.Aim.canceled -= AimControl;
            _playerActions.Interact.started -= OnInteracteble;
            _playerActions.Disable();
        }

        public void Initialize()
        {
            _playerActions.Enable();
            _isPlayerControl = true;
            _playerActions.Jump.performed += Jump;

            _playerActions.Aim.started += AimControl;
            _playerActions.Aim.canceled += AimControl;
            _playerActions.Interact.started += OnInteracteble;
            //_playerActions.Interact.canceled += OnInteracteble;
            //_playerActions.Shoot.started += _weaponSystem.Shoot;
            //_playerActions.Shoot.canceled += _weaponSystem.Shoot;
            //_playerActions.Help.started += Context => _playerHintSystem.MoveTarget();
        }

        public void Tick()
        {
            if (!_isPlayerControl) { return; }
            _playerMover.ProcessMove(_playerActions.Move.ReadValue<Vector2>());
        }

        public void LateTick()
        {
            if (!_isPlayerControl) { return; }
            _playerLook.ProcessLook(_playerActions.Look.ReadValue<Vector2>());
        }

        private void OnInteracteble(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                _playerInteracteble.OnInteracteble();
            }
        }

        private void Jump(InputAction.CallbackContext context)
        {
            _playerMover.Jump();
        }

        private void AimControl(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                _playerAim.ProcessAim(true);
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                _playerAim.ProcessAim(false);
            }
        }

        public void Initialization(WeaponSystem weaponSystem)
        {

            //_playerMover = GetComponent<PlayerMover>();
            //_playerLook = GetComponent<PlayerLook>();
            //_playerAim = GetComponent<PlayerAim>();
            //_playerInteracteble = GetComponent<PlayerInteracteble>();
            //_playerChooseWeapon = GetComponent<PlayerChooseWeapon>();
            //_playerHintSystem = GetComponent<PlayerHintSystem>();
            //_weaponSystem = weaponSystem;
            //_isPlayerControl = true;
            //SetUp();
        }

        //public void ControlMovePlayer(bool isPlayerControl)
        //{
        //    //_isPlayerControl = isPlayerControl;
        //    //playerControlAnimation.ControlMovePlayer(isPlayerControl);
        //}

        //private void SetUp()
        //{
        //    //_playerActions.Enable();
        //    //_playerActions.Jump.performed += Context =>
        //    //{
        //    //    if (_isPlayerControl)
        //    //    {
        //    //        _playerMover.Jump();
        //    //    }
        //    //};
        //    //_playerActions.Aim.started += Context => _playerAim.AimingOn();
        //    //_playerActions.Aim.canceled += Context => _playerAim.AimingOff();
        //    //_playerActions.Aim.started += _playerMover.ActiveAimSpeed;
        //    //_playerActions.Aim.canceled += _playerMover.ActiveAimSpeed;
        //    //_playerActions.Interact.started += Context => _playerInteracteble.OnInteracteble(true);
        //    //_playerActions.Interact.canceled += Context => _playerInteracteble.OnInteracteble(false);
        //    //_playerActions.Shoot.started += _weaponSystem.Shoot;
        //    //_playerActions.Shoot.canceled += _weaponSystem.Shoot;
        //    //_playerActions.Help.started += Context => _playerHintSystem.MoveTarget();
        //}

        //private void OnDisable()
        //{
        //    //_playerActions.Jump.performed -= Context =>
        //    //{
        //    //    if (_isPlayerControl)
        //    //    {
        //    //        _playerMover.Jump();
        //    //    }
        //    //};
        //    //_playerActions.Aim.started -= Context => _playerAim.AimingOn();
        //    //_playerActions.Aim.canceled -= Context => _playerAim.AimingOff();
        //    //_playerActions.Aim.started -= _playerMover.ActiveAimSpeed;
        //    //_playerActions.Aim.canceled -= _playerMover.ActiveAimSpeed;
        //    //_playerActions.Interact.started -= Context => _playerInteracteble.OnInteracteble(true);
        //    //_playerActions.Interact.canceled -= Context => _playerInteracteble.OnInteracteble(false);
        //    //_playerActions.Shoot.started -= _weaponSystem.Shoot;
        //    //_playerActions.Shoot.canceled -= _weaponSystem.Shoot;
        //    //_playerActions.Help.started -= Context => _playerHintSystem.MoveTarget();
        //    //_playerActions.Disable();
        //}

        //private void Update()
        //{
        //    if (true)
        //    {
        //        _playerCharacter.PlayerMove.ProcessMove(_playerActions.Move.ReadValue<Vector2>());
        //    }
        //}

        //private void LateUpdate()
        //{
        //    //if (_isPlayerControl)
        //    //{
        //    //    _playerLook.ProcessLook(_playerActions.Look.ReadValue<Vector2>());
        //    //}
        //}


    }

}
