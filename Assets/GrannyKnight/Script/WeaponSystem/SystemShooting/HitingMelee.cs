using Cysharp.Threading.Tasks;
using Refactor;
using System;
using System.Threading;
using UnityEngine;
using Zenject;

public class HitingMelee : IShootingSystemble, IDisposable, IInitializable
{
    private PlayerHealth _playerHealth;
    private TestWeapon _weapon;
    private CancellationTokenSource _cancellationToken;
    private DataWeaponMelee _meleeWeapon;
    private Transform _head;
    private Collider[] _targets;

    private float _nextTimeToFire;
    private float _nextTimeToHit;
    private int _currentId;
    private bool _isBlock;

    public event Action OnAttackOne;
    public event Action OnAttackTwo;
    public event Action<bool> OnBlock;

    public HitingMelee(Transform head, PlayerCharacter playerCharacter)
    {
        _head = head;
        _currentId = -1;
        _targets = new Collider[5];
        _playerHealth = playerCharacter.PlayerHealth;
    }

    public void Initialize()
    {
        _cancellationToken?.Dispose();
        _cancellationToken = new CancellationTokenSource();
    }

    public void Dispose()
    {
        _cancellationToken?.Cancel();
        _cancellationToken?.Dispose();
    }

    public void AttackOne(TestWeapon testWeapon)
    {
        if (!CheckAttack(testWeapon)) { return; }

        HitMelee();
    }

    public void Block(bool isActive)
    {
        _isBlock = isActive;
        _playerHealth.Block(isActive);
        OnBlock?.Invoke(isActive);
    }

    public void AttackTwo(TestWeapon testWeapon)
    {
        if (!CheckAttack(testWeapon)) { return; }

        ShootDistance();
        //if (_raycastWeapon.IsAutoFire)
        //{
        //    ShootAutoFire();
        //}
        //else
        //{
        //    ShootSingleFire();
        //}
    }

    public void StopShoot()
    {
        ////OnEndFire?.Invoke();
        //_isFire = false;

    }

    private bool ControlCurrentWeapon(TestWeapon testWeapon)
    {
        bool isSuccess = true;
        if (_currentId == testWeapon.ID) { return isSuccess; }
        if (testWeapon.TypeShootingSystem != TypeShootingSystem.Melee) { return isSuccess = false; }

        if (testWeapon.DataWeapon is DataWeaponMelee)
        {
            _meleeWeapon = testWeapon.DataWeapon as DataWeaponMelee;
            _weapon = testWeapon;
        }
        else
        {
            isSuccess = false;
        }
        return isSuccess;
    }

    private void HitMelee()
    {
        if (Time.time >= _nextTimeToHit)
        {
            OnAttackOne?.Invoke();
            _nextTimeToHit = Time.time + _meleeWeapon.TimeWaitNextHit;

            TimeActiveHit(_cancellationToken.Token).Forget();
        }
    }

    private void ShootDistance()
    {
        Fire();
    }

    private void Fire()
    {
        if (Time.time >= _nextTimeToFire)
        {
            _nextTimeToFire = Time.time + _meleeWeapon.TimeWaitFire;
            Bullet tempBullet = _meleeWeapon.GetBullet();
            ControlBullet(tempBullet);
            tempBullet.Rigidbody.AddForce(_head.forward * _meleeWeapon.SpeedBullet, ForceMode.VelocityChange);
            Debug.Log("OnShoot");
            OnAttackTwo?.Invoke();
        }
    }

    private async UniTaskVoid TimeActiveHit(CancellationToken cancellationToken)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_meleeWeapon.TimeActiveHit), false, PlayerLoopTiming.Update, cancellationToken);
        int countTarget = Physics.OverlapSphereNonAlloc(_weapon.Point.FirePoint.position, _meleeWeapon.RangeHit, _targets, _meleeWeapon.LayerEnemy);
        for (int i = 0; i < countTarget; i++)
        {
            //_targets[i].gameObject.SetActive(false);
            if (_targets[i].TryGetComponent(out IHealtheble target))
            {
                target.TakeDamage(_meleeWeapon.DamageHit);
            }
        }
    }

    private void ControlBullet(Bullet tempBullet)
    {
        Rigidbody tempRigidbody = tempBullet.Rigidbody;
        tempRigidbody.angularVelocity = Vector3.zero;
        tempRigidbody.linearVelocity = Vector3.zero;
        tempRigidbody.position = _weapon.Point.FirePointTwo.position;
    }

    private bool CheckAttack(TestWeapon testWeapon)
    {
        bool isComplite = true;
        if (testWeapon == null)
        {
            isComplite = false;
        }
        if (_isBlock)
        {
            isComplite = false;
        }
        if (!ControlCurrentWeapon(testWeapon))
        {
            isComplite = false;
        }
        return isComplite;
    }
}