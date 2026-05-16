using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class BlackoutScreen : IPlayerStrategyHealtheble, IDisposable
{
    private CancellationTokenSource _cancelToken;
    private Image _blackoutImage;
    private int[] _stageHealth;
    private float _timeOverImmunityDamage;
    private int _healthPlayer;
    private bool _isAlife;

    private readonly float _timeToRegen;
    private readonly float _timeImmunity;
    private readonly int _maxHealthPlayer;

    private const float _constantOne = 1f;
    private const float _constantZero = 0f;

    public event Action OnEnd;

    public BlackoutScreen(Image image, int health, float timeImmunity, float timeToRegen, int[] stageHealth)
    {
        _timeImmunity = timeImmunity;
        _timeToRegen = timeToRegen;
        _blackoutImage = image;
        _maxHealthPlayer = health;
        _healthPlayer = health;
        _isAlife = true;
        _stageHealth = stageHealth;
    }

    public void Dispose()
    {
        _cancelToken?.Cancel();
        _cancelToken?.Dispose();
        _cancelToken = null;
    }

    public void TakeDamage(float damage)
    {
        if (!CheckImmunityDamage()) { return; }
        _healthPlayer -= 1;
        Debug.Log($"_healthPlayer = {_healthPlayer}");
        CheckHealth();
    }

    public void OffUi()
    {
        _blackoutImage.enabled = false;
        _isAlife = false;
    }

    private void CheckHealth()
    {
        if (_healthPlayer < 0)
        {
            End();
        }
        else
        {
            _timeOverImmunityDamage = Time.time + _timeImmunity;
            ChangeBlackout();
            if (_cancelToken != null)
            {
                _cancelToken.Cancel();
            }
            _cancelToken = new CancellationTokenSource();
            RegenHealth(_cancelToken).Forget();
        }
    }

    private void ChangeBlackout()
    {
        ChangeBlackoutForm();
    }

    private void ChangeBlackoutForm()
    {
        Color color = _blackoutImage.color;
        if (_healthPlayer == _maxHealthPlayer)
        {
            color.a = _constantZero;
            Debug.Log($"tempAlpha = {color.a}");
            _blackoutImage.color = color;
            return;
        }
        if (_healthPlayer > _stageHealth.Length) { return; }

        Tween _tween = DOTween.To(() => _blackoutImage.pixelsPerUnitMultiplier, x => _blackoutImage.pixelsPerUnitMultiplier = x, _stageHealth[_healthPlayer], 0.3f);
        _tween.Play();

        //_blackoutImage.pixelsPerUnitMultiplier = _stageHealth[_healthPlayer];

        color.a = _constantOne;
        Debug.Log($"tempAlpha = {color.a}");
        _blackoutImage.color = color;
    }

    private bool CheckImmunityDamage()
    {
        bool isContinueTakeDamage = true;
        if (!_isAlife)
        {
            isContinueTakeDamage = false;
        }
        if (Time.time < _timeOverImmunityDamage)
        {
            isContinueTakeDamage = false;
        }
        return isContinueTakeDamage;
    }

    private async UniTaskVoid RegenHealth(CancellationTokenSource cancellationToken)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_timeToRegen), cancellationToken: cancellationToken.Token);
        _healthPlayer = _maxHealthPlayer;
        Debug.Log($"_healthPlayer = {_healthPlayer}");
        ChangeBlackout();
    }

    private void End()
    {
        _blackoutImage.enabled = false;
        _isAlife = false;
        Debug.Log($"End");
        OnEnd?.Invoke();
    }
}