using DG.Tweening;
using System;
using UnityEngine;

public class AnimationMonsterGarden : IDisposable
{
    private ParticleSystem _particleSystem;
    private SpriteRenderer _spriteRenderer;
    private Transform _body;
    private Vector3 _startPos;
    private Color _hitColor;
    private Color _defaultColor;
    private Sequence _onHitDamage;

    private bool isAlife;

    public event Action OnEndAnimationDead;

    public AnimationMonsterGarden(SpriteRenderer spriteRenderer, Transform body, ParticleSystem particleSystem)
    {
        _spriteRenderer = spriteRenderer;
        _body = body;
        _hitColor = Color.red;
        _defaultColor = spriteRenderer.color;
        _startPos = _body.position;
        isAlife = true;
        _particleSystem = particleSystem;
    }

    public void Dispose()
    {
        if (_onHitDamage != null)
        {
            _onHitDamage?.Kill();
        }
    }

    public void OnHitDamage()
    {
        if (!isAlife) { return; }
        if (_onHitDamage == null)
        {
            _onHitDamage = DOTween.Sequence();
            _onHitDamage.Append(_spriteRenderer.DOColor(_hitColor, 0.1f))
                .Join(_body.DOShakePosition(0.1f))
                .Append(_spriteRenderer.DOColor(_defaultColor, 0.2f))
                .SetLink(_body.gameObject)
                .SetAutoKill(false);
        }
        _onHitDamage.Restart();
    }

    public void OnDead()
    {
        isAlife = false;
        Debug.Log("OnDead");
        Sequence OnDead = DOTween.Sequence();
        OnDead.Append(_spriteRenderer.DOColor(_hitColor, 0.1f))
            .Join(_body.DOScale(2, 1f))
            .Append(_body.DOScale(0.1f, 0.3f))
            .SetLink(_body.gameObject)
            .OnComplete(() => OnEndAnimationDead?.Invoke());
        OnDead.Play();
    }

    public void OnAttack()
    {
        if (!isAlife) { return; }
        _particleSystem.Play();
        Sequence OnAttack = DOTween.Sequence();
        OnAttack.Append(
            _body.DOScaleY(1.5f, 0.5f)
            .SetLoops(4, LoopType.Yoyo)
            .SetEase(Ease.InOutSine));

        OnAttack.SetLink(_body.gameObject);
        OnAttack.Play();
    }
}