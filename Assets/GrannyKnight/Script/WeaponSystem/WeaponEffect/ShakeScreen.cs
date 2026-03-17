using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

public class ShakeScreen : MonoBehaviour
{
    [Header("ScreenShake")]
    [SerializeField] private CinemachineBasicMultiChannelPerlin _virtualCamera;
    [SerializeField] private float _shakeDuration = 0.3f;
    [SerializeField] private float _shakeAmplitude = 1f;
    [SerializeField] private float _shakeFrequency = 2f;
    private Tween _shakeTween;
    public void ShakeScreenMetod()
    {
        // Останавливаем предыдущую тряску
        _shakeTween?.Kill();

        // Устанавливаем параметры шума
        if (_virtualCamera != null)
        {
            _virtualCamera.AmplitudeGain = _shakeAmplitude;
            _virtualCamera.FrequencyGain = _shakeFrequency;
            
            // Создаем твин для плавного возврата
            _shakeTween = DOVirtual.Float(_shakeAmplitude, 0f, _shakeDuration, value =>
            {
                _virtualCamera.AmplitudeGain = value;
            }).SetEase(Ease.OutQuad);
            _shakeTween.Play();
        }
    }
}
