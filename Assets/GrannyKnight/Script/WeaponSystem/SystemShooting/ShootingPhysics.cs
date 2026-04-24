using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using Zenject;

namespace Refactor
{
    public class ShootingPhysics : IShootingSystemble, IDisposable, IInitializable
    {
        private TestWeapon _weapon;
        private DataWeaponPhysics _physicsWeapon;
        private TrajectoryPredictor _trajectoryPredictor;
        private Transform _head;

        private CancellationTokenSource _cancellationToken;
        private float _nextTimeToFire;
        private int _currentId;
        private bool _isFire;

        public event Action OnCharge;
        public event Action OnShoot;

        public ShootingPhysics(Transform head)
        {
            _head = head;
            _currentId = -1;
        }

        public void Shoot(TestWeapon testWeapon)
        {
            if (testWeapon == null) return;
            if (_isFire) { return; }
            if (!ControlCurrentWeapon(testWeapon)) { return; }

            ChargeFire();
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

        public void StopShoot()
        {
            _isFire = false;
        }

        private void ChargeFire()
        {
            if (Time.time >= _nextTimeToFire)
            {
                _nextTimeToFire = Time.time + _physicsWeapon.TimeWaitFire;
                StartTimer(_cancellationToken.Token).Forget();
                OnCharge?.Invoke();
            }
        }

        private async UniTaskVoid StartTimer(CancellationToken token)
        {
            try
            {
                _isFire = true;
                float tempTime = 0;
                float timeWaitMaxForce = _physicsWeapon.TimeWaitMaxForce;
                while (_isFire)
                {
                    await UniTask.Yield(PlayerLoopTiming.Update);

                    if (tempTime <= timeWaitMaxForce)
                    {
                        tempTime += Time.deltaTime;
                    }
                    ControlTrajectoryPredictor(tempTime);
                    ControlVisibleTrajectory(_isFire);
                    //Debug.Log($"TempTime = {tempTime}");
                }
                Fire(tempTime);
            }
            catch (OperationCanceledException)
            {
                ControlVisibleTrajectory(false);
                Debug.Log("Операция отменена ");
            }
        }

        private void Fire(float tempTime)
        {
            OnShoot?.Invoke();
            float tempForce = ControlForce(tempTime);
            Vector3 direction = ControlAngle(tempTime);
            Bullet tempBullet = _physicsWeapon.GetBullet();
            ControlBullet(tempBullet);
            //Debug.Log($"tempForce = {tempForce}");

            tempBullet.Rigidbody.AddForce(direction * tempForce, ForceMode.VelocityChange);
        }

        private float ControlForce(float tempTime)
        {
            return ControlStat(tempTime, _physicsWeapon.MinForce, _physicsWeapon.MaxForce);
        }

        private Vector3 ControlAngle(float tempTime)
        {
            float _directionY = ControlStat(tempTime, _physicsWeapon.MinAngle, _physicsWeapon.MaxAngle);
            Vector3 tempDirection = Quaternion.AngleAxis(-_directionY, _head.right) * _head.forward;
            return tempDirection;
        }

        private float ControlStat(float tempTime, float min, float max)
        {
            tempTime = tempTime > _physicsWeapon.TimeWaitMaxForce ? _physicsWeapon.TimeWaitMaxForce : tempTime;

            float _coefficient = ControlCoefficientMaxForce(tempTime);
            float tempForce = max * _coefficient;
            tempForce = Mathf.Clamp(tempForce, min, max);
            return tempForce;
        }

        private void ControlBullet(Bullet tempBullet)
        {
            Rigidbody tempRigidbody = tempBullet.Rigidbody;
            tempRigidbody.angularVelocity = Vector3.zero;
            tempRigidbody.linearVelocity = Vector3.zero;
            tempRigidbody.position = _weapon.Point.FirePoint.position;
        }

        private void ControlTrajectoryPredictor(float tempTime)
        {
            if (_trajectoryPredictor == null) { return; }
            float tempForce = ControlForce(tempTime);
            Vector3 direction = ControlAngle(tempTime);
            _trajectoryPredictor.ShowTrajectory(_weapon.Point.FirePoint.position, tempForce * direction);
        }

        private void ControlVisibleTrajectory(bool visible)
        {
            if (_trajectoryPredictor == null) { return; }
            _trajectoryPredictor.ControlVisible(visible);
        }

        private float ControlCoefficientMaxForce(float tempTime)
        {
            float _coefficient;
            if (_physicsWeapon.TimeWaitMaxForce <= 0)
            {
                _coefficient = 1f;
            }
            else
            {
                _coefficient = tempTime / _physicsWeapon.TimeWaitMaxForce;
            }
            return _coefficient;
        }

        private bool ControlCurrentWeapon(TestWeapon testWeapon)
        {
            bool isSuccess = true;
            if (_currentId == testWeapon.ID) { return isSuccess; }
            if (testWeapon.TypeShootingSystem == TypeShootingSystem.Raycast) { return isSuccess = false; }

            if (testWeapon.DataWeapon is DataWeaponPhysics)
            {
                _physicsWeapon = testWeapon.DataWeapon as DataWeaponPhysics;
                _weapon = testWeapon;
                _trajectoryPredictor = testWeapon.Point.TryGetComponent(out TrajectoryPredictor trajectoryPredictor) ? trajectoryPredictor : null;
            }
            else
            {
                isSuccess = false;
            }
            return isSuccess;
        }
    }
}