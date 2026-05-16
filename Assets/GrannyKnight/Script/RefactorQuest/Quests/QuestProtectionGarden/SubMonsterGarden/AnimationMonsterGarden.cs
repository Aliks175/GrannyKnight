using DG.Tweening;
using System;
using UnityEngine;

public class AnimationMonsterGarden : IDisposable
{
    private SpriteRenderer _spriteRenderer;
    private Transform _body;
    private Vector3 _startPos;
    private Color _hitColor;
    private Color _defaultColor;
    private Sequence _onHitDamage;

    private bool isAlife;

    //private Sequence _currentSequence;
    //private Sequence _currentSequence;

    public event Action OnEndAnimationDead;

    public AnimationMonsterGarden(SpriteRenderer spriteRenderer, Transform body)
    {
        _spriteRenderer = spriteRenderer;
        _body = body;
        _hitColor = Color.red;
        _defaultColor = spriteRenderer.color;
        _startPos = _body.position;
        isAlife = true;
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
            //.OnComplete(() => DefaultStage());
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
            .OnComplete(()=> OnEndAnimationDead?.Invoke());
        OnDead.Play();
    }

    public void OnAttack()
    {
        if (!isAlife) { return; }
        Sequence OnAttack = DOTween.Sequence();
        OnAttack.Append(_body.DOScaleY(1.5f, 0.3f).SetLoops(4, LoopType.Yoyo))
            .Append(_body.DOPunchPosition(-_body.forward * 2, 1f, 0, 0))
        .Join(_body.DOScale(1, 1f))
        .SetLink(_body.gameObject);

        OnAttack.Play();
    }

    //private void ChangeSequence(Sequence sequence)
    //{
    //    if (_currentSequence != null)
    //    {
    //        _currentSequence?.Kill(true);
    //    }
    //    _currentSequence = sequence;
    //}

    //private void OnEndAnimation()
    //{
    //    OnEndAnimationDead?.Invoke();
    //}

    //private void DefaultStage()
    //{
    //    _spriteRenderer.color = _defaultColor;
    //    _body.localScale = Vector3.one;
    //}


}
