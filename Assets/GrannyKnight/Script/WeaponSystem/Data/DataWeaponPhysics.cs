using UnityEngine;

public class DataWeaponPhysics : DataWeapon
{
    public Bullet PrefBullet => _prefBullet;
    public float MaxForce => _maxForce;
    public float MinForce => _minForce;
    public float MaxAngle => _maxAngle;
    public float MinAngle => _minAngle;
    public float TimeWaitMaxForce => _timeWaitMaxForce;

    private FactoryBullet _factoryBullet;
    private Bullet _prefBullet;
    private float _maxForce;
    private float _minForce;
    private float _maxAngle;
    private float _minAngle;
    private float _timeWaitMaxForce;
    private int _sizePoolBullet;
    private const string nameParent = "BulletPhysicsPool";

    public DataWeaponPhysics(WeaponSetting weaponSetting)
    {
        Initialization(weaponSetting);
        _factoryBullet = new(_prefBullet, _sizePoolBullet, Damage, 3f, nameParent);
    }

    protected override void Initialization(WeaponSetting weaponSetting)
    {
        base.Initialization(weaponSetting);
        if (weaponSetting is PhysicsWeaponSetting)
        {
            PhysicsWeaponSetting _physicsWeapon = weaponSetting as PhysicsWeaponSetting;
            _prefBullet = _physicsWeapon.PrefBullet;
            _maxForce = _physicsWeapon.MaxForce;
            _minForce = _physicsWeapon.MinForce;
            _maxAngle = _physicsWeapon.MaxAngle;
            _minAngle = _physicsWeapon.MinAngle;
            _timeWaitMaxForce = _physicsWeapon.TimeWaitMaxForce;
            _sizePoolBullet = _physicsWeapon.SizePoolBullet;
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