using System;
using UnityEngine;
using Zenject;

namespace Refactor
{
    public class ImporterPlayerWeaponSystem : IDisposable, IInitializable
    {
        private PlayerWeapon _playerWeapon;
        private TestWeaponSystem _weaponSystem;
       
        public ImporterPlayerWeaponSystem(PlayerWeapon playerWeapon, TestWeaponSystem weaponSystem)
        {
            _playerWeapon = playerWeapon;
            _weaponSystem = weaponSystem;
        }
        
        public void Dispose()
        {
            _playerWeapon.OnChangeHand -= OnChangeWeapon;
        }

        public void Initialize()
        {
            _playerWeapon.OnChangeHand += OnChangeWeapon;
        }

        private void OnChangeWeapon(object sender, OnDataEquipHand dataEquipHand)
        {
            TestWeapon weapon = dataEquipHand.Weapon;
            if (weapon == null)
            {
                _weaponSystem.ChangeShootSystem(TypeShootingSystem.none);
            }
            else
            {
                Debug.Log($"ImporterPlayerWeaponSystem - OnChangeWeapon {weapon.TypeShootingSystem}");
                _weaponSystem.ChangeShootSystem(weapon.TypeShootingSystem);
            }
            _weaponSystem.SetWeapon(weapon);
        }
    }
}