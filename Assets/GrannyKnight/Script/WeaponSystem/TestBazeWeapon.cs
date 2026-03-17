using UnityEngine;

    [CreateAssetMenu(fileName = "TestBazeWeapon", menuName = "Create/Weapon/BazeWeapon")]
    public class TestBazeWeapon : ScriptableObject
    {
        [Header("WeaponSettings")]
        public WeaponSetting DataWeapon;
        [Header("ItemSettings")]
        public DataItem DataItem;
    }