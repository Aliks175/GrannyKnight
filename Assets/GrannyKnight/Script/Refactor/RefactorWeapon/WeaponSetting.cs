using UnityEngine;

namespace Refactor
{
    public abstract class WeaponSetting : ScriptableObject
    {
        #region PublicField
        public TypeWeapon TypeWeapon => _typeWeapon;
        public TypeShootingSystem TypeShootingSystem => _typeShootingSystem;
        public float TimeWaitFire => _timeWaitFire;
        public int Damage => _damage;
        #endregion
        
        [Header("GlobalSetting")]
        [SerializeField] private TypeWeapon _typeWeapon;
        [SerializeField] private TypeShootingSystem _typeShootingSystem;
        [SerializeField] private float _timeWaitFire;
        [SerializeField] private int _damage;
    }
}