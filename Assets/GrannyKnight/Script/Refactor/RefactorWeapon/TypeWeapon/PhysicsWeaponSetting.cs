using UnityEngine;

namespace Refactor
{
    [CreateAssetMenu(fileName = "PhysicsWeapon", menuName = "Create/Weapon/PhysicsWeapon")]
    public class PhysicsWeaponSetting : WeaponSetting
    {
        #region PublicField
       public Bullet PrefBullet => _prefBullet;
       public float MaxForce => _maxForce;
       public float MinForce => _minForce;
       public float MaxAngle => _maxAngle;
       public float MinAngle => _minAngle;
       public float TimeWaitMaxForce => _timeWaitMaxForce;
       public int SizePoolBullet => _sizePoolBullet;
        #endregion

        [Header("PhysicsSetting")]
        [SerializeField] private Bullet _prefBullet; 
        [SerializeField] private float _maxForce;
        [SerializeField] private float _minForce;
        [SerializeField] private float _maxAngle;
        [SerializeField] private float _minAngle;
        [SerializeField] private float _timeWaitMaxForce;
        [SerializeField] private int _sizePoolBullet;
     
    }
}

//* Префаб снаряда (С Rigidbody)
//*Сила максимальная
//* Сила минимальная 
//* Максимальный угол подъема направления 
//* Минимальный угол подъема направления 
//* Время за которое достигается максимальная сила 