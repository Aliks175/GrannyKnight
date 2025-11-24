using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMover : MonoBehaviour
{
    [Header("Player-Parameters")]
    [SerializeField] private float _speedWalk = 8.0f;
    [SerializeField, Range(0.1f, 1f)] private float _coefficientSpeedForAim = 0.5f;
    [SerializeField, Range(0.1f, 1f)] private float _coefficientSpeedWalkForAir = 0.5f;
    [SerializeField] private float jumpHeight = 3.0f;
    [Header("Physic")]
    [SerializeField] private Transform _groundPoint;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundPointRadius = 1f;
    [SerializeField] private float _gravity = -9.8f;
    [SerializeField] private bool _isGrounded;
    private PlayerControlAnimation _playerControlAnimation;
    private CharacterController _controller;
    private Vector3 _playerVelocity;
    private Vector3 _velocityGround;
    private Vector3 _moveDirection;
    private Vector3 _final;
    private float _speed;
    private bool _isAim;

    private void OnDisable()
    {
        MainSystem.OnUpdate -= OnUpdate;
    }

    public void Initialization(PlayerControlAnimation playerControlAnimation)
    {
        _playerControlAnimation = playerControlAnimation;
        _speed = _speedWalk;
        _velocityGround = new Vector3(0, -2, 0);
        _controller = GetComponent<CharacterController>();
        MainSystem.OnUpdate += OnUpdate;
    }

    public void ProcessMove(Vector2 pos)
    {
        _moveDirection.x = pos.x;
        _moveDirection.z = pos.y;
        _moveDirection = transform.TransformDirection(_moveDirection);
        _playerVelocity.y += _gravity * Time.deltaTime;
        if (!_isGrounded)
        {
            _speed = _speedWalk * _coefficientSpeedWalkForAir;
        }
        else if (_isGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity = _velocityGround;
            _speed = _speedWalk;
        }
        if (_isAim) _speed = _speed * _coefficientSpeedForAim;
        _final = _moveDirection * _speed + _playerVelocity;
        _controller.Move(_final * Time.deltaTime);
    }

    public void ActiveAimSpeed(InputAction.CallbackContext value)
    {
        if (value.phase == InputActionPhase.Started)
        {
            _isAim = true;
        }
        if (value.phase == InputActionPhase.Canceled)
        {
            _isAim = false;
        }
    }

    public void Jump()
    {
        if (_isGrounded)
        {
            _playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * _gravity);
            _playerVelocity.x = _moveDirection.x * _speed;
            _playerVelocity.z = _moveDirection.z * _speed;
        }
    }

    private void OnUpdate()
    {
        _isGrounded = Physics.OverlapSphere(_groundPoint.position, _groundPointRadius, _groundLayer).Length > 0;
        _playerControlAnimation.SetCheckGround(_isGrounded);
        _playerControlAnimation.SetSpeed(Vector3.SqrMagnitude(_final));
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawSphere(_groundPoint.position, _groundPointRadius);
    //}
}