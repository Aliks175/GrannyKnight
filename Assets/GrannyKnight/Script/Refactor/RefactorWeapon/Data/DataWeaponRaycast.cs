using UnityEngine;

namespace Refactor
{

    public class DataWeaponRaycast : DataWeapon
    {
        #region PublicField
        public float Range => _range;
        public bool IsAutoFire => _isAutoFire;

        #endregion
        private float _range;
        private bool _isAutoFire;

        public DataWeaponRaycast(WeaponSetting weaponSetting)
        {
            Initialization(weaponSetting);
        }

        protected override void Initialization(WeaponSetting weaponSetting)
        {
            base.Initialization(weaponSetting);

            if (weaponSetting is RaycastWeaponSetting)
            {
                RaycastWeaponSetting _raycastWeapon = weaponSetting as RaycastWeaponSetting;
                _range = _raycastWeapon.Range;
                _isAutoFire = _raycastWeapon.IsAutoFire;
            }
            else
            {
                Debug.LogError($"Not Found WeaponSetting");
                return;
            }
        }
    }
}