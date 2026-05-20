using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeapon", menuName = "Create/Weapon/MeleeWeapon")]
public class MeleeWeaponSetting : WeaponSetting
{
    #region PublicField
    public Bullet PrefBullet => _prefBullet;
    public LayerMask LayerEnemy => _layerEnemy;
    public float TimeWaitNextHit => _timeWaitNextHit;
    public float TimeActiveHit => _timeActiveHit;
    public float SpeedBullet => _speedBullet;
    public float RangeHit => _rangeHit;
    public int DamageHit => _damageHit;
    public int SizePoolBullet => _sizePoolBullet;
    #endregion

    [Header("ShootSetting")]
    [SerializeField] private Bullet _prefBullet;
    [SerializeField] private float _speedBullet;
    [SerializeField] private int _sizePoolBullet;
    [Header("MeleeSetting")]
    [SerializeField] private LayerMask _layerEnemy;
    [SerializeField] private float _timeWaitNextHit;
    [SerializeField] private float _timeActiveHit;
    [SerializeField] private float _rangeHit;
    [SerializeField] private int _damageHit;
}