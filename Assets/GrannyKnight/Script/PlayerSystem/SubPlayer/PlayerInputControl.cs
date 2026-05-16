using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using static Unity.Collections.AllocatorManager;

namespace Refactor
{
    public class PlayerInputControl : IDisposable, IInitializable, ILateTickable, ITickable
    {
        private PlayerMove _playerMover;
        private PlayerLook _playerLook;
        private PlayerAim _playerAim;
        private PlayerInteracteble _playerInteracteble;
        private TestWeaponSystem _weaponSystem;
        private PlayerSystemActions _playerInput;
        private PlayerSystemActions.PlayerActions _playerActions;
        private bool _isPlayerControl;

        public PlayerInputControl(PlayerCharacter testPlayerCharacter, PlayerSystemActions inputActions, PlayerInteracteble testPlayerInteracteble, TestWeaponSystem testWeaponSystem)
        {
            _weaponSystem = testWeaponSystem;
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
            //_playerActions.Aim.started -= AimControl;
            //_playerActions.Aim.canceled -= AimControl;
            _playerActions.Interact.started -= OnInteracteble;
            _playerActions.Shoot.canceled -= OnShoot;
            _playerActions.Shoot.started -= OnShoot;
            _playerActions.ShootTwo.started -= OnShootTwo;
            _playerActions.ShootTwo.canceled -= OnShootTwo;
            _playerActions.Block.started -= BlockControl;
            _playerActions.Block.canceled -= BlockControl;
            _playerActions.Disable();
        }

        public void Initialize()
        {
            _playerActions.Enable();
            _isPlayerControl = true;
            _playerActions.Jump.performed += Jump;
            //_playerActions.Aim.started += AimControl;
            //_playerActions.Aim.canceled += AimControl;
            _playerActions.Interact.started += OnInteracteble;
            _playerActions.Shoot.started += OnShoot;
            _playerActions.Shoot.canceled += OnShoot;
            _playerActions.ShootTwo.started += OnShootTwo;
            _playerActions.ShootTwo.canceled += OnShootTwo;
            _playerActions.Block.started += BlockControl;
            _playerActions.Block.canceled += BlockControl;
        }

        private void OnShoot(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                _weaponSystem.AttackOne(true);
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                _weaponSystem.AttackOne(false);
            }
        }

        private void OnShootTwo(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                _weaponSystem.AttackTwo(true);
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                _weaponSystem.AttackTwo(false);
            }
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

        private void BlockControl(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Started)
            {
                _weaponSystem.Block(true);
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                _weaponSystem.Block(false);
            }
        }

        //private void AimControl(InputAction.CallbackContext context)
        //{
        //    if (context.phase == InputActionPhase.Started)
        //    {
        //        _playerAim.ProcessAim(true);
        //    }
        //    else if (context.phase == InputActionPhase.Canceled)
        //    {
        //        _playerAim.ProcessAim(false);
        //    }
        //}
    }
}