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
    private Sequence _currentSequence;

    public event Action OnEndAnimationDead;

    public AnimationMonsterGarden(SpriteRenderer spriteRenderer, Transform body)
    {
        _spriteRenderer = spriteRenderer;
        _body = body;
        _hitColor = Color.red;
        _defaultColor = spriteRenderer.color;
        _startPos = _body.position;
    }

    public void Dispose()
    {
        if (_currentSequence != null)
        {
            _currentSequence?.Kill();
        }
    }

    public void OnHitDamage()
    {
        Sequence mySequence = DOTween.Sequence();
        ChangeSequence(mySequence);
        mySequence.Append(_spriteRenderer.DOColor(_hitColor, 0.1f))
            .Join(_body.DOShakePosition(0.1f))
            .Append(_spriteRenderer.DOColor(_defaultColor, 0.2f))
        .OnComplete(() => DefaultStage());
        mySequence.Play();
    }

    public void OnDead()
    {
        Sequence mySequence = DOTween.Sequence();
        ChangeSequence(mySequence);
        mySequence.Append(_spriteRenderer.DOColor(_hitColor, 0.1f))
            .Join(_body.DOScale(2, 1f))
            .Append(_body.DOScale(0.1f, 0.3f))
        .OnComplete(() => OnEndAnimation());
        mySequence.Play();
    }

    public void OnAttack()
    {
        Debug.Log("OnAttack");
        Sequence mySequence = DOTween.Sequence();
        ChangeSequence(mySequence);
        _currentSequence.Append(_body.DOScale(2, 1f))
            
            .Join(_body.DOJump(_body.position, 2f, 3, 1f))
            .Append(_body.DOScale(1, 1f))
             .OnComplete(() => DefaultStage());
        _currentSequence.SetLink(_body.gameObject);
        _currentSequence.Play();
    }

    private void ChangeSequence(Sequence sequence)
    {
        if (_currentSequence != null)
        {
            _currentSequence?.Kill(true);
        }
        _currentSequence = sequence;
    }

    private void OnEndAnimation()
    {
        OnEndAnimationDead?.Invoke();
    }

    private void DefaultStage()
    {
        _spriteRenderer.color = _defaultColor;
        _body.localScale = Vector3.one;
    }

   
}
