using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMover : MonoBehaviour
{
    [Header("Player-Parameters")]
    [SerializeField] private float _speedWalk = 5.0f;
    [SerializeField] private float _speedRun = 8.0f;
    [SerializeField] private float _coefficientSpeedForAim = 0.5f;
    [SerializeField] private float jumpHeight = 3.0f;
    [Header("Physic")]
    [SerializeField] private float _gravity = -9.8f;
    [SerializeField] private bool _isGrounded;
    private bool _isAim;
    private bool _isRun;
    private float _speed;
    private CharacterController _controller;
    private Vector3 _playerVelocity;
    private Vector3 _moveDirection;
    private Vector3 _final;

    private void OnDisable()
    {
        MainSystem.OnUpdate -= OnUpdate;
    }

    public void Initialization()
    {
        _speed = _speedWalk;
        _controller = GetComponent<CharacterController>();
        MainSystem.OnUpdate += OnUpdate;
    }

    public void ProcessMove(Vector2 pos)
    {
        _moveDirection = Vector3.zero;
        _moveDirection.x = pos.x;
        _moveDirection.z = pos.y;
        _moveDirection = transform.TransformDirection(_moveDirection);
        _playerVelocity.y += _gravity * Time.deltaTime;
        if (!_isGrounded)
        {
            _speed = _speedWalk / 4;
        }
        else if (_isGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -2;

            _speed = _isRun ? _speedRun : _speedWalk;
        }
        if (_isAim) _speed *= _coefficientSpeedForAim;
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

    public void ActiveRunSpeed(InputAction.CallbackContext value)
    {
        if (value.phase == InputActionPhase.Started)
        {
            _isRun = true;
        }
        if (value.phase == InputActionPhase.Canceled)
        {
            _isRun = false;
        }
    }

    public void Jump()
    {
        if (_isGrounded)
        {
            _playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * _gravity);
        }
    }

    private void OnUpdate()
    {
        _isGrounded = _controller.isGrounded;
    }
}