using UnityEngine;

[CreateAssetMenu(fileName = "RaycastWeapon", menuName = "Create/Weapon/RaycastWeapon")]
public class RaycastWeaponSetting : WeaponSetting
{
    #region PublicField
    public float Range => _range;
    public bool IsAutoFire => _isAutoFire;

    #endregion
    [Header("RaycastWeapon")]
    [SerializeField] private float _range;
    [SerializeField] private bool _isAutoFire;
}