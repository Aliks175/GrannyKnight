using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMover : MonoBehaviour
{
    [Header("Player-Parameters")]
    [SerializeField] private float _speedMove = 5.0f;
    [SerializeField] private float jumpHeight = 3.0f;
    [Header("Physic")]
    [SerializeField] private float _gravity = -9.8f;
    [SerializeField] private bool _isGrounded;
    private float _speed;
    private float _speedCoefficient;
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
        _speed = _speedMove;
        _speedCoefficient = 1f;
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
            _speed = _speedMove / 4;
        }
        else if (_isGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -2;
            _speed = _speedMove;
        }
        _final = _moveDirection * _speed * _speedCoefficient + _playerVelocity;
        _controller.Move(_final * Time.deltaTime);
    }

    public void ChangeSpeedCoefficient(float speedCoefficient)
    {
        _speedCoefficient = Mathf.Abs(speedCoefficient);
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