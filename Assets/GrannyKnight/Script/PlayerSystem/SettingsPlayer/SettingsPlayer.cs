using UnityEngine;


[CreateAssetMenu(fileName = "SettingsPlayer", menuName = "Create/Settings/SettingsPlayer")]
public class SettingsPlayer : ScriptableObject
{
    #region PublicField
    public LayerMask GroundLayer => _groundLayer;
    public LayerMask LayerInteracteble => _layerInteracteble;
    public float MovementSpeed => _movementSpeed;
    public float CoefficientSpeedForAim => _coefficientSpeedForAim;
    public float CoefficientSpeedWalkForAir => _coefficientSpeedWalkForAir;
    public float JumpHeight => _jumpHeight;
    public float GroundPointRadius => _groundPointRadius;
    public float Gravity => _gravity;
    public float SensitivityY => _sensitivityY;
    public float SensitivityX => _sensitivityX;
    public float CoefficientSensitivityAim => _coefficientSensitivityAim;
    public float StartFieldOfView => _startFieldOfView;
    public float EndFieldOfView => _endFieldOfView;
    public float SpeedChooseView => _speedChooseView;
    public float DistanceInteracteble => _distanceInteracteble;
    public float CoefficientSmoothSpeed => _coefficientSmoothSpeed;

    #endregion

    [Header("Settings")]
    [Header("PlayerMove")]
    [SerializeField] private float _movementSpeed = 8f;
    [SerializeField, Range(0.1f, 1f)] private float _coefficientSpeedForAim = 0.5f;
    [SerializeField, Range(0.1f, 1f)] private float _coefficientSpeedWalkForAir = 0.5f;
    [SerializeField] private float _jumpHeight = 3.0f;

    [Header("Physic")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundPointRadius = 1f;
    [SerializeField] private float _gravity = -9.8f;

    [Header("PlayerLook")]
    [SerializeField] private float _sensitivityY = 30f;
    [SerializeField] private float _sensitivityX = 30f;
    [Range(0, 5)][SerializeField] private float _sensitivity;

    [Header("PlayerAim")]
    [SerializeField, Range(0, 1)] private float _coefficientSensitivityAim = 0.5f;
    [SerializeField] private float _startFieldOfView = 60f;
    [SerializeField] private float _endFieldOfView = 30f;
    [SerializeField, Min(0.1f)] private float _speedChooseView = 1;

    [Header("PlayerInteracteble")]
    [SerializeField] private LayerMask _layerInteracteble;
    [SerializeField] private float _distanceInteracteble;

    [Header("PlayerControlAnimation")]
    [SerializeField] private float _coefficientSmoothSpeed;
}