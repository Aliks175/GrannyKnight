using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingWeapon : MonoBehaviour , IFireble
{
    private WeaponEffectAbstract _weaponEffect;
    private ControlViewMark _controlViewMark;
    private Transform _head;
    private Coroutine _coroutine;
    private bool _isFire = false;
    private float _nextTimeToFire = 0f;
    public event Action<TypeShoot> OnFire;
    public event Action OnEndFire;

    public void Initialization(Transform head)
    {
        _head = head;
        _coroutine = null;
        _nextTimeToFire = 0f;
        _isFire = false;
    }


    public void SetWeaponEffect(WeaponEffectAbstract weaponEffect)
    {
        _weaponEffect = weaponEffect;
        _weaponEffect.Initialization(this, _controlViewMark);
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
            _isFire = false;
            StopCoroutine(_coroutine);
        }
    }

    private void ShootAutoFire(Weapon weapon)
    {
        _isFire = true;
        _coroutine = StartCoroutine(AutoFire(weapon));
    }

    private void ShootSingleFire(Weapon weapon)
    {
        Fire(false,weapon);
    }

    private IEnumerator AutoFire(Weapon weapon)
    {
        while (_isFire)
        {
            yield return null;
            Fire(true,weapon);
        }
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
            OnFire?.Invoke(new TypeShoot { IsShootAutoFire = _isShootAutoFire, raycastHit = hit });
        }
    }
}

public interface IFireble
{
    public event Action<TypeShoot> OnFire;
    public event Action OnEndFire;
}

public struct TypeShoot
{
    public bool IsShootAutoFire;
    public RaycastHit raycastHit;
}