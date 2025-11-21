using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PhysicsWeapon : MonoBehaviour, IFireble
{
    [Header("Ballistics Settings")]
    [SerializeField] private Transform _startPoint;
    [SerializeField] private float _minForce = 1f;

    [Header("Trajectory Visualization")]
    [SerializeField] private LineRenderer _trajectoryLine;
    [SerializeField] private int _trajectoryPoints = 30;
    private GameObject _arrowPrefab;
    private Weapon _weapon;
    private Coroutine _coroutine;
    private WeaponEffectAbstract _weaponEffect;
    private Transform _head;
    private Vector3 _launchDirection;
    private float _maxProjectileForce, _forceMultiplier, _koefCharge, _currentForce, _launchAngle, _flightDistance;
    private bool _isFire;

    public event Action<TypeShoot> OnFire;
    public event Action OnEndFire;


    public void Initialization(Transform head)
    {
        if (head == null)
        {
            Debug.LogError("Camera cannot be null");
            return;
        }

        _head = head;
        _isFire = false;
    }

    public void SetWeapon(Weapon weapon)
    {
        if (weapon == null) return;
        _weapon = weapon;
        _maxProjectileForce = weapon.MaxForce;
        _forceMultiplier = weapon.ForceMultiplier;
        _koefCharge = weapon.KoefCharge;
        _arrowPrefab = weapon.ArrowPrefab;
    }

    public void SetWeaponEffect(WeaponEffectAbstract weaponEffect)
    {
        _weaponEffect = weaponEffect;
        _weaponEffect.Initialization(this, null);
    }

    public void Shoot(InputAction.CallbackContext value)
    {
        if (_weapon == null || _head == null || _startPoint == null || _arrowPrefab == null) return;

        if (value.phase == InputActionPhase.Started)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _isFire = true;
            _currentForce = 0f;
            _coroutine = StartCoroutine(ChargeShot());
            OnFire?.Invoke(new TypeShoot());
        }
        else if (value.phase == InputActionPhase.Canceled)
        {
            OnEndFire?.Invoke();
            ShootWithCurrentForce();
        }
    }

    public void ResetShot()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

        _currentForce = 0f;
        _isFire = false;

        if (_trajectoryLine != null)
            _trajectoryLine.enabled = false;
    }
    private IEnumerator ChargeShot()
    {
        while (_isFire)
        {
            ChargeShotTime();
            yield return null;
        }
    }
    private void ShootWithCurrentForce()
    {
        if (_currentForce <= _minForce)
        {
            ResetShot();
            return;
        }

        GameObject arrow = Instantiate(_arrowPrefab, _startPoint.position, _startPoint.rotation);
        ArrowWeapon arrowComponent = arrow.GetComponent<ArrowWeapon>();

        if (arrowComponent != null)
        {
            arrowComponent.StartFlight(_currentForce, _launchAngle, CalculateParabolicPath(_flightDistance), _weapon.Damage);
        }

        ResetShot();
    }

    public void ChargeShotTime()
    {
        _currentForce = Mathf.Clamp(_currentForce + Time.deltaTime * _koefCharge, 0f, _maxProjectileForce);
        _launchDirection = _head.forward;
        _launchAngle = CalculateLaunchAngle();
        _flightDistance = CalculateFlightDistance();

        UpdateTrajectoryVisualization();
    }
    private float CalculateLaunchAngle()
    {
        float angle = Mathf.Asin(_head.forward.y) * Mathf.Rad2Deg;

        // Если угол слишком мал, используем минимальный угол для стрельбы
        if (Mathf.Abs(angle) < 5f)
            angle = 15f; // Минимальный угол возвышения

        Debug.Log($"Camera forward: {_head.forward}, Angle: {angle}");
        return Mathf.Clamp(angle, 5f, 89f);
    }
    private Vector3[] CalculateParabolicPath(float flightDistance)
    {
        Vector3[] path = new Vector3[_trajectoryPoints];
        Vector3 endPoint = _startPoint.position + _launchDirection * _flightDistance;

        for (int i = 0; i < _trajectoryPoints; i++)
        {
            float t = i / (float)(_trajectoryPoints - 1);
            path[i] = CalculatePointOnTrajectory(t, endPoint);
        }

        return path;
    }
    private float CalculateFlightDistance()
    {
        float angleRad = _launchAngle * Mathf.Deg2Rad;
        float distance = (_currentForce * _currentForce * Mathf.Sin(2 * angleRad)) / Physics.gravity.magnitude;
        distance *= 10f; // Увеличиваем дальность в 10 раз

        Debug.Log($"Force: {_currentForce}, Angle: {_launchAngle}, Distance: {distance}");
        return distance;
    }
    private Vector3 CalculatePointOnTrajectory(float t, Vector3 endPoint)
    {
        Vector3 horizontalPos = Vector3.Lerp(_startPoint.position, endPoint, t);
        float maxHeight = CalculateMaxHeight();
        float verticalOffset = maxHeight * (1f - 4f * (t - 0.5f) * (t - 0.5f));

        return new Vector3(horizontalPos.x, _startPoint.position.y + verticalOffset, horizontalPos.z);
    }
    private float CalculateMaxHeight()
    {
        // Максимальная высота параболы
        float angleRad = _launchAngle * Mathf.Deg2Rad;
        float height = (_currentForce * _currentForce * Mathf.Sin(angleRad) * Mathf.Sin(angleRad))
                      / (2 * Physics.gravity.magnitude);
        return height * _forceMultiplier;
    }
    private void UpdateTrajectoryVisualization()
    {
        if (_trajectoryLine == null || _currentForce <= _minForce)
        {
            if (_trajectoryLine != null)
                _trajectoryLine.enabled = false;
            return;
        }

        _trajectoryLine.enabled = true;
        _trajectoryLine.positionCount = _trajectoryPoints;

        float flightDistance = CalculateFlightDistance();
        Vector3 endPoint = _startPoint.transform.position + _launchDirection * flightDistance;

        for (int i = 0; i < _trajectoryPoints; i++)
        {
            float t = i / (float)(_trajectoryPoints - 1);
            Vector3 point = CalculatePointOnTrajectory(t, endPoint);
            _trajectoryLine.SetPosition(i, point);
        }
    }
    void OnDrawGizmos()
    {
        if (_startPoint == null || _currentForce <= 0.1f) return;

        Gizmos.color = Color.red;
        float distance = CalculateFlightDistance();
        Vector3 endPoint = _startPoint.position + _launchDirection * distance;
        Gizmos.DrawLine(_startPoint.position, endPoint);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_startPoint.position, _currentForce * 0.1f);
    }

}
