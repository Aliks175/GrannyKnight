namespace Refactor
{
    public abstract class DataWeapon
    {
        #region PublicField
        public TypeWeapon TypeWeapon => _typeWeapon;
        public TypeShootingSystem TypeShootingSystem => _typeShootingSystem;
        public float TimeWaitFire => _timeWaitFire;
        public int Damage => _damage;
        #endregion

        private TypeWeapon _typeWeapon;
        private TypeShootingSystem _typeShootingSystem;
        private float _timeWaitFire;
        private int _damage;

        protected virtual void Initialization(WeaponSetting weaponSetting)
        {
            _typeWeapon = weaponSetting.TypeWeapon;
            _typeShootingSystem = weaponSetting.TypeShootingSystem;
            _timeWaitFire = weaponSetting.TimeWaitFire;
            _damage = weaponSetting.Damage;
        }
    }
}