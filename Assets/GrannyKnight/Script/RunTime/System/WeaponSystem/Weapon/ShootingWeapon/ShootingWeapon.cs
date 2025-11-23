using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingWeapon : MonoBehaviour, IFireble
{
    private WeaponEffectAbstract _weaponEffect;
    private ControlViewMark _controlViewMark;
    private Transform _head;
    private Coroutine _coroutine;
    private Coroutine _waitAnimationFire;
    private bool _isFire = false;
    private float _nextTimeToFire = 0f;
    public event Action OnStartFire;
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
        _waitAnimationFire = StartCoroutine(SingleFire(weapon));
    }

    private IEnumerator AutoFire(Weapon weapon)
    {
        while (_isFire)
        {
            yield return null;
            Fire(true, weapon);
        }
    }

    private IEnumerator SingleFire(Weapon weapon)
    {
        OnStartFire?.Invoke();
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
                Debug.Log("Выстрел");
                if (hit.collider.TryGetComponent(out IHealtheble target))
                {
                    Debug.Log($"Pos - {hit.point}");

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
    public event Action OnStartFire;
}

public struct TypeShoot
{
    public bool IsShootAutoFire;
    public RaycastHit raycastHit;
}