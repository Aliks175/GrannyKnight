using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;
using DG.Tweening;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _timeToRegen;
    [SerializeField] private float _timeToInv;
    [Range(0, 1)][SerializeField] private float[] _vingetteToGo;
    [SerializeField] private VolumeProfile _volume;
    private Vignette _vignette;
    private int _currentHealth;
    private float _vingetteStart;
    private CancellationTokenSource _cancelToken;
    private Tween _tween;
    private bool _isInvincible = false;

    void Start()
    {
        _volume.TryGet(out _vignette);
        _vingetteStart = _vignette.intensity.value;
        _currentHealth = _maxHealth;;
    }

    public int TakeDamage()
    {
        if (_isInvincible)
        {
            return _currentHealth;
        }
        _currentHealth--;
        if (_currentHealth <= 0)
        {
            Die();
            return _currentHealth;
        }
        _vignette.rounded.value = true;
        _tween = DOTween.To(() => _vignette.intensity.value, x => _vignette.intensity.value = x, _vingetteToGo[_currentHealth], 0.3f);
        _tween.Play();
        if (_cancelToken != null)
        {
            _cancelToken.Cancel();
        }
        _cancelToken = new CancellationTokenSource();
        RegenHealth(_cancelToken).Forget();
        Inv().Forget();
        return _currentHealth;
    }

    private async UniTaskVoid RegenHealth(CancellationTokenSource cancellationToken)
    {
        await UniTask.Delay((int)_timeToRegen * 1000 , cancellationToken: cancellationToken.Token);
        _currentHealth = _maxHealth;
        _tween = DOTween.To(() => _vignette.intensity.value, x => _vignette.intensity.value = x, _vingetteStart, 0.3f);
        _tween.Play();
    }

    private async UniTaskVoid Inv()
    {
        _isInvincible = true;
        await UniTask.Delay((int)_timeToInv * 1000);
        _isInvincible = false;
    }

    public void Die()
    {
        _vignette.intensity.value = _vingetteStart;
        _vignette.rounded.value = false;
    }
}
