using System.Collections;
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
    private Camera _camera;
    private Coroutine _coroutine;
    private bool _isActive = false;
    private bool _isPlaying = false;
    private const int _zoomOn = -1;
    private const int _zoomOFF = 1;

    public void Initialization(Camera camera)
    {
        _camera = camera;
        _isActive = false;
        _isPlaying = false;
    }

    public void AimingOn()
    {
        if (_isActive) return;
        _isActive = true;
        StopMoveAim();
        _isPlaying = true;
        _coroutine = StartCoroutine(MoveAim(_endFieldOfView, _zoomOn));
    }

    public void AimingOff()
    {
        if (!_isActive) return;
        _isActive = false;
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
    /// Плавное изменение FieldOfView камеры
    /// </summary>
    /// <param name="end"></param>
    /// <param name="modifier"></param>
    /// <returns></returns>
    private IEnumerator MoveAim(float end, int modifier)
    {
        while (_isPlaying)
        {
            if (_camera.fieldOfView <= end && modifier < 0)
            {
                _isPlaying = false;
            }
            else if (_camera.fieldOfView >= end && modifier > 0)
            {
                _isPlaying = false;
            }
            _camera.fieldOfView += Time.deltaTime * _speedChooseView * modifier;
            yield return null;
        }
        _coroutine = null;
    }
}