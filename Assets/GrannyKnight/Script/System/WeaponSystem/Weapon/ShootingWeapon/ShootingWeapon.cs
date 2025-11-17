using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingWeapon : MonoBehaviour
{
    private WeaponEffect _weaponEffect;
    private ControlViewMark _controlViewMark;
    private Weapon _weapon;
    private Camera _camera;
    private Coroutine _coroutine;
    private bool _isFire = false;
    private float _nextTimeToFire = 0f;
    public Action<TypeShoot> OnFire;
    public Action OnEndFire;

    public void Initialization(Camera camera, ControlViewMark controlViewMark)
    {
        _controlViewMark = controlViewMark;
        _camera = camera;
        _coroutine = null;
        _nextTimeToFire = 0f;
        _isFire = false;
    }

    public void SetWeapon(Weapon weapon)
    {
        _weapon = weapon;
    }

    public void SetWeaponEffect(WeaponEffect weaponEffect)
    {
        _weaponEffect = weaponEffect;
        _weaponEffect.Initialization(this, _controlViewMark);
    }

    public void Shoot(InputAction.CallbackContext value)
    {
        if (_weaponEffect == null || _weapon == null) return;

        if (value.phase == InputActionPhase.Started)
        {
            if (_weapon.IsAutoFire)
            {
                ShootAutoFire();
            }
            else
            {
                ShootSingleFire();
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

    private void ShootAutoFire()
    {
        _isFire = true;
        _coroutine = StartCoroutine(AutoFire());
    }

    private void ShootSingleFire()
    {
        Fire(false);
    }

    private IEnumerator AutoFire()
    {
        while (_isFire)
        {
            yield return null;
            Fire(true);
        }
    }

    private void Fire(bool _isShootAutoFire)
    {
        if (Time.time >= _nextTimeToFire)
        {
            _nextTimeToFire = Time.time + _weapon.TimeWaitFire;

            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hit, _weapon.Range))
            {
                if (hit.collider.TryGetComponent(out IHealtheble target))
                {
                    target.TakeDamage(_weapon.Damage);
                }
            }
            OnFire?.Invoke(new TypeShoot { IsShootAutoFire = _isShootAutoFire, raycastHit = hit });
        }
    }
}

public struct TypeShoot
{
    public bool IsShootAutoFire;
    public RaycastHit raycastHit;
}