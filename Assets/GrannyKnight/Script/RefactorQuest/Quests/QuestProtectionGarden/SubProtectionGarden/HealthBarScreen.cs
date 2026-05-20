using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScreen : IPlayerStrategyHealtheble, IDisposable
{
    private Image _imageScroller;
    private Canvas _canvas;
    private readonly float _startHealth;
    private float _timeOverImmunityDamage;
    private float _timeImmunity;
    private float _healthPlayer;
    private bool _isAlife;

    public event Action OnEnd;

    public HealthBarScreen(Canvas canvas, Image imageScroller, float startHealth, float timeImmunity)
    {
        _canvas = canvas;
        _timeImmunity = timeImmunity;
        _startHealth = startHealth;
        _healthPlayer = _startHealth;
        _imageScroller = imageScroller;
        _isAlife = true;
    }

    public void Dispose()
    {
        //_cancelToken?.Cancel();
        //_cancelToken?.Dispose();
        //_cancelToken = null;
    }

    public void TakeDamage(float damage)
    {
        if (!CheckImmunityDamage()) { return; }
        damage = MathF.Abs(damage);
        _healthPlayer -= damage;
        _timeOverImmunityDamage = Time.time + _timeImmunity;
        Debug.Log($"_healthPlayer = {_healthPlayer}");
        CheckHealth();
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

    private void CheckHealth()
    {
        if (_healthPlayer < 0)
        {
            End();
        }
        else
        {
            ChangeUi();
            //ChangeBlackout();
            //if (_cancelToken != null)
            //{
            //    _cancelToken.Cancel();
            //}
            //_cancelToken = new CancellationTokenSource();
            //RegenHealth(_cancelToken).Forget();
        }
    }

    private void ChangeUi()
    {
        float coeficent = _healthPlayer / _startHealth;
        _imageScroller.fillAmount = coeficent;
    }

    private void End()
    {
        _canvas.gameObject.SetActive(false);
        _isAlife = false;
        Debug.Log($"End");
        OnEnd?.Invoke();
    }
}