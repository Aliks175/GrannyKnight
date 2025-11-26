using System.Collections;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [Header("SpeedMove")]
    public float SpeedCoefficient => _speedCoefficient;
    [SerializeField, Range(0, 1)] private float _speedCoefficient = 0.5f;
    [Header("FieldOfView")]
    [SerializeField] private float _startFieldOfView = 60f;
    [SerializeField] private float _endFieldOfView = 30f;
    [SerializeField, Min(0.1f)] private float _speedChooseView = 1;
    private CinemachineCamera _cinemachineCamera;
    private Coroutine _coroutine;
    private PlayerLook _playerLook;
    private Tween _tween;
    private bool _isActive = false;
    private bool _isPlaying = false;
    private const int _zoomOn = -1;
    private const int _zoomOFF = 1;

    public void Initialization(CinemachineCamera cinemachineCamera)
    {
        _cinemachineCamera = cinemachineCamera;
        _isActive = false;
        _isPlaying = false;
        _playerLook = GetComponent<PlayerLook>();
    }

    public void AimingOn()
    {
        if (_isActive) return;
        _isActive = true;
        _playerLook.IsAim = true;
        StopMoveAim();
        _isPlaying = true;
        _coroutine = StartCoroutine(MoveAim(_endFieldOfView, _zoomOn));
    }
    public void StartAim( float endFieldOfView, float speedChooseView)
    {
        _playerLook.IsAim = true;
        _tween = DOVirtual.Float(_startFieldOfView, endFieldOfView, speedChooseView, value => _cinemachineCamera.Lens.FieldOfView = value);
        _tween.Play();
    }
    public void StopAim()
    {
        _playerLook.IsAim = false;
        _tween.Kill();
        _cinemachineCamera.Lens.FieldOfView = _startFieldOfView;
    }

    public void AimingOff()
    {
        if (!_isActive) return;
        _isActive = false;
        _playerLook.IsAim = false;
        StopMoveAim();
        _isPlaying = true;
        _coroutine = StartCoroutine(MoveAim(_startFieldOfView, _zoomOFF));
    }

    private void StopMoveAim()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    /// <summary>
    /// ������� ��������� FieldOfView ������
    /// </summary>
    /// <param name="end"></param>
    /// <param name="modifier"></param>
    /// <returns></returns>
    private IEnumerator MoveAim(float end, int modifier)
    {
        while (_isPlaying)
        {
            if (_cinemachineCamera.Lens.FieldOfView <= end && modifier < 0)
            {
                _isPlaying = false;
            }
            else if (_cinemachineCamera.Lens.FieldOfView >= end && modifier > 0)
            {
                _isPlaying = false;
            }
            _cinemachineCamera.Lens.FieldOfView += Time.deltaTime * _speedChooseView * modifier;
            yield return null;
        }
        _coroutine = null;
    }
}