using UnityEngine;
using Unity.Cinemachine;
using DG.Tweening;

public class ChangeFOV : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _camera;
    [SerializeField] private float _targetFOV;
    [SerializeField] private float _duration;
    private Tween _tween;
    private float _startFOV;
    public void Change()
    {
        _startFOV = _camera.Lens.FieldOfView;
        Debug.Log(_startFOV);
        Debug.Log(_camera.Lens.FieldOfView);
        _tween = DOVirtual.Float(_startFOV, _targetFOV, _duration, value => _camera.Lens.FieldOfView = value);
        _tween.Play();
    }
    public void ResetFOV()
    {
        if (_tween != null)
        {
            _tween.Kill();
        }
        if (_startFOV != 0)   _camera.Lens.FieldOfView = _startFOV;
    }
}
