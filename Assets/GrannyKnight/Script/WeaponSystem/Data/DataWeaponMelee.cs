using UnityEngine;

public class DataWeaponMelee : DataWeapon
{
    #region PublicField
    public LayerMask LayerEnemy => _layerEnemy;
    public float TimeWaitNextHit => _timeWaitNextHit;
    public float TimeActiveHit => _timeActiveHit;
    public float SpeedBullet => _speedBullet;
    public float RangeHit => _rangeHit;
    public int DamageHit => _damageHit;
    #endregion

    private FactoryBullet _factoryBullet;
    private Bullet _prefBullet;
    private LayerMask _layerEnemy;
    private float _speedBullet;
    private float _timeActiveHit;
    private float _timeWaitNextHit;
    private float _rangeHit;
    private int _damageHit;
    private int _sizePoolBullet;
    private const string nameParent = "BulletMeleePool";

    public DataWeaponMelee(WeaponSetting weaponSetting)
    {
        Initialization(weaponSetting);
        _factoryBullet = new(_prefBullet, _sizePoolBullet, Damage, 3f, nameParent);
    }

    protected override void Initialization(WeaponSetting weaponSetting)
    {
        base.Initialization(weaponSetting);
        if (weaponSetting is MeleeWeaponSetting)
        {
            MeleeWeaponSetting _meleeWeapon = weaponSetting as MeleeWeaponSetting;
            _prefBullet = _meleeWeapon.PrefBullet;
            _timeWaitNextHit = _meleeWeapon.TimeWaitNextHit;
            _damageHit = _meleeWeapon.DamageHit;
            _sizePoolBullet = _meleeWeapon.SizePoolBullet;
            _speedBullet = _meleeWeapon.SpeedBullet;
            _rangeHit = _meleeWeapon.RangeHit;
            _layerEnemy = _meleeWeapon.LayerEnemy;
            _timeActiveHit = _meleeWeapon.TimeActiveHit;
        }
        else
        {
            Debug.LogError($"Not Found WeaponSetting");
            return;
        }
    }

    public Bullet GetBullet()
    {
        Bullet tempBullet = _factoryBullet.GetBullet();
        tempBullet.Shoot();
        return tempBullet;
    }
}