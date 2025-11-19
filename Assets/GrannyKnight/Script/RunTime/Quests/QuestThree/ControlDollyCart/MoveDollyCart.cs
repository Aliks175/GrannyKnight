using Unity.Cinemachine;
using UnityEngine;

public class MoveDollyCart : MonoBehaviour
{
    [SerializeField] private AnimationCurve m_AnimationCurve;
    [SerializeField] private CinemachineSplineCart _cinemachineSplineCart;
    [SerializeField, Range(0, 1)] private float _speed;
    private bool _isPlay;
    private float _position;
    private float _tempPosition;

    private void OnDisable()
    {
        MainSystem.OnUpdate -= OnUpdate;
    }

    public void Initialization()
    {
        MainSystem.OnUpdate += OnUpdate;
    }

    public void OnPlay(bool isPlay = true)
    {
        _isPlay = isPlay;
        if (!_isPlay)
        {
            MainSystem.OnUpdate -= OnUpdate;
            return;
        }
    }

    private void OnUpdate()
    {
        if (!_isPlay) return;
        _tempPosition += Time.deltaTime * _speed;
        _position = m_AnimationCurve.Evaluate(_tempPosition);
        _cinemachineSplineCart.SplinePosition = _position;
    }
}