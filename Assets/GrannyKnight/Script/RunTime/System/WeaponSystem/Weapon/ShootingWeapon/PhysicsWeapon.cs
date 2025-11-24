using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PhysicsWeapon : MonoBehaviour, IFireble
{
    [Header("Ballistics Settings")]
    [SerializeField] private Transform _startPoint;
    [SerializeField] private float _minForce = 1f;
    [SerializeField] private float _distanceMultiplier = 10f;

    [Header("Trajectory Visualization")]
    [SerializeField] private LineRenderer _trajectoryLine;
    [SerializeField] private int _trajectoryPoints = 30;
    private GameObject _arrowPrefab;
    private Weapon _weapon;
    private Coroutine _coroutine;
    private WeaponEffectAbstract _weaponEffect;
    private Transform _head;
    private Vector3 _launchDirection;
    private float _maxProjectileForce;
    private float _forceMultiplier;
    private float _koefCharge;
    private float _currentForce;
    private float _launchAngle;
    private float _flightDistance;
    private bool _isFire;

    public event Action<TypeShoot> OnFire;
    public event Action OnEndFire;
    public event Action OnStartFire;

    public void Initialization(Transform head)
    {
        if (head == null)
        {
            Debug.LogError("Head cannot be null");
            return;
        }

        _head = head;
        _isFire = false;
    }

    public void SetWeapon(Weapon weapon)
    {
        if (weapon == null)
        {
            Debug.LogWarning("Weapon is null");
            return;
        }
        _weapon = weapon;
        _maxProjectileForce = weapon.MaxForce;
        _forceMultiplier = weapon.ForceMultiplier;
        _koefCharge = weapon.KoefCharge;
        _arrowPrefab = weapon.ArrowPrefab;
    }

    public void SetWeaponEffect(WeaponEffectAbstract weaponEffect)
    {
        if (weaponEffect == null) return;
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

        Vector3[] trajectoryPath = CalculateParabolicPath(_flightDistance);
        GameObject arrow = Instantiate(_arrowPrefab, trajectoryPath[0], _startPoint.rotation);
        ArrowWeapon arrowComponent = arrow.GetComponent<ArrowWeapon>();

        if (arrowComponent != null)
        {
            arrowComponent.StartFlight(_currentForce, _launchAngle, trajectoryPath, _weapon.Damage);
        }
        else
        {
            Debug.LogWarning("ArrowWeapon component not found on arrow prefab");
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
        float clampedY = Mathf.Clamp(_head.forward.y, -1f, 1f);
        float angle = Mathf.Asin(clampedY) * Mathf.Rad2Deg;
        return Mathf.Clamp(angle, -89f, 89f);
    }
    private Vector3[] CalculateParabolicPath(float flightDistance)
    {
        Vector3[] path = new Vector3[_trajectoryPoints];
        Vector3 endPoint = CalculateEndPoint(flightDistance);

        for (int i = 0; i < _trajectoryPoints; i++)
        {
            float t = i / (float)(_trajectoryPoints - 1);
            path[i] = CalculatePointOnTrajectory(t, endPoint);
        }

        return path;
    }

    private Vector3 CalculateEndPoint(float flightDistance)
    {
        Vector3 horizontalDirection = new Vector3(_launchDirection.x, 0, _launchDirection.z).normalized;
        Vector3 endPoint = _startPoint.position + horizontalDirection * flightDistance;
        
        // При отрицательных углах конечная точка ниже
        if (_launchAngle < 0)
        {
            float verticalDrop = Mathf.Tan(Mathf.Abs(_launchAngle) * Mathf.Deg2Rad) * flightDistance;
            endPoint.y -= verticalDrop;
        }
        
        return endPoint;
    }
    private float CalculateFlightDistance()
    {
        if (_launchAngle < 3f)
        {
            // При горизонтальном полете используем простую формулу
            return _currentForce * _distanceMultiplier * 0.5f;
        }
        
        float angleRad = _launchAngle * Mathf.Deg2Rad;
        float distance = (_currentForce * _currentForce * Mathf.Sin(2 * angleRad)) / Physics.gravity.magnitude;
        return Mathf.Abs(distance) * _distanceMultiplier;
    }
    private Vector3 CalculatePointOnTrajectory(float t, Vector3 endPoint)
    {
        Vector3 basePos = Vector3.Lerp(_startPoint.position, endPoint, t);
        
        // При углах меньше 3 градусов - прямая линия
        if (_launchAngle < 3f)
        {
            return basePos;
        }
        
        float maxHeight = Mathf.Abs(CalculateMaxHeight());
        float parabolaHeight = maxHeight * (1f - 4f * (t - 0.5f) * (t - 0.5f));
        
        // При отрицательных углах парабола идет вниз
        if (_launchAngle < 0) parabolaHeight = -parabolaHeight;

        return new Vector3(basePos.x, basePos.y + parabolaHeight, basePos.z);
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

        Vector3 endPoint = CalculateEndPoint(_flightDistance);

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
        Vector3 endPoint = CalculateEndPoint(_flightDistance);
        Gizmos.DrawLine(_startPoint.position, endPoint);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_startPoint.position, _currentForce * 0.1f);
    }

}
