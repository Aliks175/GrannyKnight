using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingWeapon : MonoBehaviour, IFireble
{
    private WeaponEffectAbstract _weaponEffect;
    private Transform _head;
    private Coroutine _coroutine;
    private Coroutine _waitAnimationFire;
    private float _nextTimeToFire = 0f;
    private bool _isAutoFire = false;
    private bool _isSingleFire = false;
    public event Action OnPreFire;
    public event Action OnFirePhysics;
    public event Action<RaycastHit> OnFireRaycast;
    public event Action OnEndFire;

    public void Initialization(Transform head)
    {
        _head = head;
        _coroutine = null;
        _nextTimeToFire = 0f;
        _isAutoFire = false;
        _isSingleFire = false;
    }


    public void SetWeaponEffect(WeaponEffectAbstract weaponEffect)
    {
        _weaponEffect = weaponEffect;
        _weaponEffect.Initialization(this);
    }

    public void Shoot(InputAction.CallbackContext value, Weapon weapon)
    {
        if (weapon == null) return;

        if (value.phase == InputActionPhase.Started)
        {
            if (weapon.IsAutoFire)
            {
                ShootAutoFire(weapon);
            }
            else
            {
                ShootSingleFire(weapon);
            }
        }
        if (value.phase == InputActionPhase.Canceled)
        {
            StopFire();
        }
    }

    public void StopFire()
    {
        OnEndFire?.Invoke();
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        //if (_waitAnimationFire != null)
        //{
        //    StopCoroutine(_waitAnimationFire);
        //}
        _isAutoFire = false;
        //_isSingleFire = false;
    }

    private void ShootAutoFire(Weapon weapon)
    {
        _isAutoFire = true;
        _coroutine = StartCoroutine(AutoFire(weapon));
    }

    private void ShootSingleFire(Weapon weapon)
    {
        //if (_isSingleFire) return;
        OnPreFire?.Invoke();
        //_isSingleFire = true;
        _waitAnimationFire = StartCoroutine(SingleFire(weapon));
    }

    private IEnumerator AutoFire(Weapon weapon)
    {
        OnPreFire?.Invoke();
        while (_isAutoFire)
        {
            yield return null;
            Fire(true, weapon);
        }
    }

    private IEnumerator SingleFire(Weapon weapon)
    {
        yield return new WaitForSeconds(weapon.TimeWaitFire);
        Fire(false, weapon);
    }

    private void Fire(bool _isShootAutoFire, Weapon weapon)
    {
        if (Time.time >= _nextTimeToFire)
        {
            _nextTimeToFire = Time.time + weapon.TimeWaitFire;

            if (Physics.Raycast(_head.position, _head.forward, out RaycastHit hit, weapon.Range))
            {
                if (hit.collider.TryGetComponent(out IHealtheble target))
                {
                    target.TakeDamage(weapon.Damage);
                }
            }
            OnFireRaycast?.Invoke(hit);
        }
    }
}

public interface IFireble
{
    public event Action OnPreFire;
    public event Action<RaycastHit> OnFireRaycast;
    public event Action OnFirePhysics;
    public event Action OnEndFire;
}

public struct TypeShoot
{
    public bool IsShootAutoFire;
    public RaycastHit raycastHit;
}