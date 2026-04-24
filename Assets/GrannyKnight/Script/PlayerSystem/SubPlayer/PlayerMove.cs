
using System;
using UnityEngine;
using Zenject;

public class PlayerMove : ITickable
{
    private Collider[] _colliders;
    private CharacterController _controller;
    private Transform _body;
    private Transform _groundPoint;
    private Vector3 _playerVelocity;
    private Vector3 _velocityGround;
    private Vector3 _moveDirection;
    private Vector3 _final;
    private LayerMask _groundLayer;
    private float _coefficientSpeedForAim;
    private float _coefficientSpeedForAir;
    private float _coefficientKidSpeedMove;
    private float _jumpHeight;
    private float _groundPointRadius;
    private float _gravity;
    private float _speed;
    private bool _isGrounded;

    private readonly float _normalSpeed;
    private readonly float _normalCoefficientSpeedForAir;

    public event Action<bool> OnGrounded;
    public event Action<float> OnMove;

    public PlayerMove(SettingsPlayer settingsPlayer, CharacterController characterController, Transform groundPoint)
    {
        _normalSpeed = settingsPlayer.MovementSpeed;
        _normalCoefficientSpeedForAir = settingsPlayer.CoefficientSpeedWalkForAir;
        _jumpHeight = settingsPlayer.JumpHeight;
        _groundPoint = groundPoint;
        _groundLayer = settingsPlayer.GroundLayer;
        _groundPointRadius = settingsPlayer.GroundPointRadius;
        _gravity = settingsPlayer.Gravity;
        _controller = characterController;
        _velocityGround = new Vector3(0, -2, 0);
        _speed = _normalSpeed;
        _body = characterController.transform;
        _colliders = new Collider[10];
        _coefficientSpeedForAim = 1;
        _coefficientSpeedForAir = 1;
        _coefficientKidSpeedMove = 1;
    }

    public void Tick()
    {
        OnUpdate();
    }

    public void ProcessMove(Vector2 pos)
    {
        _moveDirection.x = pos.x;
        _moveDirection.z = pos.y;
        _moveDirection = _body.TransformDirection(_moveDirection);
        _playerVelocity.y += _gravity * Time.deltaTime;
        _speed = _normalSpeed * _coefficientSpeedForAim * _coefficientSpeedForAir * _coefficientKidSpeedMove;
        if ((_isGrounded && _playerVelocity.y < 0))
        {
            _playerVelocity = _velocityGround;
        }
        _final = _moveDirection * _speed + _playerVelocity;
        _controller.Move(_final * Time.deltaTime);
    }

    /// <summary>
    /// Изменяет coefficient скорости когда игрок прицеливается 
    /// </summary>
    /// <param name="coefficientSpeed"></param>
    public void ChangeCoefficientSpeedForAim(float coefficientSpeed)
    {
        ChangeCoefficientSpeed(ref _coefficientSpeedForAim, coefficientSpeed);
    }

    /// <summary>
    /// Изменяет coefficient скорости когда игрок находится в воздухе
    /// </summary>
    /// <param name="coefficientSpeed"></param>
    public void ChangeCoefficientSpeedForAir(float coefficientSpeed)
    {
        ChangeCoefficientSpeed(ref _coefficientSpeedForAir, coefficientSpeed);
    }

    public void ChangeCoefficientKidSpeedMove(float coefficientSpeed)
    {
        ChangeCoefficientSpeed(ref _coefficientKidSpeedMove, coefficientSpeed);
    }

    public void Jump()
    {
        if (_isGrounded)
        {
            _playerVelocity.y = Mathf.Sqrt(_jumpHeight * -3.0f * _gravity);
            _playerVelocity.x = _moveDirection.x * _speed;
            _playerVelocity.z = _moveDirection.z * _speed;
            //SoundSystem.instance.PlayJump();
        }
    }

    private void ChangeCoefficientSpeed(ref float coefficient, float tempCoefficientSpeed)
    {
        tempCoefficientSpeed = Mathf.Abs(tempCoefficientSpeed);
        coefficient = tempCoefficientSpeed > 1 ? 1 : tempCoefficientSpeed;
    }

    private void OnUpdate()
    {
        _isGrounded = Physics.OverlapSphereNonAlloc(_groundPoint.position, _groundPointRadius, _colliders, _groundLayer) > 0;
        float coefficientSpeed = _isGrounded ? 1 : _normalCoefficientSpeedForAir;
        ChangeCoefficientSpeedForAir(coefficientSpeed);
        OnMove?.Invoke(Vector3.SqrMagnitude(_final));
        OnGrounded?.Invoke(_isGrounded);
    }
}
