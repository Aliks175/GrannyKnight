using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using Zenject;

namespace Refactor
{
    public class ShootingRaycast : IShootingSystemble, IDisposable , IInitializable
    {
        private CancellationTokenSource _cancellationToken;
        private DataWeaponRaycast _raycastWeapon;
        private Transform _head;
        private float _nextTimeToFire;
        private int _currentId;
        private bool _isFire;

        public event Action OnPreFire;
        public event Action OnFirePhysics;
        public event Action<RaycastHit> OnFireRaycast;
        public event Action OnEndFire;

        public ShootingRaycast(Transform head)
        {
            _head = head;
            _nextTimeToFire = 0f;
            _currentId = -1;
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

        public void Shoot(TestWeapon testWeapon)
        {
            if (testWeapon == null) return;
            if (!ControlCurrentWeapon(testWeapon)) { return; }

            if (_raycastWeapon.IsAutoFire)
            {
                ShootAutoFire();
            }
            else
            {
                ShootSingleFire();
            }
        }

        public void StopShoot()
        {
            OnEndFire?.Invoke();
            _isFire = false;
        }

        private bool ControlCurrentWeapon(TestWeapon testWeapon)
        {
            bool isSuccess = true;
            if (_currentId == testWeapon.ID) { return isSuccess; }
            if (testWeapon.TypeShootingSystem == TypeShootingSystem.Physics) { return isSuccess = false; }

            if (testWeapon.DataWeapon is DataWeaponRaycast)
            {
                _raycastWeapon = testWeapon.DataWeapon as DataWeaponRaycast;
            }
            else
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        private void ShootAutoFire()
        {
            _isFire = true;
            StartTimer(_cancellationToken.Token).Forget();
        }

        private void ShootSingleFire()
        {
            OnPreFire?.Invoke();
            Fire();
        }

        private async UniTaskVoid StartTimer(CancellationToken token)
        {
            try
            { 
                float time = _raycastWeapon.TimeWaitFire;
                while (_isFire)
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(time));
                    Fire();
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Операция отменена ");
            }
        }

        private void Fire()
        {
            if (Time.time >= _nextTimeToFire)
            {
                _nextTimeToFire = Time.time + _raycastWeapon.TimeWaitFire;

                if (Physics.Raycast(_head.position, _head.forward, out RaycastHit hit, _raycastWeapon.Range))
                {
                    if (hit.collider.TryGetComponent(out IHealtheble target))
                    {
                        target.TakeDamage(_raycastWeapon.Damage);
                    }
                }
                OnFireRaycast?.Invoke(hit);
                Debug.Log("Fire ");
            }
        }
    }
}