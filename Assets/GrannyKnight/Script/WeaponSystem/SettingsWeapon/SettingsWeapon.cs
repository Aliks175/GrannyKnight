using UnityEngine;

namespace Refactor
{
    [CreateAssetMenu(fileName = "SettingsWeapon", menuName = "Create/SettingsWeapon")]
    public class SettingsWeapon : ScriptableObject
    {
        public WeaponSetting DataWeapon => _dataWeapon;
        public DataItem DataItem => _dataItem;
        [Header("Weapon")]
        [SerializeField] private WeaponSetting _dataWeapon;
        [Header("Settings")]
        [SerializeField] private DataItem _dataItem;
    }
}